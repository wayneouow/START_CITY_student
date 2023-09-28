using System.Collections.Generic;
using LO.Event;
using LO.Helper;
using LO.Meta;
using LO.Utils;

namespace LO.Model
{
	public enum LODemoState
	{
		DEFAULT = 0,
		BLOCK_SELECT = 1,
		CARD_SELECT = 2,
		BUILDING_ACTION = 3,
		BUILDING_ACTION_CONFIRM = 4,
		CONFIRM_DISCARD_CARD = 5,
		CONFIRM_COIN_NOT_ENOUGH,
		EndGame,
	}

    public interface ILODemoModel
    {
        event LOSimpleEvent OnDataLoaded;
        event LOBoolEvent OnCardDrew;
        event LOSimpleEvent OnCardUsed;
        event LOSimpleEvent OnTimeUpdate;
        event LOSimpleEvent OnBankrupt;
        event LOFloatEvent OnTaxCharged;
        event LOSimpleEvent OnEndGame;
        event LODisasterEvent OnDisasterOccur;

        void UpdateDemoState(LODemoState state);
        void DrawCard(bool animation);
        void Restart();
        void Reload();
        void AddCoin(float value);
        void UseCard();
        void SetGameTime(float newTime);
        bool CheckEndGame();

        float Coin { get; }
        int People { get; }
        float GameTime { get; }
        float EndTime { get; }
        LOTaxSequenceMeta TaxSequence { get; }

        LOGameCardModel CurrentCardModel { get; }

        LODemoState DemoState { get; }
    }

    public class LODemoModel : ILODemoModel, ILODisasterHelperDelegate, ILOTaxHelperDelegate
    {
        public static ILODemoModel Create(LODemoMeta meta)
        {
            return new LODemoModel(meta);
        }

        public int NowCardIndex; // 目前發到第幾張牌
        public List<LOGameCardModel> UserCardList = new List<LOGameCardModel>();

        public LOGameCardModel CurrentCardModel
        {
            get => UserCardList[NowCardIndex];
        }

        public float Coin { get; private set; }
        public int People { get; private set; }
        public float GameTime { get; set; }
        public float EndTime { get; private set; }
        public LOTaxSequenceMeta TaxSequence
        {
            get => m_Meta.TaxSequenceMeta;
        }

        public event LOSimpleEvent OnDataLoaded;
        public event LOBoolEvent OnCardDrew;
        public event LOSimpleEvent OnCardUsed;
        public event LOSimpleEvent OnCoinChange;
        public event LOSimpleEvent OnBankrupt;
        public event LOSimpleEvent OnStateChange;
        public event LOSimpleEvent OnTimeUpdate;
        public event LOFloatEvent OnTaxCharged;
        public event LOSimpleEvent OnEndGame;
        public event LODisasterEvent OnDisasterOccur;

        LODemoState m_DemoState = LODemoState.DEFAULT;
        public LODemoState DemoState
        {
            get => m_DemoState;
        }

        LODemoMeta m_Meta;
        LODisasterHelper m_DisasterHelper;
        LOTaxHelper m_TaxHelper;

        private LODemoModel(LODemoMeta meta)
        {
            m_Meta = meta;
            Coin = meta.Coin;
            EndTime = meta.EndTime;

            m_DisasterHelper = LODisasterHelper.Create(meta.DisasterSequenceMeta, this);
            m_TaxHelper = LOTaxHelper.Create(meta.TaxSequenceMeta, this);
        }

        public void Reload()
        {
            NowCardIndex = -1;
            LOAddressable.Load<LOCardBookMeta>(
                LOConst.UserCardKey,
                (book) =>
                {
                    foreach (var cardMeta in book.CardList)
                    {
                        var cardModel = LOGameCardModel.Create(cardMeta);
                        UserCardList.Add(cardModel);
                        // 根據設定的使用者卡牌，產生對應的 model 之後，隨機排列
                        UserCardList = UserCardList.Shuffle<LOGameCardModel>();
                    }

                    OnDataLoaded?.Invoke();
                }
            );
        }

        public void Restart()
        {
            Coin = m_Meta.Coin;
            GameTime = 0;
            NowCardIndex = 0;
            m_DisasterHelper.Reset();
            m_TaxHelper.Reset();
            OnCoinChange?.Invoke();
            OnTimeUpdate?.Invoke();
        }

        public void DrawCard(bool animation)
        {
            if (NowCardIndex < UserCardList.Count - 1)
            {
                NowCardIndex++;
                OnCardDrew?.Invoke(animation);
            }
        }

        public void AddCoin(float value)
        {
            Coin += value;

            if (Coin < 0)
            {
                OnBankrupt?.Invoke();
            }

            OnCoinChange?.Invoke();
        }

        public void UseCard()
        {
            OnCardUsed?.Invoke();
        }

        public void SetGameTime(float newTime)
        {
            GameTime = newTime;
            OnTimeUpdate?.Invoke();
            CheckTax();
            CheckDisaster();
        }

        public bool CheckEndGame()
        {
            return GameTime >= EndTime;
        }

        public void UpdateDemoState(LODemoState state)
        {
            m_DemoState = state;
            OnStateChange?.Invoke();

            if (m_DemoState == LODemoState.EndGame)
            {
                OnEndGame?.Invoke();
            }
        }

        void CheckTax()
        {
            m_TaxHelper.CheckNextTaxOccur(GameTime);
        }

        void CheckDisaster()
        {
            m_DisasterHelper.CheckNextDisasterOccur(GameTime);
        }

        void ILODisasterHelperDelegate.DisasterGroupOccur(ILODisasterGroupModelBase disasterGroup)
        {
            foreach (var disaster in disasterGroup.DisasterList)
            {
                OnDisasterOccur?.Invoke(disaster.Position, disaster.Damage);
            }
        }

        void ILOTaxHelperDelegate.TaxOccur(ILOTaxModel tax)
        {
            AddCoin(-tax.TaxAmount);
            OnTaxCharged?.Invoke(tax.TaxAmount);
        }
    }
}

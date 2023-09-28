using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Event;
using LO.Utils;
using LO.Meta;

namespace LO.Model
{
    public enum LOGameState
    {
        DEFAULT = 0,
        BLOCK_SELECT = 1, // 一般選取區塊時的狀態，如果上面有建築物則會跳出建築物功能
        CARD_SELECT = 2, // 選擇卡片後的狀態
        DISASTER_SELECT = 3 // 選擇災害後的狀態， only for testing
    }

    public class LOGameModel
    {
        public static LOGameModel Create(LOGameMeta meta)
        {
            return new LOGameModel(meta);
        }

        public int NowCardIndex; // 目前發到第幾張牌
        public List<LOGameCardModel> UserCardList = new List<LOGameCardModel>();
        public int TotalCardNumber
        {
            get => UserCardList.Count;
        }
        public LOGameCardModel CurrentCardModel
        {
            get => UserCardList[NowCardIndex];
        }

        public float Coin;
        public int People;
        public float GameTime;
        public LOGameState GameState = LOGameState.DEFAULT;
        public float DisasterValue;

        public event LOSimpleEvent OnGameDataLoaded;
        public event LOSimpleEvent OnGameStateChange;
        public event LOSimpleEvent OnGameCardDrew;
        public event LOSimpleEvent OnGameCoinChange;
        public event LOSimpleEvent OnGamePeopleChange;
        public event LOSimpleEvent OnGameTimeUpdate;

        private LOGameModel(LOGameMeta meta)
        {
            Coin = meta.Coin;
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

                    OnGameDataLoaded?.Invoke();
                }
            );
        }

        public void AddCoin(float value)
        {
            Coin += value;
            OnGameCoinChange?.Invoke();
        }

        public void AddTime(float value)
        {
            GameTime += value;
            OnGameTimeUpdate?.Invoke();
        }

        public void UpdatePeople(int value)
        {
            People = value;
            OnGamePeopleChange?.Invoke();
        }

        public void UpdateGameState(LOGameState gameState)
        {
            GameState = gameState;
            OnGameStateChange?.Invoke();
        }

        public void DrawCard()
        {
            if (NowCardIndex < UserCardList.Count)
            {
                NowCardIndex++;
                OnGameCardDrew?.Invoke();
            }
        }

        public void DrawCard(int cardIndex)
        {
            for (int i = 0; i < cardIndex; i++)
            {
                DrawCard();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using LO.Controller;
using LO.Model;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
    public class LOGameTestView : MonoBehaviour
    {
        [SerializeField]
        Text m_NowStateText;

        [SerializeField]
        Text m_TotalCardText;

        [SerializeField]
        Text m_TotalDrawCardText;

        [SerializeField]
        Text m_LastCardText;

        [SerializeField]
        InputField m_CoinInput;

        [SerializeField]
        InputField m_TimeInput;

        [SerializeField]
        InputField m_DisasterInput;

        LOGameController m_GameController;

        public void Init(LOGameModel gameModel)
        {
            m_GameController = LOGameApplication.Instance.GameController;
            // subscribe
            gameModel.OnGameCardDrew += HandleGameCardDrew;
            gameModel.OnGameStateChange += HandleGameStateChange;
            gameModel.OnGameCoinChange += HandleGameCoinChange;
            gameModel.OnGameTimeUpdate += HandleGameTimeChange;
            // force update first time
            HandleGameCardDrew();
            HandleGameStateChange();
        }

        void HandleGameCoinChange() { }

        void HandleGameTimeChange() { }

        void HandleGameCardDrew()
        {
            var model = m_GameController.GameModel;
            m_TotalCardText.text = $"總共有 {model.TotalCardNumber} 張卡片";
            m_TotalDrawCardText.text = $"已經抽了 {model.NowCardIndex + 1} 張卡片";
            m_LastCardText.text = $"剩下 {model.TotalCardNumber - (model.NowCardIndex + 1)} 張卡片";
        }

        void HandleGameStateChange()
        {
            var nowState = m_GameController.GameModel.GameState;
            var text = "現在狀態 ";
            switch (nowState)
            {
                case LOGameState.DEFAULT:
                    text += "<color=yellow>沒有點擊</color>";
                    break;
                case LOGameState.BLOCK_SELECT:
                    text += "<color=yellow>選擇地區</color>";
                    break;
                case LOGameState.CARD_SELECT:
                    text += "<color=yellow>選擇卡片</color>";
                    break;
                case LOGameState.DISASTER_SELECT:
                    text += "<color=yellow>選擇災害</color>";
                    break;
                default:
                    break;
            }
            m_NowStateText.text = text;
        }

        #region button handlers
        public void HandleDrawBtnClick()
        {
            m_GameController.DrawCard();
        }

        public void HandleAddCoinBtnClick()
        {
            var valueText = m_CoinInput.text;
            float value = 0;
            try
            {
                value = float.Parse(valueText);
                m_GameController.AddCoin(value);
            }
            catch (System.Exception)
            {
                // NOTE : if user input invalid value then do nothing
            }

            m_CoinInput.text = "";
        }

        public void HandleAddTimeBtnClick()
        {
            string valueText = m_TimeInput.text;

            if (valueText == "")
            {
                valueText = "1";
            }

            float value = 0;
            try
            {
                value = float.Parse(valueText);
                value = Mathf.Max(0, value);
                m_GameController.AddTime(value);
            }
            catch (System.Exception)
            {
                // NOTE : if user input invalid value then do nothing
            }

            m_TimeInput.text = "";
        }

        public void HandleDisasterToggleClick(bool isOn)
        {
            LOGameApplication.Instance.GameController.UpdateGameState(
                isOn ? LOGameState.DISASTER_SELECT : LOGameState.DEFAULT
            );
        }

        public void HandleDisasterValueChanged()
        {
            var valueText = m_DisasterInput.text;
            float value = 0;
            try
            {
                value = float.Parse(valueText);
            }
            catch (System.Exception)
            {
                // NOTE : if user input invalid value then do nothing
            }

            LOGameApplication.Instance.GameController.GameModel.DisasterValue = value;
        }

        #endregion
    }
}

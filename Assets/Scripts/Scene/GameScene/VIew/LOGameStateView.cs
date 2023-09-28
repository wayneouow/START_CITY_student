using System.Collections;
using System.Collections.Generic;
using LO.Model;
using LO.Meta;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
    public class LOGameStateView : MonoBehaviour
    {
        [SerializeField]
        Text m_CoinText;

        [SerializeField]
        Text m_PeopleText;

        [SerializeField]
        Text m_TimeText;

        [SerializeField]
        Text m_BuildingText;

        public void Init(LOGameModel gameModel)
        {
            // subscribe the model event to get the update event
            gameModel.OnGameCoinChange += HandleGameCoinChange;
            gameModel.OnGamePeopleChange += HandleGamePeopleChange;
            gameModel.OnGameTimeUpdate += HandleGameTimeChange;

            UpdateUI();
        }

        void UpdateUI()
        {
            var model = LOGameApplication.Instance.GameController.GameModel;
            m_CoinText.text = model.Coin.ToString();
            m_PeopleText.text = model.People.ToString();

            var gameTime = model.GameTime;
            int h = Mathf.FloorToInt(gameTime / 3600);
            int m = Mathf.FloorToInt(gameTime / 60);
            int s = (int)(gameTime) % 60;
            m_TimeText.text = $"{h}:{m}:{s}";
        }

        void HandleGameCoinChange()
        {
            UpdateUI();
        }

        void HandleGamePeopleChange()
        {
            UpdateUI();
        }

        void HandleGameTimeChange()
        {
            UpdateUI();
        }

        public void LogBuilding(LOGameSceneAreaView area)
        {
            if (area == null || area.BuildingView == null)
            {
                m_BuildingText.text = "";
                return;
            }

            var model = area.BuildingView.BuildingModel;

            string text = "";

            if (model.IsBuilding)
            {
                text = BuildingModelToString(model as LOGameBuildingModel);
            }
            else if (model.IsProduction)
            {
                text = ProductionModelToString(model as LOGameProductionModel);
            }

            m_BuildingText.text = text;
        }

        string BuildingModelToString(LOGameBuildingModel model)
        {
            string text = "";

            var meta = model.CardModel.CardMeta as LOBuildingCardMeta;
            text += $"名字 : {meta.Name}";
            if (!model.IsBuildDone)
            {
                var duration = model.CurrentBasicProperties.BuildingDuration;
                text += $"\n建造進度 {duration - model.BuildingTime}/{duration}";
            }
            text += $"\n空閑的人數 : {model.FreePeople}";
            text += $"\n目前總人數 : {model.TotalPeople}";
            text += $"\n-------------";
            text += $"\n每秒產生人數 : {model.CurrentBuildingProperties.PeoplePerSec}";
            text += $"\n人數上限 : {model.CurrentBuildingProperties.TotalPeople}";

            return text;
        }

        string ProductionModelToString(LOGameProductionModel model)
        {
            string text = "";

            var meta = model.CardModel.CardMeta as LOProductionCardMeta;
            text += $"名字 : {meta.Name}";
            if (!model.IsBuildDone)
            {
                var duration = model.CurrentBasicProperties.BuildingDuration;
                text += $"\n建造進度 {duration - model.BuildingTime}/{duration}";
            }
            text += $"\n總共產生的金幣數量 : {model.Coin}";
            text += $"\n目前建築內生產人數 : {model.Vacancies}";
            text += $"\n-------------";
            text += $"\n每秒產生金幣 : {model.CurrentProductionProperties.CoinPerSec}";
            text += $"\n人數上限 : {model.CurrentProductionProperties.TotalVacancies}";

            return text;
        }
    }
}

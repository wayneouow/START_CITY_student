using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LO.Model;
using TMPro;

namespace LO.View
{
    public class LOGameBuildingView : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI m_BuildDurationText;

        [SerializeField]
        Image m_HPImage;

        [SerializeField]
        Image m_BuildingImage;

        public LOGameBuildingModelBase BuildingModel;

        public void Init(LOGameBuildingModelBase buildingModel)
        {
            BuildingModel = buildingModel;

            // subscribe
            BuildingModel.OnHpUpdate += HandleHpUpdate;

            // update first time
            UpdateView();
            UpdateViewByTime();
            HandleHpUpdate();
        }

        public void UpdateView()
        {
            m_BuildingImage.sprite = BuildingModel.CurrentBuildingImage;
        }

        public void UpdateViewByTime()
        {
            var isBuildDone = BuildingModel.IsBuildDone;
            // 如果還沒建造完成，則顯示剩下的時間
            m_BuildDurationText.text = isBuildDone ? "" : BuildingModel.BuildingTime.ToString();
            // 如果還沒建造完成，顏色變灰色
            m_BuildingImage.color = isBuildDone ? Color.white : Color.gray;
        }

        public void HandleHpUpdate()
        {
            m_HPImage.fillAmount = BuildingModel.HPRatio;
        }

        public void HandleGameTimeChange(float gameTime)
        {
            BuildingModel.UpdateLastUpdateGameTime(gameTime);
            UpdateViewByTime();
        }

        public void HandleDisasterToBuilding(float disasterValue)
        {
            BuildingModel.DoDamage(disasterValue);
        }
    }
}

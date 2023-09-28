using System.Collections;
using System.Collections.Generic;
using LO.Meta;
using LO.Model;
using UnityEngine;
using UnityEngine.UI;
using LO.Utils;

namespace LO.View
{
    public class LOCardDetailView : MonoBehaviour
    {
        [SerializeField]
        Image m_CardBgImage;

        [SerializeField]
        Image m_CardImage;

        [SerializeField]
        Text m_NameText;

        [SerializeField]
        Text m_TypeText;

        [SerializeField]
        LOCardColorMeta m_ColorMeta;

        [SerializeField]
        RectTransform m_DetailRootRect;

        [SerializeField]
        GameObject m_DetailPairPrefab;
        List<LOCardDetailPairView> m_DetailPairViews = new List<LOCardDetailPairView>();

        LOGameCardModel m_CardModel;

        public void UpdateView(LOGameCardModel cardModel)
        {
            m_CardModel = cardModel;

            RemovePreDetailPairView();
            UpdateBaseUI();
            UpdateDetailByType();
        }

        void RemovePreDetailPairView()
        {
            foreach (var pairView in m_DetailPairViews)
            {
                GameObject.Destroy(pairView.gameObject);
            }

            m_DetailPairViews = new List<LOCardDetailPairView>();
        }

        void UpdateBaseUI()
        {
            m_CardBgImage.color = m_ColorMeta.CardColors[
                (int)m_CardModel.CardMeta.CardType
            ].CardUpColor;
            m_CardImage.sprite = m_CardModel.CardMeta.CardImage;
            m_NameText.text = m_CardModel.CardMeta.Name;
            m_TypeText.text = LOCardModelBase.GetTypeName(m_CardModel.CardMeta.CardType);

            LOUtils.ClampImageSize(m_CardImage, 300, 250, 300, 250);
        }

        void UpdateDetailByType()
        {
            UpdateBaseDetail();
            switch (m_CardModel.CardMeta.CardType)
            {
                case LOCardType.Building:
                    UpdateBuildingDetail();
                    break;
                case LOCardType.Production:
                    UpdateProductionDetail();
                    break;
                default:
                    UpdateFunctionalDetail();
                    break;
            }

            UpdatePairViewsPos();
        }

        void AddPairView(string keyText, string valueText)
        {
            var pairView = GameObject
                .Instantiate(m_DetailPairPrefab, m_DetailRootRect)
                .GetComponent<LOCardDetailPairView>();

            pairView.UpdateText(keyText, valueText);

            m_DetailPairViews.Add(pairView);
        }

        void UpdateBaseDetail()
        {
            var properties = m_CardModel.CardMeta.BasicProperties[0];
            AddPairView("花費金額", properties.Cost.ToString());
        }

        void UpdateBaseBuildingDetail()
        {
            var properties = m_CardModel.CardMeta.BasicProperties[0];
            AddPairView("建造時間", LOUtils.GetTimeStringFromSecond(properties.BuildingDuration));
            AddPairView("生命值", properties.HP.ToString());
        }

        void UpdateBuildingDetail()
        {
            UpdateBaseBuildingDetail();

            var meta = m_CardModel.CardMeta as LOBuildingCardMeta;
            var properties = meta.BuildingProperties[0];
            AddPairView("人口上限", properties.TotalPeople.ToString());
            AddPairView("人口產出", $"{properties.PeoplePerSec.ToString()} / 每秒");
        }

        void UpdateProductionDetail()
        {
            UpdateBaseBuildingDetail();

            var meta = m_CardModel.CardMeta as LOProductionCardMeta;
            var properties = meta.ProductionProperties[0];
            AddPairView("職員上限", properties.TotalVacancies.ToString());
            AddPairView("職員產能", $"每人每秒 {properties.CoinPerSec.ToString()} 金幣");
        }

        void UpdateFunctionalDetail()
        {
            AddPairView("敘述", m_CardModel.CardMeta.Desc);
        }

        void UpdatePairViewsPos()
        {
            float initPos = 60;
            float gap = 30;

            for (int i = 0; i < m_DetailPairViews.Count; i++)
            {
                m_DetailPairViews[i].UpdatePos(-(initPos + i * gap));
            }
        }
    }
}

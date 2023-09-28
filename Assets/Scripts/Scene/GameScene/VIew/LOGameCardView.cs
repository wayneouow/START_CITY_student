using System.Collections;
using System.Collections.Generic;
using LO.Model;
using UnityEngine;
using UnityEngine.UI;
using LO.Utils;

namespace LO.View
{
    public class LOGameCardView : MonoBehaviour
    {
        [SerializeField]
        RectTransform m_RectTransform;

        [SerializeField]
        Text m_CostText;

        [SerializeField]
        Text m_NameText;

        [SerializeField]
        Image m_CardImage;

        [SerializeField]
        Image m_BorderImage;

        [SerializeField]
        Color m_BorderDefaultColor;

        [SerializeField]
        Color m_BorderSelectedColor;

        [SerializeField]
        LOGameCardListView m_ParentListView;

        public LOGameCardModel CardModel;

        private void Start()
        {
            m_BorderDefaultColor = m_BorderImage.color;
        }

        public void UpdateView(LOGameCardModel cardModel, LOGameCardListView parentListView)
        {
            CardModel = cardModel;
            m_ParentListView = parentListView;

            UpdateUI();
        }

        void UpdateUI()
        {
            m_CardImage.sprite = CardModel.CardMeta.CardImage;
            m_CostText.text = CardModel.CardMeta.BasicProperties[0].Cost.ToString();
            m_NameText.text = CardModel.CardMeta.Name;
        }

        public void HandleSelected()
        {
            m_BorderImage.color = m_BorderSelectedColor;
            m_RectTransform.SetAnchoredPositionY(10);
        }

        public void HandleUnSelected()
        {
            m_BorderImage.color = m_BorderDefaultColor;
            m_RectTransform.SetAnchoredPositionY(0);
        }

        public void HandleCardClick()
        {
            LOGameApplication.Instance.GameController.SelectCard(this);
        }

        public void UpdateCardPosX(float posX)
        {
            m_RectTransform.SetAnchoredPositionX(posX);
        }

        public void RemoveCard()
        {
            m_ParentListView.RemoveCard(this);
        }

        public void HandleDeleteClick()
        {
            RemoveCard();
        }
    }
}

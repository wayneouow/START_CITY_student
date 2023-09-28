using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LO.Meta;
using LO.Model;
using LO.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
    public interface ILODemoCardViewDelegate
    {
        void OnCardClick(LODemoCardView cardView);
        void OnDeleteClick(LODemoCardView cardView);
    }

    public interface ILODemoCardView
    {
        ILODemoCardViewDelegate ViewDelegate { get; }
        void CardFlyIn(float posX, bool animation);
        void UpdateCardPosX(float posX, bool animation);
        void HandleSelected();
        void HandleUnSelected();
        LOGameCardModel CardModel { get; }
    }

    public class LODemoCardView : MonoBehaviour, ILODemoCardView
    {
        public ILODemoCardViewDelegate ViewDelegate { get; set; }

        [SerializeField]
        RectTransform m_RectTransform;

        [SerializeField]
        Text m_CostText;

        [SerializeField]
        RectTransform m_CostBgRect;

        [SerializeField]
        Text m_NameText;

        [SerializeField]
        Image m_CardImage;

        [SerializeField]
        Image m_CardIcon;

        [SerializeField]
        Image m_CardAmount;

        public GameObject BorderImage;

        [SerializeField]
        LOCardColorMeta m_ColorMeta;

        [SerializeField]
        Image m_UpImage;

        [SerializeField]
        Image m_DownImage;

        [SerializeField]
        bool m_CanInteract = true; // If the card is playing an animation, interaction is blocked

        public LOGameCardModel CardModel { get; private set; }

        public GameObject DeleteBtn; // if the card is in the card view, then it should not be deleted

        public void UpdateView(LOGameCardModel cardModel)
        {
            CardModel = cardModel;
            m_CanInteract = true;
            UpdateUI();
        }

        void UpdateUI()
        {
            m_CardImage.sprite = CardModel.CardMeta.CardImage;
            m_CostText.text = CardModel.CardMeta.BasicProperties[0].Cost.ToString();
            m_CostBgRect.SetWidth(m_CostText.preferredWidth + 20);

            m_NameText.text = CardModel.CardMeta.Name;

            var cardColor = m_ColorMeta.CardColors[(int)CardModel.CardMeta.CardType];
            m_UpImage.color = cardColor.CardUpColor;

            LOUtils.ClampImageSize(m_CardImage, 170, 190);
        }

        public void HandleSelected()
        {
            BorderImage.SetActive(true);
            m_RectTransform.SetAnchoredPositionY(10);
        }

        public void HandleUnSelected()
        {
            BorderImage.SetActive(false);
            m_RectTransform.SetAnchoredPositionY(0);
        }

        public void CardFlyIn(float posX, bool animation)
        {
            // TODO : refine the default fly in position
            m_RectTransform.SetAnchoredPositionX(1100);
            UpdateCardPosX(posX, animation);
        }

        public void UpdateCardPosX(float posX, bool animation)
        {
            if (animation)
            {
                DoFlyAnimation(posX);
            }
            else
            {
                m_RectTransform.SetAnchoredPositionX(posX);
            }
        }

		#region btn event
        public void HandleCardClick()
        {
            if (!m_CanInteract)
                return;
            ViewDelegate?.OnCardClick(this);
            LOAudio.Instance.PlayGameCardClick();
        }

        public void HandleDeleteClick()
        {
            if (!m_CanInteract)
                return;
            ViewDelegate?.OnDeleteClick(this);
        }
		#endregion

        void DoFlyAnimation(float posX)
        {
            m_CanInteract = false;
            m_RectTransform
                .DOAnchorPosX(posX, 0.3f)
                .OnComplete(() =>
                {
                    m_CanInteract = true;
                });
        }
    }
}

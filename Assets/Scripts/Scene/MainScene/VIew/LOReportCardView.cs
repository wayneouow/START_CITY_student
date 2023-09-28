using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Meta;
using LO.Model;
using LO.Utils;

namespace LO.View
{
    public class LOReportCardView : LOMainReportContentView, ILODemoCardViewDelegate
    {
        [SerializeField]
        LOCardBookMeta m_CardBookMeta;

        [SerializeField]
        GameObject m_CardPrefab;

        [SerializeField]
        RectTransform m_CardRootRect;

        [SerializeField]
        LOCardDetailView m_DetailView;

        [SerializeField]
        List<LODemoCardView> m_CardViews = new List<LODemoCardView>();
        LODemoCardView m_SelectedCardView;

        const int CARD_COL = 4;
        const float CARD_WIDTH = 190;
        const float CARD_HEIGHT = 260;
        const float PADDING_BOTTOM = 10;

        public override void Init()
        {
            base.Init();

            CreateCardView();
            // select first card
            OnCardClick(m_CardViews[0]);
        }

        void CreateCardView()
        {
            for (int i = 0; i < m_CardBookMeta.CardList.Count; i++)
            {
                var cardMeta = m_CardBookMeta.CardList[i];
                var cardModel = LOGameCardModel.Create(cardMeta);

                var reportCard = GameObject.Instantiate(m_CardPrefab, m_CardRootRect);
                var cardView = reportCard.GetComponentInChildren<LODemoCardView>();

                cardView.ViewDelegate = this;
                cardView.UpdateView(cardModel);
                cardView.DeleteBtn.SetActive(false); // hide delete button

                int col = i % CARD_COL;
                int row = i / CARD_COL;

                reportCard
                    .GetComponent<RectTransform>()
                    .SetAnchoredPosition(CARD_WIDTH * col, -CARD_HEIGHT * row);

                m_CardViews.Add(cardView);
            }
            UpdateCardRootHeight();
        }

        void UpdateCardRootHeight()
        {
            int totalRow = m_CardBookMeta.CardList.Count / CARD_COL + 1;
            m_CardRootRect.SetHeight(CARD_HEIGHT * totalRow + PADDING_BOTTOM);
        }

        public void OnCardClick(LODemoCardView cardView)
        {
            if (m_SelectedCardView != null)
            {
                m_SelectedCardView.BorderImage.SetActive(false);
            }

            m_SelectedCardView = cardView;
            m_SelectedCardView.BorderImage.SetActive(true);
            m_DetailView.UpdateView(cardView.CardModel);

            LOAudio.Instance.PlayBtnActive();
        }

        public void OnDeleteClick(LODemoCardView cardView)
        {
            // Do nothing
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Meta;
using DG.Tweening;
using LO.Utils;

namespace LO.View
{
    public class LOReportLevelView
        : LOMainReportContentView,
            ILOLevelBtnViewDelegate,
            ILOLevelDetailViewDelegate
    {
        [Space]
        [SerializeField]
        LOGameLevelListMeta m_LevelListMeta;

        [SerializeField]
        GameObject m_LevelBtnPrefab;

        [SerializeField]
        RectTransform m_LevelRootRect;

        [SerializeField]
        LOLevelDetailView m_DetailView;

        [Space]
        [SerializeField]
        CanvasGroup m_ListPageCanvasGroup;

        [SerializeField]
        CanvasGroup m_DetailPageCanvasGroup;

        const int LV_BTN_COL = 3;
        const float LV_BTN_WIDTH = 285;
        const float LV_BTN_HEIGHT = 250;
        const float PADDING_BOTTOM = 10;

        public override void Init()
        {
            base.Init();

            CreateLevelList();
            InitPage();
        }

        void InitPage()
        {
            m_ListPageCanvasGroup.SetInteract(true);
            m_ListPageCanvasGroup.alpha = 1;
            m_DetailPageCanvasGroup.SetInteract(false);
            m_DetailPageCanvasGroup.alpha = 0;
        }

        void CreateLevelList()
        {
            
            foreach (var levelMeta in m_LevelListMeta.LevelList)
            {
                var btnView = GameObject
                    .Instantiate(m_LevelBtnPrefab, m_LevelRootRect)
                    .GetComponent<LOLevelBtnView>();
                btnView.UpdateView(levelMeta, this);
            }
        
        }
/*
        void CreateLevelList()
        {
            
            for(int i = 0; i < m_LevelListMeta.LevelList.Count; i++)
            {
                var levelMeta = m_LevelListMeta.LevelList[i];
                var reportLevel = GameObject.Instantiate(m_LevelBtnPrefab, m_LevelRootRect);
                var btnView = reportLevel.GetComponent<LOLevelBtnView>();
                
                int col = i % LV_BTN_COL;
                int row = i / LV_BTN_COL;

                reportLevel
                    .GetComponent<RectTransform>()
                    .SetAnchoredPosition(LV_BTN_WIDTH * col, -LV_BTN_HEIGHT * row);
    
                btnView.UpdateView(levelMeta, this);
                UpdateLevelRootHeight();
            }
        
        }
/*      
        void CreateLevelc()
        {
            for (int i = 0; i < m_LevelListMeta.LevelList.Count; i++)
            {
                var levelMeta = m_LevelListMeta.LevelList[i];
                //var cardModel = LOGameCardModel.Create(cardMeta);

                var reportCard = GameObject.Instantiate(m_LevelBtnPrefab, m_LevelRootRect);
                //var cardView = reportCard.GetComponentInChildren<LODemoCardView>();
                var btnView = GameObject
                    .Instantiate(m_LevelBtnPrefab, m_LevelRootRect)
                    .GetComponent<LOLevelBtnView>();

                //cardView.ViewDelegate = this;
                //cardView.UpdateView(cardModel);
                //cardView.DeleteBtn.SetActive(false); // hide delete button

                int col = i % LV_BTN_COL;
                int row = i / LV_BTN_COL;

                reportCard
                    .GetComponent<RectTransform>()
                    .SetAnchoredPosition(LV_BTN_WIDTH * col, -LV_BTN_HEIGHT * row);

                //m_CardViews.Add(cardView);
            }
            UpdateLevelRootHeight();
        }
*/
        void UpdateLevelRootHeight()
        {
            int totalRow = m_LevelListMeta.LevelList.Count / LV_BTN_COL + 1;
            m_LevelRootRect.SetHeight(LV_BTN_HEIGHT * totalRow + PADDING_BOTTOM);
        }
      
        void ILOLevelBtnViewDelegate.HandleLevelBtnClick(LOGameLevelMeta levelMeta)
        {
            ChangePage(m_ListPageCanvasGroup, m_DetailPageCanvasGroup);

            m_DetailView.UpdateView(levelMeta, this);
            LOAudio.Instance.PlayBtnActive();
        }

        void ILOLevelDetailViewDelegate.HandleBackBtnClick()
        {
            ChangePage(m_DetailPageCanvasGroup, m_ListPageCanvasGroup);
            LOAudio.Instance.PlayBtnBack();
        }

        void ChangePage(CanvasGroup from, CanvasGroup to)
        {
            from.SetInteract(false);
            to.SetInteract(false);

            // fade out from -> fade in to -> open the interact
            var sequence = DOTween.Sequence();
            sequence
                .Append(from.DOFade(0, 0.5f))
                .Append(to.DOFade(1, 0.5f))
                .AppendCallback(() =>
                {
                    to.SetInteract(true);
                });
        }
    }
}

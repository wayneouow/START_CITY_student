using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
    public enum MainReportType
    {
        Main = 0,
        Card = 1,
        Deck = 2,
        Tech = 3,
        Store = 4,
        Level = 5
    }

    public class LOMainReportView : MonoBehaviour, ILOMainReportTabViewDelegate
    {
        [SerializeField]
        List<LOMainReportTabView> m_ReportTabs;

        [SerializeField]
        List<GameObject> m_ReportPrefabs;

        [Space]
        [SerializeField]
        RectTransform m_ContentRect;

        [Space]
        [SerializeField]
        LOMainReportContentView m_CurrentContentView;

        [SerializeField]
        LOMainReportTabView m_CurrentTabView;

        bool m_CanShow = true;

        public void Init()
        {
            foreach (var tab in m_ReportTabs)
            {
                tab.Init(this);
            }

            HandleTabClick(MainReportType.Main);
        }

        #region  ILOMainReportTabViewDelegate

        public void HandleTabClick(MainReportType type)
        {
            if (!m_CanShow)
            {
                return;
            }

            UpdateTabs(type);
            UpdateContentView(type);

            LOAudio.Instance.PlayTabSwitch();
        }

        void UpdateTabs(MainReportType type)
        {
            foreach (var tab in m_ReportTabs)
            {
                tab.UpdateActive(type);
            }
        }

        void UpdateContentView(MainReportType type)
        {
            m_CanShow = false;

            if (m_CurrentContentView != null)
            {
                m_CurrentContentView.Dismiss(() =>
                {
                    CreateContentView(type);
                });
                return;
            }
            CreateContentView(type);
        }

        void CreateContentView(MainReportType type)
        {
            m_CurrentContentView = GameObject
                .Instantiate(m_ReportPrefabs[(int)type], m_ContentRect)
                .GetComponent<LOMainReportContentView>();

            m_CurrentContentView.Init();
            m_CurrentContentView.Show(() =>
            {
                m_CanShow = true;
            });
        }

        #endregion
    }
}

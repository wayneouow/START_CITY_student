using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Utils;
using UnityEngine.UI;

namespace LO.View
{
    public interface ILOMainReportTabViewDelegate
    {
        void HandleTabClick(MainReportType type);
    }

    public class LOMainReportTabView : MonoBehaviour
    {
        [SerializeField]
        Image m_IconImg;

        [SerializeField]
        Color m_ActiveColor;

        [SerializeField]
        Color m_InactiveColor;

        [SerializeField]
        MainReportType m_TabType;

        [SerializeField]
        RectTransform m_RectTransform;
        
        ILOMainReportTabViewDelegate m_ViewDelegate;

        public void Init(ILOMainReportTabViewDelegate viewDelegate)
        {
            m_ViewDelegate = viewDelegate;
        }

        public void HandleTabClick()
        {
            m_ViewDelegate.HandleTabClick(m_TabType);
        }

        public void UpdateActive(MainReportType type)
        {
            bool active = type == m_TabType;
            m_IconImg.color = active ? m_ActiveColor : m_InactiveColor;
        
            //m_RectTransform.SetAnchoredPositionX(active ? 0 : 10); tab無退縮
        }
    }
}

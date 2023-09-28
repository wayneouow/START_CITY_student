using System.Collections;
using System.Collections.Generic;
using LO.Meta;
using LO.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
    public interface ILOLevelBtnViewDelegate
    {
        void HandleLevelBtnClick(LOGameLevelMeta levelMeta);
    }

    public class LOLevelBtnView : MonoBehaviour
    {
        [SerializeField]
        RectTransform m_RectTransform;

        [SerializeField]
        Image m_LevelImage;

        [SerializeField]
        Text m_NameText;

        [SerializeField]
        GameObject m_ActiveObject;

        [SerializeField]
        GameObject m_InactiveObject;

        [SerializeField]
        Button m_Button;

        LOGameLevelMeta m_LevelMeta;
        ILOLevelBtnViewDelegate m_ViewDelegate;

        public void UpdateView(LOGameLevelMeta levelMeta, ILOLevelBtnViewDelegate viewDelegate)
        {
            m_LevelMeta = levelMeta;
            m_ViewDelegate = viewDelegate;

            UpdateUI();
        }

        void UpdateUI()
        {
            m_LevelImage.sprite = m_LevelMeta.LevelImage;
            m_NameText.text = m_LevelMeta.Name;

            m_ActiveObject.SetActive(m_LevelMeta.Active);
            m_InactiveObject.SetActive(!m_LevelMeta.Active);

            m_Button.interactable = m_LevelMeta.Active;
        }

        public void HandleLevelBtnClicked()
        {
            m_ViewDelegate.HandleLevelBtnClick(m_LevelMeta);
        }
    }
}

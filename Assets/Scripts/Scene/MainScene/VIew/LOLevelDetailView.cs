using System.Collections;
using System.Collections.Generic;
using LO.Meta;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
    public interface ILOLevelDetailViewDelegate
    {
        void HandleBackBtnClick();
    }

    public class LOLevelDetailView : MonoBehaviour
    {
        [SerializeField]
        Image m_LevelImage;

        [SerializeField]
        Text m_NameText;

        [SerializeField]
        Text m_DescText;

        [SerializeField]
        Text m_DisasterText;

        LOGameLevelMeta m_LevelMeta;
        ILOLevelDetailViewDelegate m_ViewDelegate;

        public void UpdateView(LOGameLevelMeta levelMeta, ILOLevelDetailViewDelegate viewDelegate)
        {
            m_LevelMeta = levelMeta;
            m_ViewDelegate = viewDelegate;

            UpdateUI();
        }

        void UpdateUI()
        {
            m_LevelImage.sprite = m_LevelMeta.LevelImage;
            m_NameText.text = m_LevelMeta.Name;
            m_DescText.text = m_LevelMeta.Desc;
            m_DisasterText.text = m_LevelMeta.DisasterDesc;
        }

        public void HandleBackBtnClick()
        {
            m_ViewDelegate.HandleBackBtnClick();
            LOAudio.Instance.PlayBtnBack();
        }

        public void HandleConfirmBtnClick()
        {
            float delay = LOAudio.Instance.PlayGameStart();
            StartCoroutine(DelayGoToGame(delay));
        }

        IEnumerator DelayGoToGame(float delay)
        {
            yield return new WaitForSeconds(delay);
            LOApplication.Instance.GoToGame();
        }
    }
}

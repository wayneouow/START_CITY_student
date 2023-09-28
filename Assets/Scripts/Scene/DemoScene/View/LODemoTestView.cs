using LO.Event;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
    public interface ILODemoTestViewEventDispatcher
    {
        event LOSimpleEvent OnBankrupt;
        event LOSimpleEvent OnEndGame;
        event LOBoolEvent OnCardDrew;
    }

    public interface ILODemoTestViewDelegate
    {
        void OnDrawBtnClick();
        void OnRestartBtnClick();
    }

    public class LODemoTestView : MonoBehaviour
    {
        ILODemoTestViewDelegate m_ViewDelegate;
        ILODemoTestViewEventDispatcher m_EventDispatcher;

        [SerializeField]
        Button m_RestartBtn;

        [SerializeField]
        Button m_DrawBtn;

        public void Init(
            ILODemoTestViewDelegate viewDelegate,
            ILODemoTestViewEventDispatcher eventDispatcher
        )
        {
            m_ViewDelegate = viewDelegate;
            m_EventDispatcher = eventDispatcher;
            m_EventDispatcher.OnBankrupt += HandleGameBankrupt;
            m_EventDispatcher.OnEndGame += HandleEndGame;

            m_DrawBtn.gameObject.SetActive(false);
        }

        void ShowRestartBtn()
        {
            m_RestartBtn.gameObject.SetActive(true);
        }

        void HandleGameBankrupt()
        {
            ShowRestartBtn();
        }

        void HandleEndGame()
        {
            ShowRestartBtn();
        }

        void HandleCardUse()
        {
            m_DrawBtn.gameObject.SetActive(true);
        }

		#region button handlers
        public void HandleDrawBtnClick()
        {
            m_ViewDelegate.OnDrawBtnClick();
        }

        public void HandleRestartBtnClick()
        {
            m_ViewDelegate.OnRestartBtnClick();
            m_RestartBtn.gameObject.SetActive(false);
        }
		#endregion
    }
}

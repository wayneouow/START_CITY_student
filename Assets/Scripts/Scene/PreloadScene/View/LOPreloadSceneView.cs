using DG.Tweening;
using LO.Event;
using LO.Utils;
using UnityEngine;
using System.Collections;

namespace LO.View
{
    public interface ILOPreloadSceneViewDelegate
    {
        void HandleStartBtnClick();
    }

    public interface ILOPreloadSceneView : ILOUIBaseView
    {
        void Init(ILOPreloadSceneViewDelegate viewDelegate);
    }

    public class LOPreloadSceneView : MonoBehaviour, ILOPreloadSceneView
    {
        [SerializeField]
        CanvasGroup m_CanvasGroup;

        ILOPreloadSceneViewDelegate m_ViewDelegate;

        const float ANIM_DURATION = 1f;

        public void Init(ILOPreloadSceneViewDelegate viewDelegate)
        {
            m_ViewDelegate = viewDelegate;
        }

        // ui btn click
        public void HandleStartBtnClick()
        {
            float delay = LOAudio.Instance.PlayGameStart();
            StartCoroutine(DelayStart(delay + 1));
        }

        IEnumerator DelayStart(float time)
        {
            yield return new WaitForSeconds(time);
            m_ViewDelegate?.HandleStartBtnClick();
        }

        #region ILOUIBaseView

        public void Show(LOSimpleEvent complete = null)
        {
            Sequence fadeSequence = DOTween.Sequence();

            fadeSequence
                .PrependInterval(1)
                .Append(m_CanvasGroup.DOFade(1, ANIM_DURATION))
                .AppendCallback(() =>
                {
                    m_CanvasGroup.SetInteract(true);
                    complete?.Invoke();
                });
        }

        public void Dismiss(LOSimpleEvent complete = null)
        {
            m_CanvasGroup.SetInteract(false);
            m_CanvasGroup
                .DOFade(0, ANIM_DURATION)
                .OnComplete(() =>
                {
                    complete?.Invoke();
                });
        }

        public void Reload() { }

        #endregion
    }
}

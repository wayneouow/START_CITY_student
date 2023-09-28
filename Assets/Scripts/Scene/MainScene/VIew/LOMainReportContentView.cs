using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LO.Utils;
using LO.Event;

namespace LO.View
{
    public class LOMainReportContentView : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField]
        CanvasGroup m_CanvasGroup;

        [SerializeField]
        RectTransform m_RectTransform;

        float m_OffsetY = -10;
        float m_Duration = 0.5f;

        public virtual void Show(LOSimpleEvent callback)
        {
            var sequence = DOTween.Sequence();
            sequence
                .Append(m_CanvasGroup.DOFade(1, m_Duration))
                .Join(m_RectTransform.DOAnchorPosY(0, m_Duration))
                .AppendCallback(() =>
                {
                    m_CanvasGroup.SetInteract(true);
                    callback.Invoke();
                });
        }

        public virtual void Dismiss(LOSimpleEvent callback)
        {
            m_CanvasGroup.SetInteract(false);
            var sequence = DOTween.Sequence();
            sequence
                .Append(m_CanvasGroup.DOFade(0, m_Duration))
                .Join(m_RectTransform.DOAnchorPosY(m_OffsetY, m_Duration))
                .AppendCallback(() =>
                {
                    callback.Invoke();
                    GameObject.Destroy(this.gameObject);
                });
        }

        public virtual void Init()
        {
            m_RectTransform.SetAnchoredPosition(0, m_OffsetY);
        }
    }
}

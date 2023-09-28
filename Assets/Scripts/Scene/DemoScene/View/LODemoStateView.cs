using DG.Tweening;
using LO.Event;
using LO.Model;
using UnityEngine;
using UnityEngine.UI;
using LO.Utils;

namespace LO.View
{
    public interface ILODemoStateViewDataProvider
    {
        ILODemoModel DemoModel { get; }
    }

    public interface ILODemoStateViewEventDispatcher
    {
        event LOSimpleEvent OnTimeUpdate;
        event LOFloatEvent OnTaxCharged;
    }

    public class LODemoStateView : MonoBehaviour
    {
        [SerializeField]
        Text m_CoinText;

        [SerializeField]
        Text m_PeopleText;

        [SerializeField]
        LOTimelineView m_TimelineView;

        [SerializeField]
        Text m_TaxText;

        [SerializeField]
        RectTransform m_TaxRect;

        ILODemoStateViewDataProvider m_DataProvider;
        ILODemoStateViewEventDispatcher m_EventDispatcher;

        public void Init(
            ILODemoStateViewDataProvider dataProvider,
            ILODemoStateViewEventDispatcher eventDispatcher
        )
        {
            m_DataProvider = dataProvider;
            m_EventDispatcher = eventDispatcher;
            m_EventDispatcher.OnTimeUpdate += HandleTimeChange;
            m_EventDispatcher.OnTaxCharged += HandleTaxCharged;

            m_TimelineView.InitTimelineTax(m_DataProvider.DemoModel);
        }

        void UpdateUI()
        {
            var model = m_DataProvider.DemoModel;
            //m_CoinText.text = model.Coin.ToString();
            DoCoinAnimation(model.Coin);
            // m_PeopleText.text = model.People.ToString(); // TODO:

            m_TimelineView.UpdateUI(model);
        }

        void HandleTimeChange()
        {
            UpdateUI();
        }

        void HandleTaxCharged(float amount)
        {
            float duration = 0.3f;
            m_TaxText.text = $"-{amount}";

            m_TaxRect.SetAnchoredPositionX(0);

            var seq = DOTween.Sequence();
            seq.Append(m_TaxRect.DOAnchorPosX(10, duration))
                .Join(m_TaxText.DOFade(1, duration))
                .AppendInterval(1)
                .Append(m_TaxText.DOFade(0, duration));

            LOAudio.Instance.PlayGameTax();
        }

        void DoCoinAnimation(float endValue)
        {
            float startValue = float.Parse(m_CoinText.text);

            // Update the coin text to smoothly transition to the end value
            DOTween
                .To(
                    () => startValue,
                    x =>
                    {
                        m_CoinText.text = ((int)x).ToString();
                    },
                    endValue,
                    0.5f
                )
                .SetEase(Ease.Linear);
        }

        public void HandleBackBtnClick()
        {
            LOApplication.Instance.GoToMain();
        }
    }
}

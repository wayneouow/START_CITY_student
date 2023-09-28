using System.Collections;
using System.Collections.Generic;
using LO.Model;
using LO.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
    public class LOTimelineView : MonoBehaviour
    {
        [Header("time text")]
        [SerializeField]
        Text m_TimeText;

        [Header("timeline")]
        [SerializeField]
        RectTransform m_CountdownRect;

        [SerializeField]
        float m_TimelineWidth;

        [Header("tax")]
        [SerializeField]
        RectTransform m_TaxRoot;

        [SerializeField]
        GameObject m_TaxPrefab;

        public void InitTimelineTax(ILODemoModel model)
        {
            foreach (var tax in model.TaxSequence.TaxList)
            {
                var posX = LOMath.Map(tax.OccurrenceTime, model.EndTime, 0, m_TimelineWidth, 0);

                LOTimelineTaxView taxView = GameObject
                    .Instantiate(m_TaxPrefab, m_TaxRoot)
                    .GetComponent<LOTimelineTaxView>();
                taxView.UpdateView(posX, tax.TaxAmount);
            }
        }

        public void UpdateUI(ILODemoModel model)
        {
            var remainTime = model.EndTime - model.GameTime;
            m_TimeText.text = LOUtils.GetTimeStringFromSecond(remainTime);

            var newRectPosX = LOMath.Map(model.GameTime, model.EndTime, 0, m_TimelineWidth, 0);
            m_CountdownRect.SetAnchoredPositionX(newRectPosX);
        }
    }
}

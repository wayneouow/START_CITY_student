using System.Collections;
using System.Collections.Generic;
using LO.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
    public class LOTimelineTaxView : MonoBehaviour
    {
        [SerializeField]
        Text m_TaxText;

        [SerializeField]
        RectTransform m_Rect;

        public void UpdateView(float posX, float amount)
        {
            m_Rect.SetAnchoredPositionX(posX);
            m_TaxText.text = amount.ToString();
        }
    }
}

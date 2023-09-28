using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LO.Utils;

namespace LO.View
{
    public class LOCardDetailPairView : MonoBehaviour
    {
        [SerializeField]
        Text m_KeyText;

        [SerializeField]
        Text m_ValueText;

        [SerializeField]
        RectTransform m_RectTransform;

        public void UpdateText(string keyText, string valueText)
        {
            m_KeyText.text = keyText;
            m_ValueText.text = valueText;
        }

        public void UpdatePos(float offsetY)
        {
            m_RectTransform.SetAnchoredPositionY(offsetY);
        }
    }
}

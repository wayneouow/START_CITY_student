using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
    [System.Serializable]
    public struct CardColor
    {
        public Color CardUpColor;
        public Color CardDownColor;
    }

    [CreateAssetMenu(fileName = "card color meta", menuName = "城市遊戲/card color meta")]
    public class LOCardColorMeta : ScriptableObject
    {
        public List<CardColor> CardColors = new List<CardColor>();
    }
}

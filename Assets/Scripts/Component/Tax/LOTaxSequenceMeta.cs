using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
    [CreateAssetMenu(fileName = "tax-sequence-meta", menuName = "城市遊戲/稅收資料/稅收序列", order = 0)]
    public class LOTaxSequenceMeta : ScriptableObject
    {
        public List<LOTaxMeta> TaxList;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
    [CreateAssetMenu(fileName = "demo-info", menuName = "城市遊戲/遊戲基本資料V2")]
    public class LODemoMeta : ScriptableObject
    {
        public float Coin;
        public LOTaxSequenceMeta TaxSequenceMeta;
        public float EndTime;
        public LODisasterSequenceMeta DisasterSequenceMeta;
    }
}

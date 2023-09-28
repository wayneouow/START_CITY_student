using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta {

    [System.Serializable]
    public class LOCardProductionProperties {

        public float CoinPerSec;
        public float TotalVacancies; // 可以容納人的數量
    }

    [CreateAssetMenu(fileName = "生產卡牌", menuName = "城市遊戲/卡牌資料/生產卡牌")]
    public class LOProductionCardMeta : LOCardMetaBase {

        public LOCardProductionProperties[] ProductionProperties = new LOCardProductionProperties[3];

        public LOProductionCardMeta() {

            CardType = LOCardType.Production;
        }
    }
}
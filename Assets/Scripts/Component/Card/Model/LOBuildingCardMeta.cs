using UnityEngine;

namespace LO.Meta {

    [System.Serializable]
    public class LOCardBuildingProperties {

        public float PeoplePerSec;
        public float TotalPeople;
    }

    [CreateAssetMenu(fileName = "人口卡牌", menuName = "城市遊戲/卡牌資料/人口卡牌")]
    public class LOBuildingCardMeta : LOCardMetaBase {

        [Header("Building")]
        public LOCardBuildingProperties[] BuildingProperties = new LOCardBuildingProperties[3];

        public LOBuildingCardMeta() {

            CardType = LOCardType.Building;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta {

    [CreateAssetMenu(fileName = "card-book", menuName = "城市遊戲/卡牌資料/卡牌總列表")]
    public class LOCardBookMeta : ScriptableObject {

        public List<LOCardMetaBase> CardList = new List<LOCardMetaBase>();
    }
}
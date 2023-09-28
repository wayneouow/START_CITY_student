using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
    [CreateAssetMenu(fileName = "level-list-meta", menuName = "城市遊戲/關卡資料/關卡清單 meta")]
    public class LOGameLevelListMeta : ScriptableObject
    {
        public List<LOGameLevelMeta> LevelList = new List<LOGameLevelMeta>();
    }
}

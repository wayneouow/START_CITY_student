using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
    [CreateAssetMenu(fileName = "level-meta", menuName = "城市遊戲/關卡資料/關卡 meta")]
    public class LOGameLevelMeta : ScriptableObject
    {
        public string Identity;
        public bool Active;
        public string Name;
        public Sprite LevelImage;
        public string Desc;
        public string DisasterDesc;
        public float DifficultyStar;
    }
}

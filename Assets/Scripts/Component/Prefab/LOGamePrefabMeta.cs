using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
    [CreateAssetMenu(fileName = "game-prefab", menuName = "城市遊戲/Prefab/遊戲的Prefab")]
    public class LOGamePrefabMeta : ScriptableObject
    {
        public GameObject GameBlockPrefab;
        public GameObject GameCardPrefab;
        public GameObject GameBuildingPrefab;
    }
}

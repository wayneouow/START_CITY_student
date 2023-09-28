using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
    [CreateAssetMenu(fileName = "game-info", menuName = "城市遊戲/遊戲基本資料")]
    public class LOGameMeta : ScriptableObject
    {
        public float Coin;
    }
}

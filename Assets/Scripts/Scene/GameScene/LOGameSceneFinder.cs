using System.Collections;
using System.Collections.Generic;
using LO.View;
using LO.Meta;
using UnityEngine;

namespace LO
{
    public class LOGameSceneFinder : MonoBehaviour
    {
        public static LOGameSceneFinder Instance { get; private set; }

        private void Awake()
        {
            // init singleton to set this
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        [Header("Scene")]
        public LOGameSceneLevelView GameSceneLevelView;
        public LOGameSceneUIView GameSceneUIView;

        [Header("Meta")]
        public LOGamePrefabMeta PrefabMeta;
        public LOGameMeta GameMeta;
    }
}

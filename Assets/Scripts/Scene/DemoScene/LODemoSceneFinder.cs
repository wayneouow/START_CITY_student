using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.View;
using LO.Meta;

namespace LO
{
    public class LODemoSceneFinder : MonoBehaviour
    {
        public static LODemoSceneFinder Instance { get; private set; }

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
        public LOCardDetailView CardDetailView;
        public LODemoSceneCityView DemoSceneCityView;
        public LODemoSceneUIView DemoSceneUIView;

        [Header("Meta")]
        public LODemoPrefabMeta PrefabMeta;
        public LODemoMeta DemoMeta;
    }
}

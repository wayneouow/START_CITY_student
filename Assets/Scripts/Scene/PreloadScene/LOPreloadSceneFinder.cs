using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.View;

namespace LO {

    public class LOPreloadSceneFinder : MonoBehaviour {

        public static LOPreloadSceneFinder Instance { get; private set; }

        public LOPreloadSceneView PreloadSceneView;

        private void Awake() {
            // init singleton to set this
            if (Instance != null && Instance != this) {
                Destroy(this);
            } else {
                Instance = this;
            }
        }
    }
}
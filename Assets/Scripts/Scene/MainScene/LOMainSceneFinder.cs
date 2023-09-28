using LO.View;
using UnityEngine;

namespace LO
{
    public class LOMainSceneFinder : MonoBehaviour
    {
        public static LOMainSceneFinder Instance { get; private set; }

        public LOMainSceneView MainSceneView;

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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Controller;

namespace LO
{
    public class LOGameApplication : MonoBehaviour
    {
        #region singleton
        public static LOGameApplication Instance { get; private set; }

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
        #endregion

        public ILOGameSceneController GameSceneController;
        public LOGameController GameController
        {
            get => GameSceneController.GameController as LOGameController;
        }
        public LOGameSceneUIController GameSceneUIController
        {
            get => GameSceneController.GameSceneUIController as LOGameSceneUIController;
        }

        void Start()
        {
            GameSceneController = LOGameSceneController.Create();

            // TODO : refactor
            GameController.GameModel.OnGameDataLoaded += HandleGameDataLoaded;
            GameController.GameModel.Reload();
        }

        void HandleGameDataLoaded()
        {
            LOLogger.DI("LOGameData Reload User Card Meta", "complete");
            // NOTE : init the UI after load the model
            GameSceneUIController.Init();
        }
    }
}

using LO.Manager;
using LO.Controller;
using LO.Event;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LO
{
    public interface ILOApplication
    {
        event LOSimpleEvent ApplicationReady;

        // Property - manager
        ILOGameDataManager GameDataManager { get; }

        // Property - controller
        ILOPreloadSceneController PreloadSceneController { get; }
        ILOMainSceneController MainSceneController { get; }
        ILOGameSceneController GameSceneController { get; }

        // Method
        void Init();
        void GoToMain();
        void GoToGame();
    }

    public class LOApplication : MonoBehaviour, ILOApplication
    {
        public static LOApplication Instance { get; private set; }

        // Event
        public event LOSimpleEvent ApplicationReady = null;

        // Property - manager
        public ILOGameDataManager GameDataManager { get; private set; }

        // Property - controller
        public ILOPreloadSceneController PreloadSceneController { get; private set; }
        public ILOMainSceneController MainSceneController { get; private set; }
        public ILOGameSceneController GameSceneController { get; private set; }

        LOAppSceneState m_CurSceneSate;

        public void Init()
        {
            // init manager and reload
            GameDataManager = LOGameDataManager.Create();
            GameDataManager.Reload(HandleLoadCompleted);
            // TODO: fetch user data

            // TODO : other init
            SceneManager.sceneLoaded += OnSceneLoaded;

            ChangeSceneState(LOAppSceneState.Preload, false);
        }

        public void GoToMain()
        {
            ChangeSceneState(LOAppSceneState.Main);
        }

        public void GoToGame()
        {
            ChangeSceneState(LOAppSceneState.Game);
        }

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
                Init();
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void InitSceneController()
        {
            switch (m_CurSceneSate)
            {
                case LOAppSceneState.Preload:
                    PreloadSceneController = LOPreloadSceneController.Create();
                    break;
                case LOAppSceneState.Main:
                    MainSceneController = LOMainSceneController.Create();
                    break;
                case LOAppSceneState.Game:
                    // GameSceneController = LOGameSceneController.Create();
                    break;
                default:
                    break;
            }
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LOLogger.I($"{LOConst.GetSceneNameByState(m_CurSceneSate)} loaded");
            InitSceneController();
        }

        void ChangeSceneState(LOAppSceneState state, bool loadScene = true)
        {
            m_CurSceneSate = state;
            // reset controller, just load current scene controller
            PreloadSceneController = null;
            MainSceneController = null;
            GameSceneController = null;

            LOLogger.I($"ChangeSceneState to {state.ToString()}");

            if (loadScene)
            {
                SceneManager.LoadScene(LOConst.GetSceneNameByState(state));
            }
            else
            {
                InitSceneController();
            }
        }

        void HandleLoadCompleted()
        {
            // TODO : wait until data fetch done, but version 1 doesn't need to fetch
            ApplicationReady?.Invoke();
        }
    }
}

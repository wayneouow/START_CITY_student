using UnityEngine;
using LO.Controller;
using LO.Helper;
using UnityEngine.SceneManagement;

namespace LO
{
	public class LODemoApplication : MonoBehaviour
	{
		public static LODemoApplication Instance { get; private set; }

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

		public ILODemoSceneController DemoSceneController;
		public LODemoController DemoController
		{
			get => DemoSceneController.DemoController as LODemoController;
		}
		public LODemoSceneUIController DemoSceneUIController
		{
			get => DemoSceneController.DemoSceneUIController as LODemoSceneUIController;
		}

		public LOTimer Timer;

		void Start()
		{
			DemoSceneController = LODemoSceneController.Create();

			DemoController.DemoApplication = this;
			DemoController.DemoModel.OnDataLoaded += HandleDataLoaded;
			DemoController.DemoModel.Reload();
			Timer.DemoTimeEvent += DemoController.HandleTimeChange;
		}

		public void Restart()
		{
			Timer.Restart();
			SceneManager.LoadScene("demo-scene");
			LOLogger.DI("Game Restart!");
		}

		public void EndGame()
		{
			Timer.Pause();
			LOLogger.DI("Game Ended!");
		}

		void HandleDataLoaded()
		{
			LOLogger.DI("LODemoData Reload User Card Meta", "complete");
			DemoSceneController.Init();

			DemoController.StartGame();
		}
	}
}
using LO.Helper;
using LO.View;

namespace LO.Controller
{
	public interface ILODemoSceneController
	{
		ILODemoController DemoController { get; }
		ILODemoSceneUIController DemoSceneUIController { get; }
		void Init();
		bool AreAreasNeighbors(int pos1, int pos2);
	}

	public class LODemoSceneController : ILODemoSceneController
	{
		public static ILODemoSceneController Create()
		{
			return new LODemoSceneController();
		}

		LODemoSceneCityView m_LODemoSceneCityView;

		public ILODemoController DemoController { get; private set; }
		public ILODemoSceneUIController DemoSceneUIController { get; private set; }

		private LODemoSceneController()
		{
			m_LODemoSceneCityView = LODemoSceneFinder.Instance.DemoSceneCityView;

			DemoController = LODemoController.Create(this);
			DemoSceneUIController = LODemoSceneUIController.Create(DemoController);
			DemoSceneUIController.Init();
		}

		public void Init()
		{
			m_LODemoSceneCityView.Init(DemoController, DemoController);
		}

		public bool AreAreasNeighbors(int pos1, int pos2)
		{
			return m_LODemoSceneCityView.AreAreasNeighbors(pos1, pos2);
		}
	}
}
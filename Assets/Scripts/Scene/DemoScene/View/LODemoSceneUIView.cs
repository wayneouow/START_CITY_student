using UnityEngine;

namespace LO.View
{
	public interface ILODemoSceneUIViewDataProvider : ILODemoCardListViewDataProvider, ILODemoStateViewDataProvider
	{

	}

	public interface ILODemoSceneUIViewEventDispatcher : ILODemoCardListViewEventDispatcher, ILODemoStateViewEventDispatcher, ILODemoTestViewEventDispatcher
	{

	}

	public interface ILODemoSceneUIViewDelegate : ILODemoTestViewDelegate, ILODemoCardListViewDelegate, ILODialogViewDelegate, ILOBuildInfoViewDelegate
	{

	}

	public class LODemoSceneUIView : MonoBehaviour
	{
		public LODemoCardListView CardListView;
		public LODemoStateView StateView;
		public LODemoTestView TestView;
		public LODialogView DialogView;
		public LOBuildInfoView BuildingInfoView;

		public void Init(ILODemoSceneUIViewDataProvider dataProvider,
		ILODemoSceneUIViewEventDispatcher eventDispatcher,
		ILODemoSceneUIViewDelegate viewDelegate)
		{

			CardListView.Init(eventDispatcher, dataProvider, viewDelegate);
			TestView.Init(viewDelegate, eventDispatcher);
			StateView.Init(dataProvider, eventDispatcher);
			DialogView.Init(viewDelegate);
			BuildingInfoView.Init(viewDelegate);
		}
	}
}
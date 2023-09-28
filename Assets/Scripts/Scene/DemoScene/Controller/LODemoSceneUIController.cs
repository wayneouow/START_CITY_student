using LO.Event;
using LO.Model;
using LO.View;

namespace LO.Controller
{
	public interface ILODemoSceneUIController
		: ILODemoSceneUIViewDataProvider,
			ILODemoSceneUIViewEventDispatcher
	{
		void Init();
		void RemoveCard(LODemoCardView cardView);
		void Restart();
		void ShowBuildingInfo(ILODemoSceneAreaView areaView);
	}

	public class LODemoSceneUIController : ILODemoSceneUIController
	{
		public static ILODemoSceneUIController Create(ILODemoController baseController)
		{
			return new LODemoSceneUIController(baseController);
		}

		ILODemoController m_BaseController;
		LODemoSceneUIView m_DemoSceneUIView;

		public event LOBoolEvent OnCardDrew;
		public event LOSimpleEvent OnCardUsed;
		public event LOSimpleEvent OnTimeUpdate;
		public event LOSimpleEvent OnBankrupt;
		public event LOFloatEvent OnTaxCharged;
		public event LOSimpleEvent OnEndGame;

		public ILODemoModel DemoModel
		{
			get => m_BaseController.DemoModel;
		}

		public LOGameCardModel CurrentCardModel => DemoModel.CurrentCardModel;

		private LODemoSceneUIController(ILODemoController baseController)
		{
			m_BaseController = baseController;
			m_BaseController.DemoSceneUIController = this;
			m_DemoSceneUIView = LODemoSceneFinder.Instance.DemoSceneUIView;
		}

		public void Init()
		{
			m_DemoSceneUIView.Init(this, this, m_BaseController);

			DemoModel.OnCardDrew += this.OnCardDrew;
			DemoModel.OnCardUsed += this.OnCardUsed;
			DemoModel.OnTimeUpdate += this.OnTimeUpdate;
			DemoModel.OnBankrupt += this.OnBankrupt;
			DemoModel.OnTaxCharged += this.OnTaxCharged;
			DemoModel.OnEndGame += this.OnEndGame;
		}

		public void RemoveCard(LODemoCardView cardView)
		{
			m_DemoSceneUIView.CardListView.RemoveCard(cardView);
		}

		public void Restart()
		{
			m_DemoSceneUIView.CardListView.Restart();
		}

		public void ShowConfirmDialog(string title, string desc)
		{
			m_DemoSceneUIView.DialogView.ShowConfirmDialog(title, desc);
		}

		public void HideDialog()
		{
			m_DemoSceneUIView.DialogView.HideDialog();
		}

		public void ShowBuildingInfo(ILODemoSceneAreaView areaView)
		{
			if (areaView?.BuildingView?.BuildingModel == null)
			{
				// TODO: show info about empty field
				m_DemoSceneUIView.BuildingInfoView.HideBuildingInfo();
				return;
			}
			m_DemoSceneUIView.BuildingInfoView.ShowBuildingInfo(areaView.BuildingView.BuildingModel);
		}

		public void HideBuildingInfo()
		{
			m_DemoSceneUIView.BuildingInfoView.HideBuildingInfo();
		}

		public void ShowMoveOutUI(LOBuildingModelBase buildingModel)
		{
			m_DemoSceneUIView.BuildingInfoView.ShowMoveOutUI(buildingModel);
		}

		public void ShowMoveInUI(LOBuildingModelBase buildingModel)
		{
			m_DemoSceneUIView.BuildingInfoView.ShowMoveInUI(buildingModel);
		}
	}
}

using System.Linq;
using LO.Meta;
using LO.Model;
using LO.View;
using UnityEngine;

namespace LO.Helper
{
	public interface ILOBuildingActionHelperDelegate : ILOBuildingModelBaseDataProvider
	{
		ILODemoModel DemoModel { get; }
		LODemoCardView SelectedCardView { get; }
		ILODemoSceneAreaView SelectedAreaView { get; }

		bool AreAreasNeighbors(int actionAreaPositionIndex, int actionTargetAreaPositionIndex);
		void UseCard(LODemoCardView cardView, float cost);
	}

	public interface ILOBuildingActionHelper
	{
		bool CanDoAction(ILODemoSceneAreaView actionArea, ILODemoSceneAreaView actionTargetArea);
		void DoBuildingAction(ILODemoSceneAreaView actionArea, ILODemoSceneAreaView actionTargetArea);
		void AddCardToArea(LODemoCardView cardView, ILODemoSceneAreaView areaView);
	}

	public class LOBuildingActionHelper : ILOBuildingActionHelper
	{
		protected ILOBuildingActionHelperDelegate m_Delegate;

		public static ILOBuildingActionHelper Create(ILOBuildingActionHelperDelegate helperDelegate)
		{
			return new LOBuildingActionHelper(helperDelegate);
		}

		protected LOBuildingActionHelper(ILOBuildingActionHelperDelegate helperDelegate)
		{
			m_Delegate = helperDelegate;
		}

		public bool CanDoAction(ILODemoSceneAreaView actionArea, ILODemoSceneAreaView actionTargetArea)
		{
			if (actionArea == null || actionArea.BuildingView == null) { return false; }
			if (actionTargetArea == null || actionTargetArea.BuildingView == null) { return false; }
			if (actionArea.BuildingView.BuildingModel.BuildingDestroyed) { return false; }

			// TODO: add expansion buildings, including fire station and hospitals
			switch (actionArea.BuildingView.BuildingModel.CardType)
			{
				case LOCardType.Building:
					switch (actionTargetArea.BuildingView.BuildingModel.CardType)
					{
						case LOCardType.Production:

							if (!m_Delegate.AreAreasNeighbors(actionArea.PositionIndex, actionTargetArea.PositionIndex)) { return false; }

							LOProductionModel targetProductionBuilding = actionTargetArea.BuildingView.BuildingModel as LOProductionModel;

							if (!targetProductionBuilding.IsBuildDone) { return false; }
							if (targetProductionBuilding.BuildingDestroyed) { return false; }
							if (targetProductionBuilding.GetEmptyVacancies() == 0) { return false; }

							return true;
						default:
							return false;
					}
				default:
					return false;
			}
		}

		public void DoBuildingAction(ILODemoSceneAreaView actionArea, ILODemoSceneAreaView actionTargetArea)
		{
			// TODO: add expansion buildings, including fire station and hospitals
			switch (actionArea.BuildingView.BuildingModel.CardType)
			{
				case LOCardType.Building:
					switch (actionTargetArea.BuildingView.BuildingModel.CardType)
					{
						case LOCardType.Production:
							AssignFreePeopleToProduction(actionArea, actionTargetArea);
							break;
						default:
							break;
					}
					break;
				default:
					break;
			}
		}

		void AssignFreePeopleToProduction(ILODemoSceneAreaView actionArea, ILODemoSceneAreaView actionTargetArea)
		{
			var actionBuildingView = actionArea.BuildingView;
			var targetBuildingView = actionTargetArea.BuildingView;
			LOBuildingModel actionBuildingModel = actionArea.BuildingView.BuildingModel as LOBuildingModel;
			LOProductionModel targetBuildingModel = actionTargetArea.BuildingView.BuildingModel as LOProductionModel;

			actionBuildingModel.AssignFreePeopleToProduction(targetBuildingModel);

			// TODO: do animation to building views
		}

		public void AddCardToArea(LODemoCardView cardView, ILODemoSceneAreaView areaView)
		{
			var cardType = cardView.CardModel.CardMeta.CardType;

			if (cardType == LOCardType.Building || cardType == LOCardType.Production || cardType == LOCardType.Hospital)
			{
				var buildingModel = LOBuildingModelBase.CreateModel(
					m_Delegate.SelectedCardView.CardModel,
					m_Delegate
				);

				if (areaView.BuildingView != null)
				{
					// TODO : refactor the same card check
					if (
						areaView.BuildingView.BuildingModel.CardModel.CardMeta.Identity
						== cardView.CardModel.CardMeta.Identity
					)
					{
						if (areaView.BuildingView.CanUpgrade())
						{
							areaView.BuildingView.UpgradeLevel();
							m_Delegate.UseCard(cardView, buildingModel.CurrentBasicProperties.Cost);
						}
					}
				}
				else
				{
					if (Build(buildingModel, areaView))
					{
						if (cardType == LOCardType.Hospital)
						{
							((ILOHospitalModel)buildingModel).SetNeighbors(areaView.Neighbors3x3.Cast<ILOHealable>().ToList());
						}
						m_Delegate.UseCard(cardView, buildingModel.CurrentBasicProperties.Cost);
					}
				}
			}

			if (cardType == LOCardType.Functional)
			{
				var functionalModel = LOFunctionalModelBase.CreateModel(
					cardView.CardModel.CardMeta as LOFunctionalCardMeta
				);

				if (functionalModel.DoEffect(areaView))
				{
					m_Delegate.UseCard(cardView, functionalModel.CardMeta.BasicProperties[0].Cost);
				}
			}
		}

		bool Build(LOBuildingModelBase buildingModel, ILODemoSceneAreaView areaView)
		{
			if (!CheckCanBuild(buildingModel))
			{
				return false;
			}

			m_Delegate.DemoModel.OnTimeUpdate += buildingModel.HandleTimeChange;
			m_Delegate.SelectedAreaView.Build(buildingModel);
			return true;
		}

		bool CheckCanBuild(LOBuildingModelBase buildingModel)
		{
			var cost = buildingModel.CurrentBasicProperties.Cost;
			return m_Delegate.DemoModel.Coin >= cost;
		}
	}
}
using LO.Event;
using LO.Helper;
using LO.Meta;
using LO.Model;
using LO.View;

namespace LO.Controller
{
    public interface ILODemoController
        : ILODemoSceneUIViewDelegate,
            ILODemoSceneCityViewDelegate,
            ILODemoSceneCityViewEventDispatcher,
            ILOBuildingModelBaseDataProvider
    {
        void StartGame();
        ILODemoModel DemoModel { get; }
        LODemoSceneUIController DemoSceneUIController { get; set; }

        void HandleTimeChange(float newTime);
    }

    public class LODemoController : ILODemoController, ILOBuildingActionHelperDelegate
    {
        public static ILODemoController Create(ILODemoSceneController demoSceneController)
        {
            return new LODemoController(demoSceneController);
        }

        public ILODemoModel DemoModel { get; private set; }

        LODemoSceneCityView m_SceneCityView;
        LODemoSceneUIView m_GameSceneUIView;
        LOCardDetailView m_CardDetailView;
        ILODemoSceneAreaView m_SelectedAreaView;
        ILODemoSceneAreaView m_ActionTargetAreaView;
        LODemoCardView m_SelectedCardView;
        ILODemoSceneAreaView ILOBuildingActionHelperDelegate.SelectedAreaView
        {
            get => m_SelectedAreaView;
        }
        LODemoCardView ILOBuildingActionHelperDelegate.SelectedCardView
        {
            get => m_SelectedCardView;
        }

        public event LODisasterEvent OnDisasterOccur;

        public LODemoSceneUIController DemoSceneUIController { get; set; }
        public LODemoApplication DemoApplication { get; set; }

        public float GameTime => DemoModel.GameTime;

        ILODemoSceneController m_DemoSceneController;
        ILOBuildingActionHelper m_BuildingActionHelper;

        private LODemoController(ILODemoSceneController demoSceneController)
        {
            m_DemoSceneController = demoSceneController;
            m_BuildingActionHelper = LOBuildingActionHelper.Create(this);

            m_SceneCityView = LODemoSceneFinder.Instance.DemoSceneCityView;
            m_GameSceneUIView = LODemoSceneFinder.Instance.DemoSceneUIView;
            m_CardDetailView = LODemoSceneFinder.Instance.CardDetailView;

            DemoModel = LODemoModel.Create(LODemoSceneFinder.Instance.DemoMeta);
            DemoModel.OnDisasterOccur += HandleDisasterOccur;

            StartGame();
        }

        public void StartGame()
        {
            for (var i = 0; i < 4; i++)
            {
                DemoModel.DrawCard(false);
            }
        }

        void UseCard(LODemoCardView cardView, float cost)
        {
            DemoModel.AddCoin(-cost);
            DemoModel.UseCard();
            DemoSceneUIController.RemoveCard(cardView);
            DrawCard();
        }

        void DrawCard()
        {
            DemoModel.DrawCard(true);
        }

        bool CheckCanBuild(LOBuildingModelBase buildingModel)
        {
            var cost = buildingModel.CurrentBasicProperties.Cost;
            return DemoModel.Coin >= cost;
        }

        void UnSelectArea()
        {
            m_SelectedAreaView?.HandleUnSelected();
            m_SelectedAreaView = null;
            m_ActionTargetAreaView?.HandleUnSelected();
            m_ActionTargetAreaView = null;
        }

        void UnSelectCard()
        {
            m_SelectedCardView?.HandleUnSelected();
            m_SelectedCardView = null;
            HideDetailView();
        }

        void UnSelectAll()
        {
            UnSelectArea();
            UnSelectCard();
            DemoSceneUIController.HideBuildingInfo();
        }

        void StartBuildingAction()
        {
            if (DemoModel.DemoState != LODemoState.BLOCK_SELECT)
            {
                return;
            }

            UpdateDemoState(LODemoState.BUILDING_ACTION);
        }

        void DoBuildingAction()
        {
            m_BuildingActionHelper.DoBuildingAction(m_SelectedAreaView, m_ActionTargetAreaView);

            UpdateDemoState(LODemoState.DEFAULT);
        }

        void SelectArea(ILODemoSceneAreaView areaView)
        {
            if (
                DemoModel.DemoState == LODemoState.EndGame
                || DemoModel.DemoState == LODemoState.BUILDING_ACTION_CONFIRM
            )
            {
                return;
            }

            if (areaView == m_SelectedAreaView)
            {
                UnSelectArea();
                UpdateDemoState(LODemoState.DEFAULT);
            }
            else if (DemoModel.DemoState == LODemoState.BUILDING_ACTION)
            {
                if (m_BuildingActionHelper.CanDoAction(m_SelectedAreaView, areaView))
                {
                    m_ActionTargetAreaView = areaView;
                    DemoSceneUIController.ShowMoveInUI(
                        m_ActionTargetAreaView.BuildingView.BuildingModel
                    );

                    UpdateDemoState(LODemoState.BUILDING_ACTION_CONFIRM);
                    HideDetailView();
                }
                else
                {
                    // TODO: show hint
                }
            }
            else
            {
                m_SelectedAreaView?.HandleUnSelected();
                m_SelectedAreaView = areaView;
                m_SelectedAreaView.HandleSelected();

                UpdateDemoState(LODemoState.BLOCK_SELECT);
            }
        }

        void HideDetailView()
        {
            m_CardDetailView.gameObject.SetActive(false);
        }

        void ShowDetailView(LOGameCardModel cardModel)
        {
            m_CardDetailView.UpdateView(cardModel);
            m_CardDetailView.gameObject.SetActive(true);
        }

        void SelectCard(LODemoCardView cardView)
        {
            if (DemoModel.DemoState == LODemoState.EndGame)
            {
                return;
            }

            if (cardView == m_SelectedCardView)
            {
                UnSelectCard();
                UpdateDemoState(LODemoState.DEFAULT);

                HideDetailView();
            }
            else
            {
                m_SelectedCardView?.HandleUnSelected();
                m_SelectedCardView = cardView;
                m_SelectedCardView.HandleSelected();

                ShowDetailView(m_SelectedCardView.CardModel);

                UpdateDemoState(LODemoState.CARD_SELECT);
            }
        }

        public void UpdateDemoState(LODemoState newState)
        {
            var nowState = DemoModel.DemoState;

            switch (nowState)
            {
                case LODemoState.DEFAULT:
                    if (newState == LODemoState.BLOCK_SELECT)
                    {
                        DemoSceneUIController.ShowBuildingInfo(m_SelectedAreaView);
                    }
                    break;
                case LODemoState.BLOCK_SELECT:
                    if (newState == LODemoState.CARD_SELECT)
                    {
                        UnSelectArea();
                    }
                    else if (newState == LODemoState.BUILDING_ACTION)
                    {
                        DemoSceneUIController.ShowMoveOutUI(
                            m_SelectedAreaView.BuildingView.BuildingModel
                        );
                    }
                    else if (newState == LODemoState.BLOCK_SELECT)
                    {
                        DemoSceneUIController.ShowBuildingInfo(m_SelectedAreaView);
                    }
                    break;
                case LODemoState.CARD_SELECT:
                    if (newState == LODemoState.BLOCK_SELECT)
                    {
                        m_BuildingActionHelper.AddCardToArea(
                            m_SelectedCardView,
                            m_SelectedAreaView
                        );
                        UnSelectAll();
                        newState = LODemoState.DEFAULT;
                    }
                    break;
                case LODemoState.EndGame:
                    DemoApplication.EndGame();

                    break;
                default:
                    break;
            }

            if (newState == LODemoState.DEFAULT)
            {
                UnSelectAll();
            }

            DemoModel.UpdateDemoState(newState);
        }

        public void HandleTimeChange(float newTime)
        {
            DemoModel.SetGameTime(newTime);
            if (DemoModel.CheckEndGame())
            {
                UpdateDemoState(LODemoState.EndGame);
            }
        }

        public void HandleDisasterOccur(int position, float damage)
        {
            OnDisasterOccur?.Invoke(position, damage);
        }

        void Restart()
        {
            DemoApplication.Restart();
            DemoModel.Restart();
            DemoSceneUIController.Restart();
        }

        void ILODemoTestViewDelegate.OnDrawBtnClick()
        {
            DrawCard();
        }

        void ILODemoTestViewDelegate.OnRestartBtnClick()
        {
            Restart();
        }

        void ILODemoCardViewDelegate.OnCardClick(LODemoCardView cardView)
        {
            SelectCard(cardView);
        }

        void ILODemoCardViewDelegate.OnDeleteClick(LODemoCardView cardView)
        {
            m_SelectedCardView = cardView;
            var cost = cardView.CardModel.CardMeta.BasicProperties[0].Cost / 2;
            if (DemoModel.Coin >= cost)
            {
                DemoSceneUIController.ShowConfirmDialog("丟棄卡片", $"是否花費 {cost} 金幣來丟棄卡片，並抽出下一張？");
                UpdateDemoState(LODemoState.CONFIRM_DISCARD_CARD);
            }
            else
            {
                DemoSceneUIController.ShowConfirmDialog("丟棄卡片", $"需要花費 {cost} 金幣，餘額不足。");
                UpdateDemoState(LODemoState.CONFIRM_COIN_NOT_ENOUGH);
            }
        }

        void ILODemoSceneAreaViewDelegate.SelectArea(ILODemoSceneAreaView areaView)
        {
            SelectArea(areaView);
        }

        void ILODemoSceneAreaViewDelegate.HandelProductionEvent(ILODemoSceneAreaView areaView)
        {
            ILOProductionModel productionModel =
                areaView.BuildingView.BuildingModel as ILOProductionModel;
            lock (productionModel)
            {
                int consumeCoinsAmount = (int)productionModel.WaitCoin;
                if (consumeCoinsAmount > 0)
                {
                    DemoModel.AddCoin(consumeCoinsAmount);
                    productionModel.ConsumeCoins(consumeCoinsAmount);
                    areaView.BuildingView.ShowCreation();
                }
            }
        }

        bool ILOBuildingActionHelperDelegate.AreAreasNeighbors(
            int actionAreaPositionIndex,
            int passiveAreaPositionIndex
        )
        {
            return m_DemoSceneController.AreAreasNeighbors(
                actionAreaPositionIndex,
                passiveAreaPositionIndex
            );
        }

        void ILODialogViewDelegate.OnCloseBtnClick()
        {
            DemoSceneUIController.HideDialog();
            UpdateDemoState(LODemoState.DEFAULT);
        }

        void ILODialogViewDelegate.OnConfirmBtnClick()
        {
            switch (DemoModel.DemoState)
            {
                case LODemoState.CONFIRM_DISCARD_CARD:
                    var cost = m_SelectedCardView.CardModel.CardMeta.BasicProperties[0].Cost / 2;
                    DemoSceneUIController.RemoveCard(m_SelectedCardView);
                    DemoModel.AddCoin(-cost);
                    DemoModel.DrawCard(true);
                    break;
                default:
                    break;
            }
            DemoSceneUIController.HideDialog();
            UpdateDemoState(LODemoState.DEFAULT);
        }

        void ILOBuildInfoViewDelegate.OnActionButtonClick()
        {
            switch (DemoModel.DemoState)
            {
                case LODemoState.BLOCK_SELECT:
                    StartBuildingAction();
                    break;
                case LODemoState.BUILDING_ACTION_CONFIRM:
                    DoBuildingAction();
                    break;
                default:
                    break;
            }
        }

        void ILOBuildInfoViewDelegate.OnCancelButtonClick()
        {
            UpdateDemoState(LODemoState.DEFAULT);
        }

        void ILOBuildingActionHelperDelegate.UseCard(LO.View.LODemoCardView cardView, float cost)
        {
            this.UseCard(cardView, cost);
        }
    }
}

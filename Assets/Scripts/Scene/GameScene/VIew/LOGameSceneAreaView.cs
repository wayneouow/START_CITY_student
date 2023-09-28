using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LO.Meta;
using LO.Model;
using LO.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

// Todo : refactor this view controller
namespace LO.View
{
    public class LOGameSceneAreaView : MonoBehaviour
    {
        [SerializeField]
        Transform m_Transform;

        [SerializeField]
        GameObject m_ActiveBorder;

        [Header("顏色變化")]
        [SerializeField]
        SpriteRenderer m_SpriteRenderer;

        [SerializeField]
        Color m_DefaultColor;

        [SerializeField]
        Color m_HoverColor;

        [SerializeField]
        public LOGameBuildingView BuildingView;

        ILOGameAreaModel m_Model;

        private void Start()
        {
            m_DefaultColor = m_SpriteRenderer.color;
        }

        public void Init(ILOGameAreaModel model, Vector2 pos)
        {
            m_Model = model;
            m_Transform.SetLocalPosition(pos);
        }

        void HandleHoverEffect()
        {
            m_SpriteRenderer.color = m_HoverColor;
        }

        void HandleEndHoverEffect()
        {
            m_SpriteRenderer.color = m_DefaultColor;
        }

        public void HandleSelected()
        {
            m_ActiveBorder.SetActive(true);

            // NOTE : log
            LOGameApplication.Instance.GameSceneController.GameSceneUIController.LogBuilding(this);
        }

        public void HandleUnSelected()
        {
            m_ActiveBorder.SetActive(false);

            // NOTE : clear log
            LOGameApplication.Instance.GameSceneController.GameSceneUIController.LogBuilding(null);
        }

        public void HandleCardSelectedToArea(LOGameCardView selectedCardView)
        {
            // 當前沒有建築的話，如果是建築類的，判斷是否有足夠的金幣，有的話就產生
            if (BuildingView == null)
            {
                var cardType = selectedCardView.CardModel.CardMeta.CardType;
                if (cardType == LOCardType.Building || cardType == LOCardType.Production)
                {
                    var buildingModel = LOGameBuildingModelBase.CreateModel(
                        selectedCardView.CardModel,
                        LOGameApplication.Instance.GameController.GameModel
                    );
                    var cost = buildingModel.CurrentBasicProperties.Cost;
                    if (LOGameApplication.Instance.GameController.CheckCanBuild(cost))
                    {
                        var buildingView = GameObject
                            .Instantiate(
                                LOGameSceneFinder.Instance.PrefabMeta.GameBuildingPrefab,
                                m_Transform
                            )
                            .GetComponent<LOGameBuildingView>();

                        buildingView.Init(buildingModel);

                        LOGameApplication.Instance.GameController.AddCoin(-cost);
                        selectedCardView.RemoveCard();

                        BuildingView = buildingView;
                    }
                }
            }
        }

        public void HandleDisasterToArea()
        {
            if (BuildingView == null)
                return;

            var disasterValue = LOGameApplication.Instance.GameController.GameModel.DisasterValue;

            BuildingView.HandleDisasterToBuilding(disasterValue);
        }

        public void HandleGameTimeChange(float gameTime)
        {
            BuildingView?.HandleGameTimeChange(gameTime);
        }

        #region UNITY METHOD
        private void OnMouseEnter()
        {
            // 阻擋 UI 點擊事件
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                HandleHoverEffect();
            }
        }

        private void OnMouseExit()
        {
            // NOTE : 滑鼠離開不用判斷，避免沒有觸發離開 effect
            HandleEndHoverEffect();
        }

        private void OnMouseDown()
        {
            // 阻擋 UI 點擊事件
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                LOGameApplication.Instance.GameController.SelectArea(this);
            }
        }
        #endregion
    }
}

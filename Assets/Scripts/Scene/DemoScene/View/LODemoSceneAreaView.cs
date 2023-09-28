using System.Collections.Generic;
using LO.Meta;
using LO.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LO.View
{
	public interface ILODemoSceneAreaViewDelegate
	{
		void SelectArea(ILODemoSceneAreaView areaView);
		void HandelProductionEvent(ILODemoSceneAreaView areaView);
	}

	public interface ILODemoSceneAreaView : ILOHealable
	{
		int PositionIndex { get; set; }
		void HandleUnSelected();
		void HandleSelected();

		void Build(LOBuildingModelBase buildingModel);
		void DoDamage(float damage);
		void DoRepair(float amount);
		void DoDemolish();
		bool DoEquip(LOEquipmentModelBase equipment);

		LODemoBuildingView BuildingView { get; }

		List<ILODemoSceneAreaView> Neighbors3x3 { get; set; }
	}

	public class LODemoSceneAreaView
		: MonoBehaviour,
			ILODemoSceneAreaView,
			ILODemoBuildingViewDelegate,
			IPointerEnterHandler,
			IPointerExitHandler,
			IPointerClickHandler
	{
		public ILODemoSceneAreaViewDelegate ViewDelegate;

		[SerializeField]
		RectTransform m_RectTransform;

		[SerializeField]
		Image m_ActiveBorder;

		[Header("動畫")]
		[SerializeField]
		Animation m_Animation;

		[Header("顏色變化")]
		[SerializeField]
		Image m_Image;

		[SerializeField]
		Color m_DefaultColor;

		[SerializeField]
		Color m_HoverColor;

		[SerializeField]
		public LODemoBuildingView BuildingView { get; private set; } // TODO: change this

		public int PositionIndex { get; set; }

		public List<ILODemoSceneAreaView> Neighbors3x3 { get; set; }

		public bool HasInjuredPeople { get => BuildingView?.BuildingModel.HasInjuredPeople ?? false; }

		void Start()
		{
			m_DefaultColor = m_Image.color;
		}

		void HandleHoverEffect()
		{
			m_Image.color = m_HoverColor;
		}

		void HandleEndHoverEffect()
		{
			m_Image.color = m_DefaultColor;
		}

		public void HandleSelected()
		{
			m_ActiveBorder.color = m_HoverColor;
		}

		public void HandleUnSelected()
		{
			m_ActiveBorder.color = Color.white;
		}

		public void Build(LOBuildingModelBase buildingModel)
		{
			BuildingView = GameObject
				.Instantiate(LODemoSceneFinder.Instance.PrefabMeta.DemoBuildingPrefab, m_RectTransform)
				.GetComponent<LODemoBuildingView>();

			buildingModel.OnCreateEvent += HandleCreateEvent;

			BuildingView.Init(buildingModel, this);
		}

		public void DoDamage(float damage)
		{
			DoShakingAnim();
			BuildingView?.BuildingModel?.DoDamage(damage);
		}

		public void DoRepair(float amount)
		{
			// NOTE : refactor !?
			BuildingView?.BuildingModel?.DoDamage(-amount);
		}

		public void DoDemolish()
		{
			// TODO : refactor
			BuildingView.BuildingModel.OnCreateEvent -= HandleCreateEvent;
			GameObject.Destroy(BuildingView.gameObject);
			BuildingView = null;
		}

		public bool DoEquip(LOEquipmentModelBase equipment)
		{
			return BuildingView.BuildingModel?.DoEquip(equipment) ?? false;
		}

		void DoShakingAnim()
		{
			m_Animation.Play();
		}

		public void HandleCardSelectedToArea(LODemoCardView selectedCardView)
		{
			if (BuildingView != null)
			{
				return;
			}
		}

		void HandleCreateEvent()
		{
			switch (BuildingView.BuildingModel.CardType)
			{
				case LOCardType.Production:
					ViewDelegate?.HandelProductionEvent(this);
					break;
				default:
					break;
			}
		}

		#region  UNITY METHOD
		private void OnMouseEnter()
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}

			HandleHoverEffect();
		}

		private void OnMouseExit()
		{
			HandleEndHoverEffect();
		}

		private void OnMouseDown()
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}

			ViewDelegate.SelectArea(this);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			HandleHoverEffect();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			HandleEndHoverEffect();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			ViewDelegate.SelectArea(this);
		}

		public void DoHealingPeoples(int num)
		{
			BuildingView.BuildingModel.DoHealingPeoples(num);
		}

		#endregion
	}
}

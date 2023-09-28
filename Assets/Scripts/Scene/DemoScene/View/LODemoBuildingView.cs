using System.Collections.Generic;
using LO.Meta;
using LO.Model;
using LO.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
	public interface ILODemoBuildingViewDelegate { }

	public class LODemoBuildingView : MonoBehaviour
	{
		[SerializeField]
		TextMeshProUGUI m_BuildDurationText;

		[SerializeField]
		Image m_HPImage;

		[SerializeField]
		Image m_BuildingImage;

		[Header("產生物件，例如金幣、人口")]
		[SerializeField]
		Transform m_CreationRoot;
		[SerializeField]
		Transform m_CitizenRoot;
		[SerializeField]
		GameObject m_CitizenObj;

		[SerializeField]
		GameObject m_CreationPrefab;

		[SerializeField]
		LOEquipmentView m_EquipmentView;

		public LOBuildingModelBase BuildingModel;
		public ILODemoBuildingViewDelegate ViewDelegate;

		List<LOCitizenView> m_CitizenViewList;

		private readonly object m_SyncRoot = new object();

		public void Init(
			LOBuildingModelBase buildingModel,
			ILODemoBuildingViewDelegate viewDelegate
		)
		{
			BuildingModel = buildingModel;
			ViewDelegate = viewDelegate;

			BuildingModel.OnConstructionProgress += HandleConstructionProgress;
			BuildingModel.OnConstructionComplete += HandleConstructionComplete;
			BuildingModel.OnBuildingDestroyed += HandleBuildingDestroyed;
			BuildingModel.OnCitizenHealed += HandleCitizenHealed;
			BuildingModel.OnCitizenUpdate += HandleCitizenUpdate;
			BuildingModel.OnItemEquipped += HandleItemEquipped;
			BuildingModel.OnHpUpdate += HandleHpUpdate;

			m_CitizenViewList = new List<LOCitizenView>() { m_CitizenObj.GetComponent<LOCitizenView>() };

			UpdateView();
			UpdateConstruction();
			UpdateHP();
		}

		void OnDestroy()
		{
			BuildingModel.OnConstructionProgress -= HandleConstructionProgress;
			BuildingModel.OnConstructionComplete -= HandleConstructionComplete;
			BuildingModel.OnBuildingDestroyed -= HandleBuildingDestroyed;
			BuildingModel.OnCitizenUpdate -= HandleCitizenUpdate;
			BuildingModel.OnHpUpdate -= HandleHpUpdate;
		}

		public void ShowCreation()
		{
			switch (BuildingModel.CardType)
			{
				case LOCardType.Production:
					var g = GameObject.Instantiate(m_CreationPrefab, m_CreationRoot);
					var rt = g.GetComponent<RectTransform>();
					LOCreationView creationView = g.GetComponent<LOCreationView>();
					creationView.SetImage(BuildingModel.CardType);
					break;
			}
		}

		void UpdateView()
		{
			m_BuildingImage.sprite = BuildingModel.CurrentBuildingImage;
		}

		void UpdateCitizen()
		{
			lock (m_SyncRoot)
			{
				var citizens = BuildingModel.DisplayCitizens;
				var count = citizens.Count;
				for (var i = m_CitizenViewList.Count; i < count; i++)
				{
					m_CitizenViewList.Add(GameObject.Instantiate(m_CitizenObj, m_CitizenRoot).GetComponent<LOCitizenView>());
				}
				for (var i = 0; i < m_CitizenViewList.Count; i++)
				{
					m_CitizenViewList[i].SetActive(i < count);

					if (i < count)
					{
						if (m_CitizenViewList[i].IsInjured && !citizens[i].IsInjured)
						{
							m_CitizenViewList[i].DoHealingAnim();
						}
						else if (!m_CitizenViewList[i].IsInjured && citizens[i].IsInjured)
						{
							m_CitizenViewList[i].DoInjuredAnim();
						}
					}
				}
			}
		}

		void UpdateEquipment()
		{
			m_EquipmentView.SetEquipments(BuildingModel.Equipments);
		}

		void UpdateConstruction()
		{
			var isBuildDone = BuildingModel.IsBuildDone;

			if (isBuildDone)
			{
				m_BuildDurationText.text = "";
				m_BuildingImage.color = Color.white;
			}
			else
			{
				m_BuildDurationText.text = BuildingModel.BuildingTime.ToString("F0");
				m_BuildingImage.color = Color.gray;
			}
		}

		void UpdateHP()
		{
			m_HPImage.fillAmount = BuildingModel.HPRatio;
		}

		void HandleConstructionProgress()
		{
			UpdateConstruction();
		}

		void HandleConstructionComplete()
		{
			UpdateConstruction();
		}

		void HandleBuildingDestroyed()
		{
			m_BuildingImage.sprite = BuildingModel.DestroyedImage;
		}

		void HandleHpUpdate()
		{
			UpdateHP();
		}

		void HandleCitizenUpdate()
		{
			UpdateCitizen();
		}

		void HandleCitizenHealed()
		{
			UpdateCitizen();
		}

		void HandleItemEquipped()
		{
			UpdateEquipment();
		}

		public bool CanUpgrade()
		{
			return BuildingModel.CanUpgrade();
		}

		public void UpgradeLevel()
		{
			BuildingModel.UpgradeLevel();
			// TODO : add upgrade animation
			UpdateView();
			UpdateHP();
			Debug.Log("UpgradeLevel done");
		}
	}
}

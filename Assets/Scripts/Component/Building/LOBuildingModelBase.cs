using System.Collections.Generic;
using LO.Event;
using LO.Meta;
using UnityEngine;

namespace LO.Model
{
	public interface ILOBuildingModelBaseDataProvider
	{
		float GameTime { get; }
	}

	public interface ILOBuildingModelBase : ILOHealable
	{
		bool BuildingDestroyed { get; }
		bool DoEquip(ILOEquipmentModelBase equipmentModelBase);
	}

	public class LOBuildingModelBase : ILOBuildingModelBase
	{
		public event LOSimpleEvent OnHpUpdate;
		public event LOSimpleEvent OnBuildingDestroyed;
		public event LOSimpleEvent OnCreateEvent;
		public event LOSimpleEvent OnConstructionProgress;
		public event LOSimpleEvent OnConstructionComplete;
		public event LOSimpleEvent OnCitizenUpdate;
		public event LOSimpleEvent OnItemEquipped;
		public event LOSimpleEvent OnCitizenHealed;

		public LOGameCardModel CardModel;

		public List<ILOEquipmentModelBase> Equipments;

		public int CurrentLevel;
		const int MAX_LEVEL = 1; // TODO : refactor
		public float StartTime { get; protected set; }
		public float LastUpdateTime { get; protected set; }
		public float LastCreateTime { get; protected set; }

		public float HP { get; protected set; }
		public float CurrentMaxHP
		{
			get => CurrentBasicProperties.HP;
		}
		public float HPRatio
		{
			get => HP / CurrentMaxHP;
		}

		bool m_BuildingDestroyed;
		public bool BuildingDestroyed
		{
			get => m_BuildingDestroyed;
		}

		public float CurrentBuildingDuration
		{
			get => CurrentBasicProperties.BuildingDuration;
		}

		public float BuildingTime
		{
			get => CurrentBuildingDuration - (LastUpdateTime - StartTime);
		}

		bool m_ConstructionComplete = false;

		public bool IsBuildDone
		{
			get => LastUpdateTime - StartTime >= CurrentBuildingDuration;
		}

		public LOCardBasicProperties CurrentBasicProperties
		{
			get => CardModel.CardMeta.BasicProperties[CurrentLevel];
		}

		public Sprite CurrentBuildingImage
		{
			get => CardModel.CardMeta.GameImage[CurrentLevel];
		}

		public Sprite DestroyedImage
		{
			get => CardModel.CardMeta.DestroyedImage;
		}

		public LOCardType CardType
		{
			get => CardModel.CardMeta.CardType;
		}

		public virtual List<ILOCitizenModel> DisplayCitizens
		{
			get => new List<ILOCitizenModel>();
		}

		public virtual bool HasInjuredPeople { get => false; }

		public static LOBuildingModelBase CreateModel(
			LOGameCardModel cardModel,
			ILOBuildingModelBaseDataProvider dataProvider
		)
		{
			switch (cardModel.CardMeta.CardType)
			{
				case LOCardType.Building:
					return LOBuildingModel.Create(cardModel, dataProvider);
				case LOCardType.Production:
					return LOProductionModel.Create(cardModel, dataProvider);
				case LOCardType.Hospital:
					return LOHospitalModel.Create(cardModel, dataProvider);
				default:
					return new LOBuildingModelBase(cardModel, dataProvider);
			}
		}

		ILOBuildingModelBaseDataProvider m_DataProvider;

		protected float GetCreateOffsetTime()
		{
			return m_DataProvider.GameTime - LastCreateTime;
		}

		protected LOBuildingModelBase(
			LOGameCardModel cardModel,
			ILOBuildingModelBaseDataProvider dataProvider
		)
		{
			CardModel = cardModel;
			HP = CurrentBasicProperties.HP;
			CurrentLevel = 0;

			m_DataProvider = dataProvider;

			var now = m_DataProvider.GameTime;
			StartTime = now;
			LastUpdateTime = now;
			LastCreateTime = CurrentBuildingDuration + now;

			Equipments = new List<ILOEquipmentModelBase>();
		}

		protected virtual void DoCreate() { }

		public virtual string GetInfoText()
		{
			return "";
		}

		public virtual string GetMovingOutInfoText()
		{
			return "";
		}

		public virtual string GetMovingOutHintText()
		{
			return "";
		}

		public virtual string GetMovingInInfoText()
		{
			return "";
		}

		public virtual string GetMovePeopleOutText()
		{
			return "";
		}

		public virtual bool CanMovePeopleOut()
		{
			return false;
		}

		protected virtual float GetDamageReductionPercentage()
		{
			float damageRatio = 1f;
			foreach (var equipment in Equipments)
			{
				if (equipment.EquipmentType == LOEquipmentType.Damper)
				{
					damageRatio *= (1 - ((LOEquipmentDamperModel)equipment).DamageReductionRatio);
				}
			}
			return 1 - damageRatio;
		}

		public virtual void DoDamage(float damage)
		{
			var damageReductionPercentage = GetDamageReductionPercentage();

			HP -= damage * (1 - damageReductionPercentage);
			HP = Mathf.Clamp(HP, 0, CurrentMaxHP);
			OnHpUpdate?.Invoke();

			if (HP <= 0 && !m_BuildingDestroyed)
			{
				m_BuildingDestroyed = true;
				DoBuildingDestroy();
				OnBuildingDestroyed?.Invoke();
			}
			else
			{
				DoDamageToCitizens();
			}
		}

		protected virtual void DoDamageToCitizens() { }

		protected virtual void DoBuildingDestroy() { }

		protected bool CanEquip(ILOEquipmentModelBase equipmentModelBase)
		{
			// TODO: add multiple equipments
			return Equipments.Count == 0;
		}

		public virtual bool DoEquip(ILOEquipmentModelBase equipmentModelBase)
		{
			if (!CanEquip(equipmentModelBase)) { return false; }

			Equipments.Add(equipmentModelBase);
			OnItemEquipped?.Invoke();

			return true;
		}

		public void HandleTimeChange()
		{
			var now = m_DataProvider.GameTime;

			if (!IsBuildDone)
			{
				OnConstructionProgress?.Invoke();
				LastUpdateTime = now;
				return;
			}

			if (!m_ConstructionComplete)
			{
				m_ConstructionComplete = true;
				OnConstructionProgress?.Invoke();
			}

			if (!m_BuildingDestroyed)
			{
				DoCreate();
			}

			LastUpdateTime = now;
		}

		protected void NotifyCreateEvent()
		{
			OnCreateEvent?.Invoke();
		}

		protected void NotifyCitizenUpdate()
		{
			OnCitizenUpdate?.Invoke();
		}

		protected void NotifyCitizenHealed()
		{
			OnCitizenHealed?.Invoke();
		}

		public virtual bool CanUpgrade()
		{
			return IsBuildDone && CurrentLevel <= MAX_LEVEL;
		}

		public virtual void UpgradeLevel()
		{
			float preHPRatio = HPRatio;
			CurrentLevel++;
			HP = CurrentMaxHP * preHPRatio;
		}

		public virtual void DoHealingPeoples(int num) { }
	}
}

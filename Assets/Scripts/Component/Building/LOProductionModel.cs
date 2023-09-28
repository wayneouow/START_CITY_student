using System.Collections.Generic;
using System.Linq;
using LO.Meta;
using UnityEngine;

namespace LO.Model
{
	public interface ILOProductionModel : ILOBuildingModelBase
	{
		void ConsumeCoins(int amount);
		float GetEmptyVacancies();
		float WaitCoin { get; }
		float Coin { get; }
		void LoseEmployee(ILOCitizenModel citizenModel);
	}

	public class LOProductionModel : LOBuildingModelBase, ILOProductionModel
	{
		public static LOProductionModel Create(LOGameCardModel cardModel, ILOBuildingModelBaseDataProvider dataProvider)
		{
			return new LOProductionModel(cardModel, dataProvider);
		}

		public LOCardProductionProperties CurrentProductionProperties
		{
			get => ((LOProductionCardMeta)(CardModel.CardMeta)).ProductionProperties[CurrentLevel];
		}

		/// <summary>
		/// 剛產生出來，
		/// </summary>
		public float WaitCoin { get; private set; }
		public float Coin { get; private set; }
		public List<ILOCitizenModel> Employees;

		public override List<ILOCitizenModel> DisplayCitizens
		{
			get => Employees;
		}

		public override bool HasInjuredPeople
		{
			get
			{
				foreach (var citizen in Employees)
				{
					if (citizen.IsInjured)
					{
						return true;
					}
				}
				return false;
			}
		}

		private readonly object m_SyncRoot = new object();

		private LOProductionModel(LOGameCardModel cardModel, ILOBuildingModelBaseDataProvider dataProvider) : base(cardModel, dataProvider)
		{
			Employees = new List<ILOCitizenModel>();
		}

		protected override void DoCreate()
		{
			lock (this)
			{
				var offsetTime = GetCreateOffsetTime();

				if (offsetTime < 1) { return; }

				int healthyEmployees = Employees.Count(e => !e.IsInjured);

				var newCoin = offsetTime * healthyEmployees * CurrentProductionProperties.CoinPerSec;

				LastCreateTime += offsetTime;

				WaitCoin += newCoin;
				Coin += newCoin;

				NotifyCreateEvent();
			}
		}

		protected override void DoBuildingDestroy()
		{
			lock (m_SyncRoot)
			{
				foreach (var citizen in Employees)
				{
					if (citizen.ResidenceBuilding.BuildingDestroyed) { continue; }

					citizen.ResidenceBuilding.DoEmployeeEvacuate(citizen);
				}

				Employees.Clear();

				NotifyCitizenUpdate();
			}
		}

		protected override void DoDamageToCitizens()
		{
			lock (m_SyncRoot)
			{
				if (Employees.Count <= 0) { return; }
				int rand = Random.Range(0, Employees.Count);
				Employees[rand].DoDamage();
				NotifyCitizenUpdate();
			}
		}

		public void ConsumeCoins(int amount)
		{
			lock (this)
			{
				WaitCoin -= (float)amount;
			}
		}

		public void LoseEmployee(ILOCitizenModel citizenModel)
		{
			lock (this)
			{
				Employees.Remove(citizenModel);

				NotifyCitizenUpdate();
			}
		}

		public void DoHireCitizens(List<ILOCitizenModel> citizens)
		{
			lock (this)
			{
				Employees.AddRange(citizens);

				NotifyCitizenUpdate();
			}
		}

		public override void DoHealingPeoples(int num)
		{
			foreach (var employee in Employees)
			{
				if (employee.IsInjured)
				{
					employee.DoRecover();
					NotifyCitizenHealed();
					return;
				}
			}
		}


		public float GetEmptyVacancies()
		{
			return CurrentProductionProperties.TotalVacancies - (float)(Employees.Count);
		}

		string GetVacanciesText()
		{
			return "職缺數: " + GetEmptyVacancies();
		}

		public override string GetInfoText()
		{
			return "職員數: " + (float)(Employees.Count) + "\n" + GetVacanciesText();
		}

		public override string GetMovingInInfoText()
		{
			return GetVacanciesText() + "\n" + "指派居民?";
		}

		public override bool CanMovePeopleOut()
		{
			return false;
		}
	}
}
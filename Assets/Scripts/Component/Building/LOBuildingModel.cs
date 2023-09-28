using System.Collections.Generic;
using LO.Meta;
using UnityEngine;

namespace LO.Model
{
	public interface ILOBuildingModel : ILOBuildingModelBase
	{
		void DoEmployeeEvacuate(ILOCitizenModel citizenModel);
	}

	public class LOBuildingModel : LOBuildingModelBase, ILOBuildingModel
	{

		public static LOBuildingModel Create(LOGameCardModel cardModel, ILOBuildingModelBaseDataProvider dataProvider)
		{
			return new LOBuildingModel(cardModel, dataProvider);
		}

		public LOCardBuildingProperties CurrentBuildingProperties
		{
			get => ((LOBuildingCardMeta)(CardModel.CardMeta)).BuildingProperties[CurrentLevel];
		}

		public float FreePeople { get => UnemployedCitizens.Count; }
		public int TotalPeople { get => EmployedCitizens.Count + UnemployedCitizens.Count; }

		public List<ILOCitizenModel> UnemployedCitizens;
		public List<ILOCitizenModel> EmployedCitizens;

		private readonly object m_CitizenSync = new object();

		public override List<ILOCitizenModel> DisplayCitizens
		{
			get => UnemployedCitizens;
		}

		public override bool HasInjuredPeople
		{
			get
			{
				foreach (var citizen in UnemployedCitizens)
				{
					if (citizen.IsInjured)
					{
						return true;
					}
				}
				return false;
			}
		}

		private LOBuildingModel(LOGameCardModel cardModel, ILOBuildingModelBaseDataProvider dataProvider) : base(cardModel, dataProvider)
		{
			UnemployedCitizens = new List<ILOCitizenModel>();
			EmployedCitizens = new List<ILOCitizenModel>();
		}

		protected override void DoCreate()
		{
			var offsetTime = GetCreateOffsetTime();
			var peoplePerSec = CurrentBuildingProperties.PeoplePerSec;
			var newPeople = Mathf.FloorToInt(offsetTime * peoplePerSec);

			var tempTotalPeople = TotalPeople + newPeople;

			tempTotalPeople = (int)Mathf.Min((float)(tempTotalPeople), CurrentBuildingProperties.TotalPeople);

			var realNewPeople = tempTotalPeople - TotalPeople;
			var realUsedTime = realNewPeople / peoplePerSec;

			LastCreateTime = LastCreateTime + realUsedTime;

			for (int i = 0; i < realNewPeople; i++)
			{
				UnemployedCitizens.Add(LOCitizenModel.Create(this));
			}

			NotifyCitizenUpdate();
		}

		public override void DoDamage(float damage)
		{
			base.DoDamage(damage);
		}

		protected override void DoBuildingDestroy()
		{
			lock (m_CitizenSync)
			{
				foreach (var citizen in EmployedCitizens)
				{
					citizen.WorkplaceBuilding.LoseEmployee(citizen);
				}

				EmployedCitizens.Clear();
				UnemployedCitizens.Clear();

				NotifyCitizenUpdate();
			}
		}

		protected override void DoDamageToCitizens()
		{
			lock (m_CitizenSync)
			{
				if (UnemployedCitizens.Count <= 0) { return; }
				int rand = Random.Range(0, UnemployedCitizens.Count);
				UnemployedCitizens[rand].DoDamage();
				NotifyCitizenUpdate();
			}
		}

		public void DoEmployeeEvacuate(ILOCitizenModel citizenModel)
		{
			lock (m_CitizenSync)
			{
				citizenModel.WorkplaceBuilding = null;
				EmployedCitizens.Remove(citizenModel);
				UnemployedCitizens.Add(citizenModel);

				NotifyCitizenUpdate();
			}
		}

		public override void DoHealingPeoples(int num)
		{
			foreach (var citizen in UnemployedCitizens)
			{
				if (citizen.IsInjured)
				{
					citizen.DoRecover();
					NotifyCitizenHealed();
					return;
				}
			}
		}

		public override string GetInfoText()
		{
			return "人口數: " + TotalPeople + "\n" + GetUnemployedInfoText();
		}

		private string GetUnemployedInfoText()
		{
			return "閒置人口: " + UnemployedCitizens.Count;
		}

		public override string GetMovingOutInfoText()
		{
			return GetUnemployedInfoText();
		}

		public override string GetMovingOutHintText()
		{
			return "選擇附近的生產建築";
		}


		public override bool CanMovePeopleOut()
		{
			if (BuildingDestroyed) { return false; }

			return UnemployedCitizens.Count > 0;
		}

		public override string GetMovePeopleOutText()
		{
			return "指派人口";
		}

		public void AssignFreePeopleToProduction(LOProductionModel targetProduction)
		{
			var vacancies = targetProduction.GetEmptyVacancies();
			var assignAmount = Mathf.Min((int)vacancies, UnemployedCitizens.Count);

			lock (m_CitizenSync)
			{
				var selectedCitizens = UnemployedCitizens.GetRange(0, assignAmount);
				UnemployedCitizens.RemoveRange(0, assignAmount);

				foreach (var citizens in selectedCitizens)
				{
					citizens.WorkplaceBuilding = targetProduction;
				}

				EmployedCitizens.AddRange(selectedCitizens);
				targetProduction.DoHireCitizens(selectedCitizens);

				NotifyCitizenUpdate();
			}
		}
	}
}
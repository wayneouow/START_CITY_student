using System.Collections.Generic;
using System.Linq;
using LO.Meta;
using UnityEngine;

namespace LO.Model
{
    public interface ILOHealable
    {
        bool HasInjuredPeople { get; }
        void DoHealingPeoples(int num);
    }

    public interface ILOHospitalModel : ILOBuildingModelBase
    {
        void SetNeighbors(List<ILOHealable> healableNeighbors);
        void DoHealingPeoples();
    }

    public class LOHospitalModel : LOBuildingModelBase, ILOHospitalModel
    {
        public static LOHospitalModel Create(
            LOGameCardModel cardModel,
            ILOBuildingModelBaseDataProvider dataProvider
        )
        {
            return new LOHospitalModel(cardModel, dataProvider);
        }

        public LOHospitalCardProperties CurrentHospitalProperties
        {
            get => ((LOHospitalCardMeta)CardModel.CardMeta).HospitalProperties[CurrentLevel];
        }

        List<ILOHealable> m_HealableNeighbors;

        private LOHospitalModel(
            LOGameCardModel cardModel,
            ILOBuildingModelBaseDataProvider dataProvider
        ) : base(cardModel, dataProvider) { }

        protected override void DoCreate()
        {
            this.DoHealingPeoples();
        }

        public void DoHealingPeoples()
        {
            var offsetTime = GetCreateOffsetTime();
            var healingPeoplePerSecond = CurrentHospitalProperties.HealingPeoplePerSecond;
            var healingAmount = Mathf.FloorToInt(healingPeoplePerSecond * offsetTime);

            if (healingAmount == 0)
            {
                return;
            }

            var neighborsWithInjuredPeople = m_HealableNeighbors
                .Where(x => x.HasInjuredPeople)
                .ToList();
            if (neighborsWithInjuredPeople.Count == 0)
            {
                return;
            }
            var rand = Random.Range(0, neighborsWithInjuredPeople.Count);
            neighborsWithInjuredPeople[rand].DoHealingPeoples(1);

            var realUsedTime = healingAmount / healingPeoplePerSecond;

            LastCreateTime = LastCreateTime + realUsedTime;
        }

        public void SetNeighbors(List<ILOHealable> healableNeighbors)
        {
            m_HealableNeighbors = healableNeighbors;
        }

		#region UI Text Properties

        public override string GetInfoText()
        {
            return CardModel.CardMeta.Desc;
        }

		#endregion
    }
}

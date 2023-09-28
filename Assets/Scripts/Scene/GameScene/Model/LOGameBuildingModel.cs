using System;
using System.Collections;
using System.Collections.Generic;
using LO.Meta;
using UnityEngine;

namespace LO.Model
{
    public class LOGameBuildingModel : LOGameBuildingModelBase
    {
        public static LOGameBuildingModel Create(LOGameCardModel cardModel, LOGameModel gameModel)
        {
            return new LOGameBuildingModel(cardModel, gameModel);
        }

        public LOCardBuildingProperties CurrentBuildingProperties
        {
            get => ((LOBuildingCardMeta)(CardModel.CardMeta)).BuildingProperties[CurrentLevel];
        }

        public float FreePeople;
        public float TotalPeople;

        private LOGameBuildingModel(LOGameCardModel cardModel, LOGameModel gameModel)
            : base(cardModel, gameModel) { }

        protected override void DoCreate(float offsetTime)
        {
            var newPeople = Mathf.FloorToInt(offsetTime * CurrentBuildingProperties.PeoplePerSec);

            var tempTotalPeople = TotalPeople + newPeople;

            tempTotalPeople = Mathf.Min(tempTotalPeople, CurrentBuildingProperties.TotalPeople);

            var realNewPeople = tempTotalPeople - TotalPeople;

            FreePeople += realNewPeople;
            TotalPeople = tempTotalPeople;
        }
    }
}

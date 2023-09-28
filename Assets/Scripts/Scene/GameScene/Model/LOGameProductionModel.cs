using System.Collections;
using System.Collections.Generic;
using LO.Meta;
using UnityEngine;

namespace LO.Model
{
    public class LOGameProductionModel : LOGameBuildingModelBase
    {
        public static LOGameProductionModel Create(LOGameCardModel cardModel, LOGameModel gameModel)
        {
            return new LOGameProductionModel(cardModel, gameModel);
        }

        public LOCardProductionProperties CurrentProductionProperties
        {
            get => ((LOProductionCardMeta)(CardModel.CardMeta)).ProductionProperties[CurrentLevel];
        }

        /// <summary>
        /// 剛產生出來，尚未結算的錢
        /// </summary>
        public float WaitCoin;
        public float Coin;
        public float Vacancies;

        private LOGameProductionModel(LOGameCardModel cardModel, LOGameModel gameModel)
            : base(cardModel, gameModel) { }

        protected override void DoCreate(float offsetTime)
        {
            var newCoin = offsetTime * Vacancies * CurrentProductionProperties.CoinPerSec;

            WaitCoin += newCoin;
            Coin += newCoin;
        }

        public float GetEmptyVacancies()
        {
            return CurrentProductionProperties.TotalVacancies - Vacancies;
        }
    }
}

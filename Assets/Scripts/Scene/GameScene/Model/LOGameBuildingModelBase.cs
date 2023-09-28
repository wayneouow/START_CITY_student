using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Meta;
using LO.Event;

namespace LO.Model
{
    public class LOGameBuildingModelBase
    {
        public LOGameCardModel CardModel;

        public bool IsBuilding
        {
            get => CardModel.CardMeta.CardType == LOCardType.Building;
        }
        public bool IsProduction
        {
            get => CardModel.CardMeta.CardType == LOCardType.Production;
        }

        public float StartGameTime; // 產生時，當前的遊戲時間
        public float LastUpdateGameTime; // 最後一次更新的遊戲時間

        public float HP;
        public float NowBuildingTime;
        public int CurrentLevel;

        public LOCardBasicProperties CurrentBasicProperties
        {
            get => CardModel.CardMeta.BasicProperties[CurrentLevel];
        }

        public float BuildingTime
        {
            get => CurrentBasicProperties.BuildingDuration - (LastUpdateGameTime - StartGameTime);
        }
        public bool IsBuildDone
        {
            get => BuildingTime <= 0;
        }

        public float HPRatio
        {
            get => HP / CurrentBasicProperties.HP;
        }

        public Sprite CurrentBuildingImage
        {
            get => CardModel.CardMeta.GameImage[CurrentLevel];
        }

        public bool IsActive
        {
            get => this.HP > 0;
        }

        public float LastCreateTime = -1;

        public event LOSimpleEvent OnHpUpdate;

        public static LOGameBuildingModelBase CreateModel(
            LOGameCardModel cardModel,
            LOGameModel gameModel
        )
        {
            switch (cardModel.CardMeta.CardType)
            {
                case LOCardType.Building:
                    return LOGameBuildingModel.Create(cardModel, gameModel);
                case LOCardType.Production:
                    return LOGameProductionModel.Create(cardModel, gameModel);
                default:
                    return new LOGameBuildingModelBase(cardModel, gameModel);
            }
        }

        protected LOGameBuildingModelBase(LOGameCardModel cardModel, LOGameModel gameModel)
        {
            CardModel = cardModel;
            StartGameTime = gameModel.GameTime;
            LastUpdateGameTime = gameModel.GameTime;

            CurrentLevel = 0;
            HP = CurrentBasicProperties.HP;
            NowBuildingTime = 0;
        }

        protected virtual void DoCreate(float offsetTime) { }

        public void UpdateLastUpdateGameTime(float gameTime)
        {
            LastUpdateGameTime = gameTime;

            if (!IsBuildDone)
                return;

            if (LastCreateTime < 0 && IsBuildDone)
            {
                LastCreateTime = -BuildingTime + LastUpdateGameTime;
            }

            var offsetTime = LastUpdateGameTime - LastCreateTime;
            DoCreate(offsetTime);

            LastCreateTime = LastUpdateGameTime;
        }

        public void DoDamage(float damage)
        {
            HP -= damage;
            HP = Mathf.Max(HP, 0);
            Debug.Log($"DoDamage {damage} {HP} {HPRatio}");
            OnHpUpdate?.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using LO.Meta;
using LO.View;
using UnityEngine;

namespace LO.Model
{
    public class LODemolishModel : LOFunctionalModelBase
    {
        public static LODemolishModel Create(LOFunctionalCardMeta cardMeta)
        {
            return new LODemolishModel(cardMeta);
        }

        private LODemolishModel(LOFunctionalCardMeta cardMeta) : base(cardMeta) { }

        public override bool DoEffect(ILODemoSceneAreaView effectAreaView)
        {
            if (effectAreaView.BuildingView == null)
            {
                return false;
            }

            effectAreaView.DoDemolish();
            return true;
        }
    }
}

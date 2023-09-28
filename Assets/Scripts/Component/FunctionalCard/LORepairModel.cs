using System.Collections;
using System.Collections.Generic;
using LO.Meta;
using LO.View;
using UnityEngine;

namespace LO.Model
{
	public class LORepairModel : LOFunctionalModelBase
	{
		public static LORepairModel Create(LOFunctionalCardMeta cardMeta)
		{
			return new LORepairModel(cardMeta);
		}

		private LORepairModel(LOFunctionalCardMeta cardMeta) : base(cardMeta) { }

		public override bool DoEffect(ILODemoSceneAreaView effectAreaView)
		{
			// TODO : refactor
			if (effectAreaView.BuildingView == null)
			{
				return false;
			}

			var hp = effectAreaView.BuildingView.BuildingModel.HPRatio;

			if (hp == 1 || hp == 0)
			{
				return false;
			}

			effectAreaView.DoRepair(CardMeta.BasicProperties[0].HP);
			return true;
		}
	}
}

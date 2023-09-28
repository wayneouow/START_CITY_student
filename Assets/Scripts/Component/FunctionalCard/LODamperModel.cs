using System.Collections;
using System.Collections.Generic;
using LO.Meta;
using LO.View;
using UnityEngine;

namespace LO.Model
{
	public class LODamperModel : LOFunctionalModelBase
	{
		public static LODamperModel Create(LOFunctionalCardMeta cardMeta)
		{
			return new LODamperModel(cardMeta);
		}
		private LODamperModel(LOFunctionalCardMeta cardMeta) : base(cardMeta) { }

		public override bool DoEffect(ILODemoSceneAreaView effectAreaView)
		{
			if (effectAreaView.BuildingView == null)
			{
				return false;
			}

			LOEquipmentDamperModel equipmentModelBase = LOEquipmentModelBase.CreateEquipmentModel(this) as LOEquipmentDamperModel;

			return effectAreaView.DoEquip(equipmentModelBase);
		}
	}
}

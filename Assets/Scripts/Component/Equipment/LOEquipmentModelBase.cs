using LO.Meta;
using UnityEngine;

namespace LO.Model
{
	public enum LOEquipmentType
	{
		None,
		Damper
	}

	public interface ILOEquipmentModelBase
	{
		LOEquipmentType EquipmentType { get; }
		Sprite EquipmentSprite { get; }
	}

	public class LOEquipmentModelBase : ILOEquipmentModelBase
	{
		public Sprite EquipmentSprite { get; private set; }

		public static ILOEquipmentModelBase CreateEquipmentModel(LOFunctionalModelBase functionalModelBase)
		{
			switch (functionalModelBase.CardMeta.FunctionCardType)
			{
				case FunctionCardType.Damper:
					return LOEquipmentDamperModel.Create(functionalModelBase);
				default:
					return new LOEquipmentModelBase(functionalModelBase);
			}

		}

		public LOEquipmentType EquipmentType { get; protected set; }
		protected LOFunctionalModelBase FunctionalModel;

		protected LOEquipmentModelBase(LOFunctionalModelBase functionalModelBase)
		{
			FunctionalModel = functionalModelBase;
			EquipmentSprite = functionalModelBase.CardMeta.GameImage[0];
		}
	}
}
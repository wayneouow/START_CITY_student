namespace LO.Model
{
	public interface ILEquipmentDamperModel : ILOEquipmentModelBase
	{
		float DamageReductionRatio { get; }
	}

	public class LOEquipmentDamperModel : LOEquipmentModelBase, ILEquipmentDamperModel
	{
		public static LOEquipmentDamperModel Create(LOFunctionalModelBase functionalModelBase)
		{
			return new LOEquipmentDamperModel(functionalModelBase);
		}

		public float DamageReductionRatio { get => FunctionalModel.CardMeta.FunctionalCardProperties.DamageReductionPercentage; }

		private LOEquipmentDamperModel(LOFunctionalModelBase functionalModelBase) : base(functionalModelBase)
		{
			EquipmentType = LOEquipmentType.Damper;
		}
	}
}
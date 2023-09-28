namespace LO.Model
{
	public interface ILOCitizenModel
	{
		ILOBuildingModel ResidenceBuilding { get; set; }
		ILOProductionModel WorkplaceBuilding { get; set; }
		bool IsInjured { get; }
		void DoDamage();
		void DoRecover();
	}

	public class LOCitizenModel : ILOCitizenModel
	{
		public ILOBuildingModel ResidenceBuilding { get; set; }
		public ILOProductionModel WorkplaceBuilding { get; set; }

		bool m_IsInjured = false;
		public bool IsInjured { get => m_IsInjured; }

		public static LOCitizenModel Create(ILOBuildingModel residenceBuilding)
		{
			return new LOCitizenModel(residenceBuilding);
		}

		private LOCitizenModel(ILOBuildingModel residenceBuilding)
		{
			ResidenceBuilding = residenceBuilding;
		}

		public void DoDamage()
		{
			m_IsInjured = true;
		}

		public void DoRecover()
		{
			m_IsInjured = false;
		}
	}
}
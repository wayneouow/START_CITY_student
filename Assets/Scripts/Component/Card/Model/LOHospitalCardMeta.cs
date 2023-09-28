using UnityEngine;

namespace LO.Meta
{
	[System.Serializable]
	public class LOHospitalCardProperties
	{
		public float HealingPeoplePerSecond;
	}

	[CreateAssetMenu(fileName = "醫院卡牌", menuName = "城市遊戲/卡牌資料/醫院卡牌")]
	public class LOHospitalCardMeta : LOCardMetaBase
	{
		[Header("Hospital")]
		public LOHospitalCardProperties[] HospitalProperties;

		public LOHospitalCardMeta()
		{
			CardType = LOCardType.Hospital;
		}
	}
}
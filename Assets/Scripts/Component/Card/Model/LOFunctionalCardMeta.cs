using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
	public enum FunctionCardType
	{
		Repair = 0, // 修理
		Demolish = 1, // 拆除
		Damper = 2, // 阻尼器
	}

	[System.Serializable]
	public class LOFunctionalCardProperties
	{
		public float DamageReductionPercentage;
	}

	[CreateAssetMenu(fileName = "功能卡牌", menuName = "城市遊戲/卡牌資料/功能卡牌")]
	public class LOFunctionalCardMeta : LOCardMetaBase
	{
		[Header("功能卡類型")]
		// TODO : refactor
		public FunctionCardType FunctionCardType;

		public LOFunctionalCardProperties FunctionalCardProperties;

		public LOFunctionalCardMeta()
		{
			CardType = LOCardType.Functional;
		}
	}
}

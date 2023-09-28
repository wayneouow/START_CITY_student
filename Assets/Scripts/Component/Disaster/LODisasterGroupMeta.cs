using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
	[CreateAssetMenu(fileName = "disaster-group-meta", menuName = "城市遊戲/災害資料/災害組合", order = 1)]
	public class LODisasterGroupMeta : ScriptableObject
	{
		public List<LODisasterMeta> DisasterList;
	}
}
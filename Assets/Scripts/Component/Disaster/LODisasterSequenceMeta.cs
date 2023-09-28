using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
	[System.Serializable]
	public class LODisasterSequenceUnit
	{
		public float OccurrenceTime;
		public LODisasterGroupMeta DisasterGroup;
	}


	[CreateAssetMenu(fileName = "disaster-sequence-meta", menuName = "城市遊戲/災害資料/災害序列", order = 0)]
	public class LODisasterSequenceMeta : ScriptableObject
	{
		public List<LODisasterSequenceUnit> DisasterGroupList;
	}
}
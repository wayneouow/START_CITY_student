using UnityEngine;

namespace LO.Meta
{
	[CreateAssetMenu(fileName = "disaster-meta", menuName = "城市遊戲/災害資料/災害", order = 2)]
	public class LODisasterMeta : ScriptableObject
	{
		public int Position;  // -1 for random position
		public float Damage;
	}
}
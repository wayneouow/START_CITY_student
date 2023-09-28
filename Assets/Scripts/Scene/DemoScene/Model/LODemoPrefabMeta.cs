using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
	[CreateAssetMenu(fileName = "demo-prefab", menuName = "城市遊戲/Prefab/遊戲的PrefabV2")]
	public class LODemoPrefabMeta : ScriptableObject
	{
		public GameObject DemoBlockPrefab;
		public GameObject DemoCardPrefab;
		public GameObject DemoBuildingPrefab;
	}
}

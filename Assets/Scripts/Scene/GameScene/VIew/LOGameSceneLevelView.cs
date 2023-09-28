using System.Collections;
using System.Collections.Generic;
using LO.Model;
using UnityEngine;

namespace LO.View
{
    public class LOGameSceneLevelView : MonoBehaviour
    {
        [SerializeField]
        Transform m_Transform;

        [SerializeField]
        List<LOGameSceneBlockView> m_BlockList = new List<LOGameSceneBlockView>();

        [SerializeField]
        float m_BlockPos = 2;

        public void Init(LOGameModel gameModel)
        {
            InitBlockPosition();
            gameModel.OnGameTimeUpdate += HandleGameTimeChange;
        }

        void InitBlockPosition()
        {
            var posMap = new Vector3[]
            {
                new Vector3(m_BlockPos, 0, m_BlockPos),
                new Vector3(m_BlockPos, 0, 0),
                new Vector3(m_BlockPos, 0, -m_BlockPos),
                new Vector3(0, 0, m_BlockPos),
                new Vector3(0, 0, 0),
                new Vector3(0, 0, -m_BlockPos),
                new Vector3(-m_BlockPos, 0, m_BlockPos),
                new Vector3(-m_BlockPos, 0, 0),
                new Vector3(-m_BlockPos, 0, -m_BlockPos),
            };

            for (int i = 0; i < 9; i++)
            {
                var newBlock = GameObject
                    .Instantiate(LOGameSceneFinder.Instance.PrefabMeta.GameBlockPrefab, m_Transform)
                    .GetComponent<LOGameSceneBlockView>();
                var newBlockModel = LOGameBlockModel.Create(i);
                newBlock.Init(newBlockModel, posMap[i]);
                m_BlockList.Add(newBlock);
            }
        }

        void HandleGameTimeChange()
        {
            var gameTime = LOGameApplication.Instance.GameController.GameModel.GameTime;
            foreach (var block in m_BlockList)
            {
                block.HandleGameTimeChange(gameTime);
            }

            float totalPeople = 0;

            // 分配多的人口同時紀錄新產生的金幣
            // 1.   找到生產建築
            // 2.   如果生產建築滿了就跳過
            // 3.   掃一遍周圍全部的住宅建築
            // 3.1  確認目前還缺多少
            // 3.2  如果住宅建築有多的人就提供過去
            foreach (var block in m_BlockList)
            {
                for (int i = 0; i < block.ChildArea.Count; i++)
                {
                    var area = block.ChildArea[i];
                    if (area.BuildingView == null)
                    {
                        continue;
                    }
                    var model = area.BuildingView.BuildingModel;
                    // 1.   找到生產建築
                    if (model.IsProduction)
                    {
                        var productionModel = model as LOGameProductionModel;

                        var waitCoin = productionModel.WaitCoin;
                        LOGameApplication.Instance.GameController.AddCoin(waitCoin);
                        productionModel.WaitCoin = 0;

                        var empty = productionModel.GetEmptyVacancies();

                        // 2.   如果生產建築滿了就跳過
                        if (empty == 0)
                            continue;
                        // 3.   掃一遍周圍全部的住宅建築
                        for (int j = 0; j < block.ChildArea.Count; j++)
                        {
                            var otherArea = block.ChildArea[j];
                            if (i == j || otherArea.BuildingView == null)
                                continue;
                            var otherModel = otherArea.BuildingView.BuildingModel;
                            if (otherModel.IsBuilding)
                            {
                                var otherBuildingModel = otherModel as LOGameBuildingModel;
                                // 3.1  確認目前還缺多少
                                var newEmpty = productionModel.GetEmptyVacancies();

                                // 3.2  如果住宅建築有多的人就提供過去
                                if (otherBuildingModel.FreePeople > 0)
                                {
                                    var transferPeople = Mathf.Min(
                                        otherBuildingModel.FreePeople,
                                        newEmpty
                                    );

                                    otherBuildingModel.FreePeople -= transferPeople;
                                    productionModel.Vacancies += transferPeople;
                                }
                            }
                        }
                    }
                    else if (model.IsBuilding)
                    {
                        var buildingModel = model as LOGameBuildingModel;
                        totalPeople += buildingModel.TotalPeople;
                    }
                }
            }
            LOGameApplication.Instance.GameController.UpdatePeople((int)totalPeople);
        }
    }
}

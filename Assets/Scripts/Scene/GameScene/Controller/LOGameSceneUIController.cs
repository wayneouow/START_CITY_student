using System.Collections;
using System.Collections.Generic;
using LO.View;
using UnityEngine;

namespace LO.Controller
{
    public interface ILOGameSceneUIController
    {
        void Init();
        void LogBuilding(LOGameSceneAreaView area);
    }

    public class LOGameSceneUIController : ILOGameSceneUIController
    {
        public static ILOGameSceneUIController Create()
        {
            return new LOGameSceneUIController();
        }

        LOGameSceneLevelView m_LOGameSceneLevelView;
        LOGameSceneUIView m_GameSceneUIView;

        private LOGameSceneUIController()
        {
            m_LOGameSceneLevelView = LOGameSceneFinder.Instance.GameSceneLevelView;
            m_GameSceneUIView = LOGameSceneFinder.Instance.GameSceneUIView;
        }

        public void Init()
        {
            var gameModel = LOGameApplication.Instance.GameController.GameModel;
            m_GameSceneUIView.Init(gameModel);
            m_LOGameSceneLevelView.Init(gameModel);
        }

        public void LogBuilding(LOGameSceneAreaView area)
        {
            m_GameSceneUIView.StateView.LogBuilding(area);
        }
    }
}

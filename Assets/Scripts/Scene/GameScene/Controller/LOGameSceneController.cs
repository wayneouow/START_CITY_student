using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Controller
{
    public interface ILOGameSceneController
    {
        ILOGameController GameController { get; }
        ILOGameSceneUIController GameSceneUIController { get; }
    }

    public class LOGameSceneController : ILOGameSceneController
    {
        public static ILOGameSceneController Create()
        {
            return new LOGameSceneController();
        }

        public ILOGameController GameController { get; private set; }
        public ILOGameSceneUIController GameSceneUIController { get; private set; }

        private LOGameSceneController()
        {
            GameController = LOGameController.Create();
            GameSceneUIController = LOGameSceneUIController.Create();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Controller {

    public interface ILOMainSceneController {

        ILOMainSceneUIController MainSceneUIController { get; }
    }

    public class LOMainSceneController : LOMainSceneBaseController, ILOMainSceneController {

        public static ILOMainSceneController Create() {
            return new LOMainSceneController();
        }

        ILOMainSceneUIController m_MainSceneUIController;
        public override ILOMainSceneUIController MainSceneUIController { get => m_MainSceneUIController; }

        private LOMainSceneController() {

            m_MainSceneUIController = LOMainSceneUIController.Create();

            MainSceneUIController.Show();
        }
    }
}

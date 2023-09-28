using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Controller {

    public interface ILOPreloadSceneController {

        ILOPreloadSceneUIController PreloadSceneUIController { get; }
    }

    public class LOPreloadSceneController : LOPreloadSceneBaseController, ILOPreloadSceneController {

        public static ILOPreloadSceneController Create() {
            return new LOPreloadSceneController();
        }

        ILOPreloadSceneUIController m_PreloadSceneUIController;

        public override ILOPreloadSceneUIController PreloadSceneUIController { get => m_PreloadSceneUIController; }

        private LOPreloadSceneController() : base() {

            m_PreloadSceneUIController = LOPreloadSceneUIController.Create();
        }
    }
}
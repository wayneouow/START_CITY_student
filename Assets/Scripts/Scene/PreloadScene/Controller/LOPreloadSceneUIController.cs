using LO.Event;
using LO.View;

namespace LO.Controller {

    public interface ILOPreloadSceneUIController : ILOUIBaseController {

    }

    public class LOPreloadSceneUIController : LOPreloadSceneBaseController, ILOPreloadSceneUIController, ILOPreloadSceneViewDelegate {

        public static ILOPreloadSceneUIController Create() {
            return new LOPreloadSceneUIController();
        }

        ILOPreloadSceneView m_View;

        private LOPreloadSceneUIController() : base() {

            // get view from scene finder and init view
            m_View = LOPreloadSceneFinder.Instance.PreloadSceneView;
            m_View.Init(this);

            LOApplication.Instance.ApplicationReady += HandleApplicationReady;
        }

        void HandleApplicationReady() {

            m_View.Show();
        }

        #region ILOPreloadSceneViewDelegate

        void ILOPreloadSceneViewDelegate.HandleStartBtnClick() {
            // TODO: can not click this button before application ready
            LOApplication.Instance.GoToMain();
        }

        #endregion

        #region ILOUIBaseController

        public void Show(LOSimpleEvent complete = null) { }
        public void Dismiss(LOSimpleEvent complete = null) { }

        #endregion
    }
}
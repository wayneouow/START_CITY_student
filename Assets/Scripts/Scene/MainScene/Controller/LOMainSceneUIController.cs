using LO.Event;
using LO.View;

namespace LO.Controller
{
    public interface ILOMainSceneUIController : ILOUIBaseController { }

    public class LOMainSceneUIController : LOMainSceneBaseController, ILOMainSceneUIController
    {
        public static ILOMainSceneUIController Create()
        {
            return new LOMainSceneUIController();
        }

        LOMainSceneView m_MainSceneView;
        ILOUIBaseController m_NowOpenController;

        private LOMainSceneUIController()
        {
            m_MainSceneView = LOMainSceneFinder.Instance.MainSceneView;
            m_MainSceneView.Init();
        }

        #region ILOUIBaseController

        public void Show(LOSimpleEvent complete = null) { }

        public void Dismiss(LOSimpleEvent complete = null) { }

        #endregion
    }
}

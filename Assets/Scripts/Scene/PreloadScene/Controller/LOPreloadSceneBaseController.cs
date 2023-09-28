namespace LO.Controller {

    public interface ILOPreloadSceneBaseController : ILOBaseController {

        // preload
        ILOPreloadSceneController PreloadSceneController { get; }
        ILOPreloadSceneUIController PreloadSceneUIController { get; }
    }

    public class LOPreloadSceneBaseController : LOBaseController, ILOPreloadSceneBaseController {

        // preload
        public virtual ILOPreloadSceneController PreloadSceneController { get => LOApplication.Instance.PreloadSceneController; }
        public virtual ILOPreloadSceneUIController PreloadSceneUIController { get => LOApplication.Instance.PreloadSceneController.PreloadSceneUIController; }

        protected LOPreloadSceneBaseController() : base() { }
    }
}
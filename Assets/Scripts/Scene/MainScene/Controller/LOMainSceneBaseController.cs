namespace LO.Controller
{
    public interface ILOMainSceneBaseController : ILOBaseController
    {
        // main
        ILOMainSceneController MainSceneController { get; }
        ILOMainSceneUIController MainSceneUIController { get; }
    }

    public class LOMainSceneBaseController : LOBaseController, ILOMainSceneBaseController
    {
        // main
        public virtual ILOMainSceneController MainSceneController
        {
            get => LOApplication.Instance.MainSceneController;
        }
        public virtual ILOMainSceneUIController MainSceneUIController
        {
            get => LOApplication.Instance.MainSceneController.MainSceneUIController;
        }

        protected LOMainSceneBaseController() : base() { }
    }
}

namespace LO.Controller
{
    public interface ILOGameSceneBaseController : ILOBaseController
    {
        // game
        ILOGameSceneController GameSceneController { get; }
        ILOGameController GameController { get; }
        ILOGameSceneUIController GameSceneUIController { get; }
    }

    public class LOGameSceneBaseController : LOBaseController, ILOGameSceneBaseController
    {
        // game
        public ILOGameSceneController GameSceneController
        {
            get => LOApplication.Instance.GameSceneController;
        }

        public ILOGameController GameController
        {
            get => LOApplication.Instance.GameSceneController.GameController;
        }

        public ILOGameSceneUIController GameSceneUIController
        {
            get => LOApplication.Instance.GameSceneController.GameSceneUIController;
        }

        protected LOGameSceneBaseController() : base() { }
    }
}

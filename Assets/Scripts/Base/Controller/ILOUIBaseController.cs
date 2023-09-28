using LO.Event;

namespace LO.Controller {

    public interface ILOUIBaseController {

        void Show(LOSimpleEvent complete = null);
        void Dismiss(LOSimpleEvent complete = null);
    }
}
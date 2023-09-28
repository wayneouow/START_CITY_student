using LO.Event;

namespace LO.View {

    public interface ILOUIBaseView {

        void Show(LOSimpleEvent complete = null);
        void Dismiss(LOSimpleEvent complete = null);
        void Reload();
    }
}
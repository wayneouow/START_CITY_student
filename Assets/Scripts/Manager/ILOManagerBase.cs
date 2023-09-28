using LO.Event;

namespace LO.Manager {

    public interface ILOManagerBase {

        void Reload(LOSimpleEvent complete = null);
    }
}
using LO.View;
using UnityEngine;
using LO.Model;

namespace LO.Event
{
    public delegate void LOSimpleEvent();
    public delegate void LOBoolEvent(bool value);
    public delegate void LOFloatEvent(float value);
    public delegate void LODisasterEvent(int position, float damage);
    public delegate void LOObjectEvent<T>(T obj) where T : Object;
    public delegate void LODialogEvent<T>(T dialog) where T : ILOUIDialogViewBase;
    public delegate void LOUserModelEvent(ILOUserModel user);
}

using UnityEngine;

namespace LO.View
{
    public interface IILOUIDialogViewBaseDelegate
    {
        void OnDismiss(string key);
    }

    public interface ILOUIDialogViewBase
    {
        void Init(string key, IILOUIDialogViewBaseDelegate viewDelegate);
        void Show();
        void Dismiss();
        void HandleDismiss();
    }

    public class LOUIDialogViewBase : MonoBehaviour, ILOUIDialogViewBase
    {
        // [SerializeField] protected GameObject m_GameObject;

        protected string m_Key;
        protected IILOUIDialogViewBaseDelegate m_ViewDelegate;

        public virtual void Init(string key, IILOUIDialogViewBaseDelegate viewDelegate)
        {
            m_Key = key;
            m_ViewDelegate = viewDelegate;
        }

        public virtual void Show() { }

        /// <summary>
        /// Call the delegate to dismiss this dialog
        /// </summary>
        public virtual void Dismiss()
        {
            m_ViewDelegate.OnDismiss(m_Key);
        }

        /// <summary>
        /// Do destroy itself
        /// </summary>
        public virtual void HandleDismiss()
        {
            // GameObject.Destroy(m_GameObject);
        }
    }
}

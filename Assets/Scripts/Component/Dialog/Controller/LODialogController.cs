using System.Collections.Generic;
using LO.Event;
using LO.Utils;
using LO.View;

namespace LO.Controller
{
    public interface ILODialogController
    {
        void Show<T>(string dialogKey, LODialogEvent<T> complete) where T : LOUIDialogViewBase;
    }

    public class LODialogController : ILODialogController, IILOUIDialogViewBaseDelegate
    {
        Dictionary<string, ILOUIDialogViewBase> m_ActiveDialogs =
            new Dictionary<string, ILOUIDialogViewBase>();

        ILODialogView m_DialogView;

        public static ILODialogController Create()
        {
            return new LODialogController();
        }

        private LODialogController() { }

        public void Show<T>(string dialogKey, LODialogEvent<T> complete)
            where T : LOUIDialogViewBase
        {
            // if the dialog is already active, just return the old value
            if (m_ActiveDialogs.ContainsKey(dialogKey))
            {
                complete?.Invoke((T)m_ActiveDialogs[dialogKey]);
                return;
            }

            LOAddressable.Instantiate<T>(
                dialogKey,
                m_DialogView.DialogRoot,
                (dialog) =>
                {
                    m_ActiveDialogs.Add(dialogKey, dialog);
                    dialog.Init(dialogKey, this);
                    complete?.Invoke(dialog);
                    dialog.Show();
                }
            );
        }

        #region IILOUIDialogViewBaseDelegate

        void IILOUIDialogViewBaseDelegate.OnDismiss(string key)
        {
            // if dialog is already closed, do nothing
            if (!m_ActiveDialogs.ContainsKey(key))
                return;

            m_ActiveDialogs[key].HandleDismiss();
            m_ActiveDialogs.Remove(key);
        }

        #endregion
    }
}

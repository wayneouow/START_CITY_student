using DG.Tweening;
using LO.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
	public interface ILODialogViewDelegate
	{
		void OnCloseBtnClick();
		void OnConfirmBtnClick();
	}

	public interface ILODialogView
	{
		void Init(ILODialogViewDelegate viewDelegate);
		void ShowConfirmDialog(string title, string desc);
		RectTransform DialogRoot { get; }
	}

	public class LODialogView : MonoBehaviour, ILODialogView
	{
		[SerializeField] protected CanvasGroup m_RootCanvasGroup;
		[SerializeField] protected RectTransform m_DialogRoot;
		[SerializeField] protected Image m_RaycastBlockingLayer;
		[SerializeField] protected Text m_TitleText;
		[SerializeField] protected Text m_DescText;

		public RectTransform DialogRoot
		{
			get => m_DialogRoot;
		}

		ILODialogViewDelegate m_ViewDelegate;

		public void Init(ILODialogViewDelegate viewDelegate)
		{
			m_ViewDelegate = viewDelegate;
		}

		void SetText(string title, string desc)
		{
			m_TitleText.text = title;
			m_DescText.text = desc;
		}

		public void ShowConfirmDialog(string title, string desc)
		{
			SetText(title, desc);

			m_RootCanvasGroup.alpha = 0;
			m_RootCanvasGroup.DOFade(1f, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
			{
				m_RootCanvasGroup.SetInteract(true);
			});
		}

		public void HideDialog()
		{
			m_RootCanvasGroup.SetInteract(false);
			m_RootCanvasGroup.DOFade(0f, 0.3f).SetEase(Ease.Linear);
		}

		#region button handlers

		public void HandleCloseBtnClick()
		{
			m_ViewDelegate?.OnCloseBtnClick();
		}

		public void HandleConfirmBtnClick()
		{
			m_ViewDelegate?.OnConfirmBtnClick();
		}

		#endregion
	}
}
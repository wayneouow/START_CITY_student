using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
	public class LOCitizenView : MonoBehaviour
	{
		bool m_IsInjured = false;
		public bool IsInjured { get => m_IsInjured; }

		[SerializeField] Image m_Image;
		[SerializeField] RectTransform m_HealingRect;
		[SerializeField] CanvasGroup m_HealingCanvasGroup;
		[SerializeField] CanvasGroup m_RootCanvasGroup;

		static readonly Vector2 HEALING_RECT_END_POS = new Vector2(0, 20);
		static readonly Vector2 HEALING_RECT_START_POS = new Vector2(0, 0);

		private void Awake()
		{
			m_Image.color = Color.green;
		}

		public void SetActive(bool active)
		{
			m_RootCanvasGroup.alpha = active ? 1 : 0;
		}

		public void DoHealingAnim()
		{
			m_IsInjured = false;
			m_Image.DOColor(Color.green, 0.5f);
			m_HealingRect.anchoredPosition = HEALING_RECT_START_POS;
			m_HealingRect.DOAnchorPos(HEALING_RECT_END_POS, 0.5f);
			m_HealingCanvasGroup.alpha = 1;
			m_HealingCanvasGroup.DOFade(0f, 0.5f).SetEase(Ease.OutSine);
		}

		public void DoInjuredAnim()
		{
			m_IsInjured = true;
			m_Image.DOColor(Color.red, 0.2f);
		}
	}
}
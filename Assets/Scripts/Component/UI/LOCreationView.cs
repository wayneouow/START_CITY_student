using System.Collections;
using System.Collections.Generic;
using LO.Meta;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
	public class LOCreationView : MonoBehaviour
	{
		[SerializeField] Image m_Image;
		[SerializeField] Sprite m_CoinSprite;
		[SerializeField] Sprite m_CitizenSprite;

		public void SetImage(LOCardType cardType)
		{
			switch (cardType)
			{
				case LOCardType.Production:
					m_Image.sprite = m_CoinSprite;
					break;
				default:
					break;
			}
		}

		public void HandleAnimationEnd()
		{
			Destroy(this.gameObject);
		}
	}
}

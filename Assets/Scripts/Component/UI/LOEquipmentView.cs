using System.Collections;
using System.Collections.Generic;
using LO.Model;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
	public class LOEquipmentView : MonoBehaviour
	{
		[SerializeField] Image m_EquipThumb;

		public void SetEquipments(List<ILOEquipmentModelBase> equipments)
		{
			// TODO: add multiple equipments
			m_EquipThumb.sprite = equipments[0].EquipmentSprite;
			this.gameObject.SetActive(true);
		}
	}
}

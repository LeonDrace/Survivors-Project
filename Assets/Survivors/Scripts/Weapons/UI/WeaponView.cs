using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Survivors.Weapons
{
	public class WeaponView : MonoBehaviour
	{
		[SerializeField]
		private Image m_IconField;
		[SerializeField]
		private Image m_CooldownField;
		[SerializeField]
		private TextMeshProUGUI m_NameField;

		public Guid Id { get; private set; }

		public void Initialize(Guid id, Sprite sprite, Color color, string name)
		{
			Id = id;
			m_CooldownField.fillAmount = 0;
			m_CooldownField.sprite = sprite;
			m_IconField.sprite = sprite;
			m_IconField.color = color;
			m_NameField.text = name;
		}

		public void UpdateSlider(float value)
		{
			m_CooldownField.fillAmount = value;
		}
	}
}

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

		public WeaponSetting WeaponSetting { get; private set; }

		public void Initialize(WeaponSetting weaponSetting)
		{
			WeaponSetting = weaponSetting;
			m_CooldownField.fillAmount = 0;
			m_CooldownField.sprite = weaponSetting.ProjectileSprite;
			m_IconField.sprite = weaponSetting.ProjectileSprite;
			m_IconField.color = weaponSetting.ProjectileColor;
			m_NameField.text = weaponSetting.WeaponName;
		}

		public void UpdateSlider(float value)
		{
			m_CooldownField.fillAmount = value;
		}
	}
}

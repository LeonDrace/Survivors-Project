using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
	public class WeaponView : MonoBehaviour
	{
		[SerializeField]
		private Image _iconField;
		[SerializeField]
		private Image _cooldownField;
		[SerializeField]
		private TextMeshProUGUI _nameField;

		public WeaponSetting WeaponSetting { get; private set; }

		public void Initialize(WeaponSetting weaponSetting)
		{
			WeaponSetting = weaponSetting;
			_cooldownField.fillAmount = 0;
			_cooldownField.sprite = weaponSetting.ProjectileSprite;
			_iconField.sprite = weaponSetting.ProjectileSprite;
			_iconField.color = weaponSetting.ProjectileColor;
			_nameField.text = weaponSetting.WeaponName;
		}

		public void UpdateSlider(float value)
		{
			_cooldownField.fillAmount = value;
		}
	}
}

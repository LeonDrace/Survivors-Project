using TMPro;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemyKillCounterView : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI m_KilledTextField;

		public TextMeshProUGUI KilledTextField => m_KilledTextField;
	}
}

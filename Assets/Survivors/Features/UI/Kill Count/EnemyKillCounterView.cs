using TMPro;
using UnityEngine;

namespace Survivors.Features.UI.Kill_Count
{
	public class EnemyKillCounterView : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI m_KilledTextField;

		public TextMeshProUGUI KilledTextField => m_KilledTextField;
	}
}

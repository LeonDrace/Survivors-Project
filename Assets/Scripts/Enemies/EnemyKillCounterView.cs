using TMPro;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemyKillCounterView : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _killedTextField;

		public TextMeshProUGUI KilledTextField => _killedTextField;
	}
}

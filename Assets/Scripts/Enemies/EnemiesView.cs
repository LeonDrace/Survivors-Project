using TMPro;
using UniRx;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemiesView : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _killedTextField;

		public TextMeshProUGUI KilledTextField => _killedTextField;
	}
}

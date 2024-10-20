using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

namespace Scripts
{
	public class EnemiesView : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _killedTextField;

		public TextMeshProUGUI KilledTextField => _killedTextField;

		public ReactiveCollection<EnemyView> Enemies { get; private set; } = new ReactiveCollection<EnemyView>();

		public void UpdateEnemies()
		{
			int count = Enemies.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				Enemies[i].OnTick();

				if (Enemies[i].IsDead.Value)
				{
					Enemies[i].DestroySelf();
					Enemies.RemoveAt(i);
				}
			}
		}
	}
}

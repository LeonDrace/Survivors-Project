using Survivors.Data;
using UniRx;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemyFactory
	{
		private readonly PlayerData m_PlayerData;
		private readonly CompositeDisposable m_Disposables;

		public EnemyFactory(PlayerData playerData, CompositeDisposable disposables)
		{
			m_PlayerData = playerData;
			m_Disposables = disposables;
		}

		public EnemyPresenter Create(EnemySettings settings, Vector2 position)
		{
			var view = GameObject.Instantiate(settings.Prefab, position, Quaternion.identity).GetComponent<EnemyView>();
			var model = new EnemyModel(m_PlayerData, settings, m_Disposables);
			return new EnemyPresenter(view, model, m_Disposables);
		}
	}
}

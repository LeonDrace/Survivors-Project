using Survivors.Player;
using UniRx;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemyFactory
	{
		private readonly IPlayerHealthData m_PlayerHealthData;
		private readonly IPlayerTransformData m_PlayerTransformData;
		private readonly CompositeDisposable m_Disposables;

		public EnemyFactory(IPlayerHealthData playerHealthData, IPlayerTransformData playerTransformData, CompositeDisposable disposables)
		{
			m_PlayerHealthData = playerHealthData;
			m_PlayerTransformData = playerTransformData;
			m_Disposables = disposables;
		}

		public EnemyPresenter Create(EnemySettings settings, Vector2 position)
		{
			var view = GameObject.Instantiate(settings.Prefab, position, Quaternion.identity).GetComponent<EnemyView>();
			var model = new EnemyModel(m_PlayerTransformData, m_PlayerHealthData, settings, m_Disposables);
			return new EnemyPresenter(view, model, m_Disposables);
		}
	}
}

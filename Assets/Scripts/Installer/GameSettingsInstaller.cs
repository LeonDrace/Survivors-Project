using Survivors.Enemy;
using UnityEngine;
using Zenject;

namespace Scripts
{
	[CreateAssetMenu]
	public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
	{
		[SerializeField]
		private EnemyModel.Settings[] _enemies;
		[SerializeField]
		private EnemiesPresenter.SpawnSettings _spawnSettings;
		[SerializeField]
		private PlayerModel.PlayerSettings _playerSettings;

		public override void InstallBindings()
		{
			Container.BindInstance(_enemies);
			Container.BindInstance(_spawnSettings);
			Container.BindInstance(_playerSettings);
		}
	}
}

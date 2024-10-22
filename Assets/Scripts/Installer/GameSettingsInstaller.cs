using Survivors.Enemy;
using Survivors.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Survivors.Installer
{
	[CreateAssetMenu]
	public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
	{
		[SerializeField, FormerlySerializedAs("_enemies")]
		private EnemyModel.Settings[] m_Enemies;
		[SerializeField, FormerlySerializedAs("_spawnSettings")]
		private EnemiesPresenter.SpawnSettings m_SpawnSettings;
		[SerializeField, FormerlySerializedAs("_playerSettings")]
		private PlayerModel.PlayerSettings m_PlayerSettings;

		public override void InstallBindings()
		{
			Container.BindInstance(m_Enemies);
			Container.BindInstance(m_SpawnSettings);
			Container.BindInstance(m_PlayerSettings);
		}
	}
}

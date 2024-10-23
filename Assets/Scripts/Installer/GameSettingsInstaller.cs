using Survivors.Enemy;
using Survivors.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Survivors.Installer
{
	[CreateAssetMenu(fileName = "Survivors/GamesettingsInstaller", menuName = "Survivors/Game Settings Installer")]
	public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
	{
		[SerializeField]
		private SpawnSettings m_SpawnSettings;
		[SerializeField]
		private EnemySettings[] m_EnemySettings;
		[SerializeField, FormerlySerializedAs("_playerSettings")]
		private CharacterSettings m_CharacterSettings;

		public override void InstallBindings()
		{
			Container.BindInstance(m_EnemySettings);
			Container.BindInstance(m_SpawnSettings);
			Container.BindInstance(m_CharacterSettings);
		}
	}
}

using Survivors.Enemy;
using Survivors.Scripts.Enemies.Settings;
using Survivors.Scripts.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Survivors.Installer
{
    [CreateAssetMenu(fileName = "Survivors/GamesettingsInstaller", menuName = "Survivors/Game Settings Installer")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private SpawnSettings _spawnSettings;
        [SerializeField] private EnemySettings[] _enemySettings;
        [SerializeField] private CharacterSettings _characterSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_spawnSettings);
            Container.BindInstance(_enemySettings);
            Container.BindInstance(_characterSettings);
        }
    }
}
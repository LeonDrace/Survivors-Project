using Survivors.Features.Enemies.Settings;
using Survivors.Features.Map;
using Survivors.Features.Player;
using UnityEngine;
using Zenject;

namespace Survivors.Features.Installer
{
    [CreateAssetMenu(fileName = "Survivors/GameSettingsInstaller", menuName = "Survivors/Game Settings Installer")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private SpawnSettings _spawnSettings;
        [SerializeField] private EnemySettings[] _enemySettings;
        [SerializeField] private CharacterSettings _characterSettings;
        [SerializeField] private MapSettings _mapSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_mapSettings);
            Container.BindInstance(_spawnSettings);
            Container.BindInstance(_enemySettings);
            Container.BindInstance(_characterSettings);
        }
    }
}
using JetBrains.Annotations;
using Survivors.Features.Contracts;
using Survivors.Features.Enemies.Components;
using Survivors.Features.Enemies.Contexts;
using Survivors.Features.Enemies.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Survivors.Features.Enemies.Core
{
    [UsedImplicitly]
    public sealed class EnemyFactory : IEnemyFactory
    {
        private readonly IEnemyPools _pools;
        private readonly EnemySettings[] _settings;

        public EnemyFactory(
            IEnemyPools enemyPools,
            EnemySettings[] settings)
        {
            _pools = enemyPools;
            _settings = settings;
        }

        private int GetRandomSettingsIndex()
        {
            return Random.Range(0, _settings.Length);
        }

        public EnemyComponentContext CreateEnemy(
            [NotNull] IEnemyComponentManager componentManager,
            Vector2 position)
        {
            var settingsIndex = GetRandomSettingsIndex();
            var settings = _settings[settingsIndex];

            var id = componentManager.GetId;

            var transform = new EnemyTransform(position);
            var vitals = new EnemyVitals(settings.Health);

            var view = _pools.PopEnemyView(settings.ConfigId, position, Quaternion.identity);
            view.Initialize(id,
                componentManager.ChangeHealth,
                (lastPosition) => OnDespawn(componentManager, view, settings.ConfigId, lastPosition));

            return new EnemyComponentContext(transform, vitals, view, settingsIndex);
        }

        private void OnDespawn(
            IEnemyComponentManager componentManager,
            IEnemyView view,
            string configId,
            Vector2 position)
        {
            componentManager.AddParticle(CreateDeathParticle(configId, position));
            _pools.PutEnemyView(configId, view);
        }

        private IParticleManager CreateDeathParticle(string configId, Vector2 position)
        {
            return _pools.PopEnemyDeathParticles(configId, position);
        }
    }
}
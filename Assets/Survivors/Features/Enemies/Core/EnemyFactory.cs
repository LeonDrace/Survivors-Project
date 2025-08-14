using System;
using JetBrains.Annotations;
using Survivors.Features.Contracts;
using Survivors.Features.Enemies.Components;
using Survivors.Features.Enemies.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Survivors.Features.Enemies.Core
{
    [UsedImplicitly]
    public sealed class EnemyFactory : IEnemyFactory
    {
        private readonly IEnemyManager _componentManager;
        private readonly IEnemyPools _pools;
        private readonly EnemySettings[] _settings;

        public EnemyFactory(
            IEnemyManager componentManager,
            IEnemyPools enemyPools,
            EnemySettings[] settings)
        {
            _componentManager = componentManager;
            _pools = enemyPools;
            _settings = settings;
            componentManager.AddSharedData(Array.ConvertAll(_settings, item => (ISharedEnemyData)item));
        }

        public void Create(Vector2 position)
        {
            var settingsIndex = GetRandomSettingsIndex();
            var settings = _settings[settingsIndex];
            var view = _pools.PopEnemyView(settings.ConfigId, position, Quaternion.identity);

            var transform = new EnemyTransform(position);
            var vitals = new EnemyVitals(settings.Health);

            _componentManager.AddEnemy(view, transform, vitals, settingsIndex);
        }

        private int GetRandomSettingsIndex()
        {
            return Random.Range(0, _settings.Length);
        }
    }
}
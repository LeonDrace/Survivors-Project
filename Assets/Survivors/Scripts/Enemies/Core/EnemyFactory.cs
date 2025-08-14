using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Survivors.Scripts.Contracts;
using Survivors.Scripts.Enemies.Components;
using Survivors.Scripts.Enemies.Settings;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Survivors.Scripts.Enemies.Core
{
    [UsedImplicitly]
    public sealed class EnemyFactory : IEnemyFactory
    {
        private readonly IEnemyManager _enemyManager;
        private readonly EnemySettings[] _enemySettings;

        public EnemyFactory(
            IEnemyManager enemyManager,
            EnemySettings[] enemySettings)
        {
            _enemyManager = enemyManager;
            _enemySettings = enemySettings;
            enemyManager.AddSharedData(Array.ConvertAll(_enemySettings, item => (ISharedEnemyData)item));
        }

        public void Create(Vector2 position)
        {
            int settingsIndex = GetRandomSettingsIndex();
            EnemySettings settings = _enemySettings[settingsIndex];
            IEnemyView view = Object.Instantiate(settings.Prefab, position, Quaternion.identity);

            EnemyTransform transform = new EnemyTransform(position);
            EnemyVitals vitals = new EnemyVitals(settings.Health);

            _enemyManager.AddEnemy(view, transform, vitals, settingsIndex);
        }

        private int GetRandomSettingsIndex()
        {
            return Random.Range(0, _enemySettings.Length);
        }
    }
}
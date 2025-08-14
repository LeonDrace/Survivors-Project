using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Survivors.Scripts.Constants;
using Survivors.Scripts.Contracts;
using Survivors.Scripts.Enemies.Settings;
using UnityEngine;

namespace Survivors.Scripts.Enemies.Core
{
    [UsedImplicitly]
    public class EnemyPools : IEnemyPools
    {
        private readonly Dictionary<string, Queue<IEnemyView>> _pools;
        private readonly EnemySettings[] _settings;

        public EnemyPools(EnemySettings[] settings)
        {
            _settings = settings;
            _pools = new Dictionary<string, Queue<IEnemyView>>(settings.Length);

            var count = EnemyConstants.PoolCapacity;

            foreach (var setting in settings)
            {
                var pool = new Queue<IEnemyView>(count);
                FillPool(pool, count, setting);
                _pools.Add(setting.ConfigId, pool);
            }
        }

        public IEnemyView PopEnemyView(string configId)
        {
            if (!_pools.TryGetValue(configId, out var pool))
                throw new ArgumentOutOfRangeException(nameof(configId), "Pool doesn't exist");

            if (pool.Count <= 0) Resize(pool, GetSetting(configId));

            var view = pool.Dequeue();
            view.SetState(true);
            return view;
        }

        public IEnemyView PopEnemyView(string configId, Vector2 position, Quaternion rotation)
        {
            var view = PopEnemyView(configId);
            view.UpdatePositionAndRotation(position, rotation);
            return view;
        }

        private EnemySettings GetSetting(string configId)
        {
            foreach (var setting in _settings)
                if (setting.ConfigId == configId)
                    return setting;

            return null;
        }

        public void PutEnemyView(string configId, IEnemyView enemyView)
        {
            if (_pools.TryGetValue(configId, out var pool))
            {
                enemyView.SetState(false);
                pool.Enqueue(enemyView);
                return;
            }

            enemyView.Dispose();
        }

        private void Resize(Queue<IEnemyView> pool, EnemySettings setting)
        {
            FillPool(pool, EnemyConstants.PoolCapacity, setting);
        }

        private void FillPool(Queue<IEnemyView> pool, int count, EnemySettings setting)
        {
            for (var i = 0; i < count; i++)
            {
                IEnemyView view = UnityEngine.Object.Instantiate(setting.Prefab);
                view.SetState(false);
                pool.Enqueue(view);
            }
        }
    }
}
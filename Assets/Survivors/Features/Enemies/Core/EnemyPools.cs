using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Survivors.Features.Constants;
using Survivors.Features.Contracts;
using Survivors.Features.Enemies.Settings;
using UniRx;
using UnityEngine;

namespace Survivors.Features.Enemies.Core
{
    //Todo: improve pooling
    [UsedImplicitly]
    public class EnemyPools : IEnemyPools, IDisposable
    {
        private readonly Dictionary<string, Queue<IEnemyView>> _viewPools;
        private readonly Dictionary<string, Queue<IParticleManager>> _deathParticlePools;
        private readonly EnemySettings[] _settings;

        public EnemyPools(EnemySettings[] settings, CompositeDisposable compositeDisposable)
        {
            compositeDisposable.Add(this);

            _settings = settings;
            _viewPools = new Dictionary<string, Queue<IEnemyView>>(settings.Length);

            var count = EnemyConstants.EnemyViewPoolCapacity;

            foreach (var setting in settings)
            {
                var pool = new Queue<IEnemyView>(count);
                FillViewPool(pool, count, setting);
                _viewPools.Add(setting.ConfigId, pool);
            }

            _deathParticlePools = new Dictionary<string, Queue<IParticleManager>>(settings.Length);

            count = EnemyConstants.EnemyParticlePoolCapacity;

            foreach (var setting in settings)
            {
                var pool = new Queue<IParticleManager>(count);
                FillDeathParticlesPool(pool, count, setting);
                _deathParticlePools.Add(setting.ConfigId, pool);
            }
        }

        #region Internal

        private EnemySettings GetSetting(string configId)
        {
            for (var i = 0; i < _settings.Length; i++)
                if (_settings[i].ConfigId == configId)
                    return _settings[i];

            return null;
        }

        public void Dispose()
        {
            foreach (var pool in _viewPools)
            foreach (var item in pool.Value)
                item.Dispose();

            foreach (var pool in _deathParticlePools)
            foreach (var item in pool.Value)
                item.Dispose();

            _viewPools.Clear();
            _deathParticlePools.Clear();
        }

        #endregion


        #region Enemy View

        public IEnemyView PopEnemyView(string configId)
        {
            if (!_viewPools.TryGetValue(configId, out var pool))
                throw new ArgumentOutOfRangeException(nameof(configId), "Pool doesn't exist");

            if (pool.Count <= 0) ResizeViewPool(pool, GetSetting(configId));

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


        public void PutEnemyView(string configId, IEnemyView enemyView)
        {
            if (!_viewPools.TryGetValue(configId, out var pool))
                throw new ArgumentOutOfRangeException(nameof(configId), "Pool doesn't exist");

            enemyView.SetState(false);
            pool.Enqueue(enemyView);
        }

        private void ResizeViewPool(Queue<IEnemyView> pool, EnemySettings setting)
        {
            FillViewPool(pool, EnemyConstants.EnemyViewPoolCapacity, setting);
        }

        private void FillViewPool(Queue<IEnemyView> pool, int count, EnemySettings setting)
        {
            for (var i = 0; i < count; i++)
            {
                IEnemyView view = UnityEngine.Object.Instantiate(setting.Prefab);
                view.SetState(false);
                pool.Enqueue(view);
            }
        }

        #endregion

        #region Death Particles

        public IParticleManager PopEnemyDeathParticles(string configId, Vector2 position)
        {
            if (!_deathParticlePools.TryGetValue(configId, out var pool))
                throw new ArgumentOutOfRangeException(nameof(configId), "Pool doesn't exist");

            if (pool.Count <= 0) ResizeDeathParticles(pool, GetSetting(configId));

            var particles = pool.Dequeue();
            particles.SetState(true);
            particles.StartParticles(position);
            return particles;
        }

        public void PutEnemyDeathParticles(string configId, IParticleManager particles)
        {
            if (!_deathParticlePools.TryGetValue(configId, out var pool))
                throw new ArgumentOutOfRangeException(nameof(configId), "Pool doesn't exist");

            particles.SetState(false);
            pool.Enqueue(particles);
        }

        private void ResizeDeathParticles(Queue<IParticleManager> pool, EnemySettings setting)
        {
            FillDeathParticlesPool(pool, EnemyConstants.EnemyParticlePoolCapacity, setting);
        }

        private void FillDeathParticlesPool(Queue<IParticleManager> pool, int count, EnemySettings setting)
        {
            for (var i = 0; i < count; i++)
            {
                var particles = UnityEngine.Object.Instantiate(setting.DeathParticles);
                particles.Initialize(() => PutEnemyDeathParticles(setting.ConfigId, particles));
                particles.SetState(false);
                pool.Enqueue(particles);
            }
        }

        #endregion
    }
}
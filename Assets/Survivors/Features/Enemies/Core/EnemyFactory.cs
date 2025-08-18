using JetBrains.Annotations;
using Survivors.Features.Constants;
using Survivors.Features.Contracts;
using Survivors.Features.Enemies.Components;
using Survivors.Features.Enemies.Contexts;
using Survivors.Features.Enemies.Settings;
using Survivors.Features.Pooling;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Survivors.Features.Enemies.Core
{
    [UsedImplicitly]
    public sealed class EnemyFactory : IEnemyFactory
    {
        private readonly IEnemyComponentManager _componentManager;
        private readonly EnemySettings[] _settings;
        private readonly MultiPool<IEnemyView> _viewPools;
        private readonly MultiPool<IParticleManager> _particlePools;

        public EnemyFactory(
            IEnemyComponentManager componentManager,
            EnemySettings[] settings,
            CompositeDisposable compositeDisposable)
        {
            _componentManager = componentManager;
            _settings = settings;

            // Create enemy view pools
            var capacity = EnemyConstants.EnemyViewPoolCapacity;
            var poolsCount = _settings.Length;
            _viewPools = new MultiPool<IEnemyView>(poolsCount);
            for (var i = 0; i < settings.Length; i++)
            {
                var index = i;
                var configId = settings[index].ConfigId;
                _viewPools.Add(settings[i].ConfigId, settings[i].Prefab.gameObject, capacity,
                    (view, pool) => { OnEnemyCreated(view, pool, configId); });
            }


            // Create death particle pool
            capacity = EnemyConstants.EnemyParticlePoolCapacity;
            _particlePools = new MultiPool<IParticleManager>(poolsCount);
            for (var i = 0; i < settings.Length; i++)
                _particlePools.Add(settings[i].ConfigId, settings[i].DeathParticles.gameObject, capacity,
                    OnDeathParticleCreated);

            compositeDisposable.Add(_viewPools);
            compositeDisposable.Add(_particlePools);
            return;

            void OnEnemyCreated(IEnemyView view, Pool<IEnemyView> pool, string configId)
            {
                view.Initialize(
                    _componentManager.ChangeHealth,
                    (lastPosition) =>
                    {
                        _componentManager.AddParticle(CreateDeathParticle(configId, lastPosition));
                        pool.Despawn(view.Transform.gameObject);
                    });
            }

            void OnDeathParticleCreated(IParticleManager particleManager, Pool<IParticleManager> pool)
            {
                particleManager.Initialize(() => pool.Despawn(particleManager.GameObject));
            }
        }

        public EnemyComponentContext CreateEnemy(Vector2 position)
        {
            var settingsIndex = Random.Range(0, _settings.Length);
            var settings = _settings[settingsIndex];

            var id = _componentManager.GetId;
            var transform = new EnemyTransform(position);
            var vitals = new EnemyVitals(settings.Health);
            var view = _viewPools.Spawn(settings.ConfigId, position, Quaternion.identity);
            view.Id = id;

            return new EnemyComponentContext(transform, vitals, view, settingsIndex);
        }

        private IParticleManager CreateDeathParticle(string key, Vector2 position)
        {
            var particle = _particlePools.Spawn(key, position, Quaternion.identity);
            particle.StartParticles(position);
            return particle;
        }
    }
}
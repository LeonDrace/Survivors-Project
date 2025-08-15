using System;
using JetBrains.Annotations;
using Survivors.Features.Constants;
using Survivors.Features.Contracts;
using Survivors.Features.Enemies.Components;
using Survivors.Features.Enemies.Contexts;
using Survivors.Features.Enemies.Settings;
using UniRx;
using UnityEngine;
using Zenject;

namespace Survivors.Features.Enemies.Core
{
    [UsedImplicitly]
    public class EnemyComponentManager : ITickable, IEnemyComponentManager, IEnemies, IDisposable
    {
        private readonly Transform _playerTransform;
        private readonly IPlayerHealthData _playerHealthData;
        private readonly ISharedEnemyData[] _sharedEnemyData;

        // Enemy core components
        private int[] _sharedDataIndex;
        private EnemyTransform[] _transforms;
        private EnemyVitals[] _vitals;
        private float[] _attackCooldowns;
        private IEnemyView[] _views;

        // Dynamic Components
        private IParticleManager[] _particles;
        private int _particlesCount;

        private int _capacity;
        private int _count;
        private Vector2 _currentPlayerPosition;

        public int Count => _count;
        public int GetId => _count;
        public ReactiveProperty<int> KilledEnemies { get; }

        public EnemyComponentManager(
            EnemySettings[] settings,
            IPlayerTransformData playerTransformData,
            IPlayerHealthData playerHealthData,
            CompositeDisposable compositeDisposable)
        {
            _sharedEnemyData = Array.ConvertAll(settings, item => (ISharedEnemyData)item);
            _playerTransform = playerTransformData.Transform;
            _playerHealthData = playerHealthData;
            compositeDisposable.Add(this);

            _capacity = EnemyConstants.ComponentsCapacity;

            _transforms = new EnemyTransform[_capacity];
            _vitals = new EnemyVitals[_capacity];
            _views = new IEnemyView[_capacity];
            _sharedDataIndex = new int[_capacity];
            _attackCooldowns = new float[_capacity];
            _particles = new IParticleManager[_capacity];
            KilledEnemies = new ReactiveProperty<int>(0);
        }

        #region Internal

        private void Resize()
        {
            _capacity *= 2;
            Array.Resize(ref _transforms, _capacity);
            Array.Resize(ref _vitals, _capacity);
            Array.Resize(ref _views, _capacity);
            Array.Resize(ref _sharedDataIndex, _capacity);
            Array.Resize(ref _attackCooldowns, _capacity);
        }

        private ISharedEnemyData GetSharedData(int index)
        {
            return _sharedEnemyData[_sharedDataIndex[index]];
        }

        #endregion

        #region Update

        public void Tick()
        {
            _currentPlayerPosition = _playerTransform.position;
            UpdateEnemies();
            UpdateParticles();
        }

        private void UpdateEnemies()
        {
            for (var i = _count - 1; i >= 0; i--)
            {
                if (IsDead(i))
                {
                    KilledEnemies.Value++;
                    RemoveEnemy(i);
                    continue;
                }

                UpdateTransform(i);
                UpdateView(i);
                UpdateAttack(i);
            }
        }

        private void UpdateTransform(int index)
        {
            var delta = GetSharedData(index).Speed * Time.deltaTime;
            var position = _transforms[index].Position;

            //Look at only using z-axis
            var dir = _currentPlayerPosition - position;
            var rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var newRotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

            position = Vector2.MoveTowards(position, _currentPlayerPosition, delta);

            _transforms[index].Populate(position, newRotation);
        }

        private void UpdateView(int index)
        {
            _views[index].OnTick();
            _views[index].UpdatePositionAndRotation(_transforms[index].Position, _transforms[index].Rotation);
        }

        private bool IsDead(int index)
        {
            return _vitals[index].CurrentHealth <= 0;
        }

        #endregion

        #region Add / Remove

        public void AddEnemy(in EnemyComponentContext context)
        {
            if (_count == _capacity)
                Resize();

            _sharedDataIndex[_count] = context.DataIndex;
            _transforms[_count] = context.Transform;
            _vitals[_count] = context.Vitals;
            _views[_count] = context.View;
            _attackCooldowns[_count] = 0f;

            _count++;
        }

        public void RemoveEnemy(int id)
        {
            _views[id].OnDespawn();

            _views[id] = _views[_count - 1];
            _vitals[id] = _vitals[_count - 1];
            _transforms[id] = _transforms[_count - 1];
            _sharedDataIndex[id] = _sharedDataIndex[_count - 1];
            _attackCooldowns[id] = _attackCooldowns[_count - 1];

            _views[id].Id = id;

            _count--;
        }

        public void Clear()
        {
            _count = 0;
            _particlesCount = 0;
            foreach (var view in _views) view?.Dispose();
            foreach (var particle in _particles) particle?.Dispose();
        }

        #endregion

        #region Health

        public void ChangeHealth(int id, float change)
        {
            var newHealth = _vitals[id].CurrentHealth + change;
            newHealth = Math.Clamp(newHealth, 0f, _vitals[id].MaxHealth);
            _vitals[id].CurrentHealth = newHealth;
        }

        #endregion

        #region Attack

        private void UpdateAttack(int index)
        {
            var data = GetSharedData(index);

            if (_attackCooldowns[index] > 0)
            {
                _attackCooldowns[index] -= Time.deltaTime;
                return;
            }

            if (!(Vector2.Distance(_currentPlayerPosition, _views[index].Transform.position) <=
                  data.AttackRange)) return;

            _playerHealthData.ChangeHealth(-data.Damage);
            _attackCooldowns[index] = data.AttackSpeed;
        }

        #endregion

        #region Update Particles

        public void AddParticle(IParticleManager particleManager)
        {
            if (_particles.Length == _particlesCount)
                ResizeParticles();

            _particles[_particlesCount] = particleManager;
            _particlesCount++;
        }

        private void UpdateParticles()
        {
            for (var i = _particlesCount - 1; i >= 0; i--)
            {
                if (_particles[i].IsPlaying) continue;

                _particles[i].Despawn();
                _particles[i] = _particles[_particlesCount - 1];
                _particlesCount--;
            }
        }

        private void ResizeParticles()
        {
            var capacity = _particles.Length * 2;
            Array.Resize(ref _particles, capacity);
        }

        #endregion

        public void Dispose()
        {
            Clear();
        }
    }
}
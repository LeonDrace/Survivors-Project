using System;
using JetBrains.Annotations;
using Survivors.Scripts.Contracts;
using Survivors.Scripts.Enemies.Components;
using UniRx;
using UnityEngine;
using Zenject;

namespace Survivors.Scripts.Enemies.Core
{
    [UsedImplicitly]
    public class EnemyComponentManager : ITickable, IEnemyManager, IEnemies
    {
        private readonly Transform _playerTransform;
        private readonly IPlayerHealthData _playerHealthData;

        private int[] _sharedDataIndex;
        private ISharedEnemyData[] _sharedEnemyData;

        private EnemyTransform[] _transforms;
        private EnemyVitals[] _vitals;
        private float[] _attackCooldowns;
        private IEnemyView[] _views;


        private const int Capacity = 128;
        private int _capacity;
        private int _count;
        private Vector2 _currentPlayerPosition;

        public int Count => _count;
        public ReactiveProperty<int> KilledEnemies { get; }

        public EnemyComponentManager(
            IPlayerTransformData playerTransformData,
            IPlayerHealthData playerHealthData)
        {
            _playerTransform = playerTransformData.Transform;
            _playerHealthData = playerHealthData;

            _capacity = Capacity;

            _transforms = new EnemyTransform[_capacity];
            _vitals = new EnemyVitals[_capacity];
            _views = new IEnemyView[_capacity];
            _sharedDataIndex = new int[_capacity];
            _attackCooldowns = new float[_capacity];
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

        private void UpdateView(int i)
        {
            _views[i].OnTick();
            _views[i].UpdatePositionAndRotation(_transforms[i].Position, _transforms[i].Rotation);
        }

        private bool IsDead(int i)
        {
            return _vitals[i].CurrentHealth <= 0;
        }

        #endregion

        #region Add / Remove

        public void AddSharedData(ISharedEnemyData[] sharedData)
        {
            _sharedEnemyData = sharedData;
        }

        public void AddEnemy(IEnemyView view, in EnemyTransform transform, in EnemyVitals vitals, int sharedDataIndex)
        {
            if (_count == _capacity)
                Resize();

            view.Initialize(this, _count);

            _sharedDataIndex[_count] = sharedDataIndex;
            _transforms[_count] = transform;
            _vitals[_count] = vitals;
            _views[_count] = view;
            _attackCooldowns[_count] = 0f;

            _count++;
        }

        public void RemoveEnemy(int index)
        {
            _views[index].Dispose();

            _views[index] = _views[_count - 1];
            _vitals[index] = _vitals[_count - 1];
            _transforms[index] = _transforms[_count - 1];
            _sharedDataIndex[index] = _sharedDataIndex[_count - 1];
            _attackCooldowns[index] = _attackCooldowns[_count - 1];

            _views[index].Index = index;

            _count--;
        }

        public void Clear()
        {
            _count = 0;
            foreach (var view in _views) view?.Dispose();
        }

        #endregion

        #region Health

        public void ChangeHealth(int index, float change)
        {
            var newHealth = _vitals[index].CurrentHealth + change;
            newHealth = Math.Clamp(newHealth, 0f, _vitals[index].MaxHealth);
            _vitals[index].CurrentHealth = newHealth;
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
    }
}
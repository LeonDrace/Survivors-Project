using Survivors.Enemy;
using Survivors.Scripts.Contracts;
using Survivors.Scripts.Enemies.Enemy;
using UniRx;
using UnityEngine;
using Zenject;

namespace Survivors.Scripts.Enemies
{
    public sealed class EnemiesContainer : IEnemies, ITickable
    {
        private readonly Camera m_Camera;
        private readonly EnemyFactory m_EnemyFactory;
        private readonly EnemySettings[] m_EnemySettings;
        private readonly SpawnSettings m_SpawnSettings;

        private float _randomSpawnCooldown;

        public EnemiesContainer(
            SpawnSettings spawnSettings,
            EnemySettings[] enemySettings,
            EnemyFactory factory)
        {
            m_SpawnSettings = spawnSettings;
            m_EnemySettings = enemySettings;
            m_EnemyFactory = factory;
            m_Camera = Camera.main;
        }

        public ReactiveProperty<int> KilledEnemies { get; } = new(0);
        public ReactiveCollection<IEnemy> Enemies { get; } = new();

        public void Tick()
        {
            SpawnerTick();
            EnemiesTick();
        }

        private void EnemiesTick()
        {
            var count = Enemies.Count;
            for (var i = count - 1; i >= 0; i--)
            {
                var enemy = Enemies[i];
                enemy.OnTick();

                if (enemy.IsDead())
                {
                    enemy.Destroy();
                    KilledEnemies.Value++;
                    Enemies.RemoveAt(i);
                }
            }
        }

        private void SpawnerTick()
        {
            _randomSpawnCooldown -= Time.deltaTime;

            if (!(_randomSpawnCooldown <= 0)) return;

            _randomSpawnCooldown = m_SpawnSettings.GetRandomSpawnCooldown();
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            if (Enemies.Count >= m_SpawnSettings.MaxSpawnAmount) return;

            var amount = m_SpawnSettings.GetSpawnAmount();
            for (var i = 0; i < amount; i++)
                if (SpawnLogic.TrySpawnEnemy(m_Camera, m_EnemyFactory, GetRandomSettings(), out var enemy))
                    Enemies.Add(enemy);
                else
                    break;
        }

        private EnemySettings GetRandomSettings()
        {
            return m_EnemySettings[Random.Range(0, m_EnemySettings.Length)];
        }
    }
}
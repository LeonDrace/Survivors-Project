using JetBrains.Annotations;
using Survivors.Features.Contracts;
using Survivors.Features.Enemies.Settings;
using UnityEngine;
using Zenject;

namespace Survivors.Features.Enemies.Core
{
    [UsedImplicitly]
    public class EnemySpawner : ITickable
    {
        private readonly SpawnSettings _spawnSettings;
        private readonly IEnemyManager _enemyManager;
        private readonly IEnemyFactory _enemyFactory;
        private readonly Camera _camera;

        private float _cooldown;

        public EnemySpawner(
            SpawnSettings spawnSettings,
            IEnemyManager enemyManager,
            IEnemyFactory enemyFactory)
        {
            _spawnSettings = spawnSettings;
            _enemyManager = enemyManager;
            _enemyFactory = enemyFactory;
            _camera = Camera.main;
        }

        public void Tick()
        {
            if (CanSpawn())
            {
                SpawnEnemies();
            }
        }

        private bool CanSpawn()
        {
            _cooldown -= Time.deltaTime;

            if (_cooldown <= 0)
            {
                _cooldown = _spawnSettings.GetRandomSpawnCooldown();
                return true;
            }

            return false;
        }

        private void SpawnEnemies()
        {
            if (_enemyManager.Count >= _spawnSettings.MaxSpawnAmount) return;

            int amount = _spawnSettings.GetSpawnAmount();
            for (var i = 0; i < amount; i++)
            {
                if (TrySpawnEnemy(out Vector2 spawnPosition))
                {
                    _enemyFactory.Create(spawnPosition);
                }
            }
        }

        private bool TrySpawnEnemy(out Vector2 spawnPosition)
        {
            Vector2 position = GetRandomScreenBorderPointAsWorldPosition(
                _camera,
                _spawnSettings.SpawnOffset.x,
                _spawnSettings.SpawnOffset.y);
            //Todo: add map size and use to check if inside

            spawnPosition = position;
            return true;
        }

        private static Vector3 GetRandomScreenBorderPointAsWorldPosition(
            Camera camera,
            float widthOffset = 0,
            float heightOffset = 0)
        {
            var width = Screen.width;
            var height = Screen.height;
            var randomBorder = Random.Range(0, 4);

            return randomBorder switch
            {
                0 => camera.ScreenToWorldPoint(new Vector3(Random.Range(-widthOffset, width + widthOffset),
                    -heightOffset, 0)),
                1 => camera.ScreenToWorldPoint(new Vector3(Random.Range(-widthOffset, width + widthOffset),
                    height + heightOffset, 0)),
                2 => camera.ScreenToWorldPoint(new Vector3(-widthOffset,
                    Random.Range(-heightOffset, height + heightOffset), 0)),
                3 => camera.ScreenToWorldPoint(new Vector3(width + heightOffset,
                    Random.Range(heightOffset, height + heightOffset), 0)),
                _ => Vector3.zero
            };
        }
    }
}
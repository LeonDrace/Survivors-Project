using Survivors.Scripts.Enemies.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Survivors.Scripts.Enemies
{
    public static class SpawnLogic
    {
        public static bool TrySpawnEnemy(Camera camera, EnemyFactory enemyFactory, EnemySettings enemySettings,
            out EnemyPresenter enemy)
        {
            Vector2 position = GetRandomScreenBorderPointAsWorldPosition(camera, 100, 100);
            if (NavMesh.SamplePosition(position, out var hit, 1, NavMesh.AllAreas))
            {
                enemy = enemyFactory.Create(enemySettings, position);
                return true;
            }

            enemy = null;
            return false;
        }

        private static Vector2 GetRandomPointOnRadius(Vector2 position, float radius)
        {
            return Vector2.one * radius * Random.onUnitSphere + position;
        }

        private static Vector3 GetRandomScreenBorderPointAsWorldPosition(Camera camera, float widthOffset = 0,
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
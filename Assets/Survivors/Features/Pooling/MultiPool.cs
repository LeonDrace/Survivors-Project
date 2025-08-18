using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Survivors.Features.Pooling
{
    public sealed class MultiPool<T> : IDisposable
    {
        private readonly Dictionary<string, Pool<T>> _pools;

        public MultiPool(int capacity)
        {
            _pools = new Dictionary<string, Pool<T>>(capacity);
        }

        public void Add(string key, GameObject prefab, int capacity, [CanBeNull] Action<T, Pool<T>> onAdd = null)
        {
            Pool<T> pool = new(prefab, capacity, onAdd);
            _pools.Add(key, pool);
        }

        public T Spawn(string key, Vector2 position, Quaternion rotation)
        {
            if (!_pools.TryGetValue(key, out var pool))
                throw new ArgumentOutOfRangeException(nameof(key), "Pool doesn't exist");

            return pool.Spawn(position, rotation);
        }

        public void Despawn(string key, GameObject gameObject)
        {
            if (!_pools.TryGetValue(key, out var pool))
                throw new ArgumentOutOfRangeException(nameof(key), "Pool doesn't exist");

            pool.Despawn(gameObject);
        }

        public void Dispose()
        {
            foreach (var pool in _pools) pool.Value.Dispose();
        }
    }
}
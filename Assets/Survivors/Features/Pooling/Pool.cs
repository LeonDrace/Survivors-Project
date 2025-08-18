using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Survivors.Features.Pooling
{
    public class Pool<T> : IDisposable
    {
        private readonly Queue<GameObject> _pool;
        private readonly GameObject _prefab;
        private Action<T, Pool<T>> _onAdd;

        public Pool(GameObject prefab, int capacity, [CanBeNull] Action<T, Pool<T>> onAdd = null)
        {
            _onAdd = onAdd;
            _prefab = prefab;
            _pool = new Queue<GameObject>(capacity);
            for (var i = 0; i < capacity; i++)
                AddPoolItem(onAdd);
        }

        public T Spawn()
        {
            var item = GetOrCreateGameObject();
            item.SetActive(true);
            return item.GetComponent<T>();
        }

        public T Spawn(Vector2 position, Quaternion rotation)
        {
            var item = GetOrCreateGameObject();
            item.transform.SetPositionAndRotation(position, rotation);
            item.SetActive(true);
            return item.GetComponent<T>();
        }

        private GameObject GetOrCreateGameObject()
        {
            if (_pool.Count <= 0)
                AddPoolItem(_onAdd);

            var item = _pool.Dequeue();
            return item;
        }

        public void Despawn(GameObject gameObject)
        {
            gameObject.SetActive(false);
            _pool.Enqueue(gameObject);
        }

        private void AddPoolItem(Action<T, Pool<T>> onAdd = null)
        {
            var item = Object.Instantiate(_prefab);
            var component = item.GetComponent<T>();
            onAdd?.Invoke(component, this);
            item.SetActive(false);
            _pool.Enqueue(item);
        }

        public void Dispose()
        {
            _onAdd = null;
        }
    }
}
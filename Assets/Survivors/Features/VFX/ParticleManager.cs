using System;
using JetBrains.Annotations;
using Survivors.Features.Contracts;
using UnityEngine;

namespace Survivors.Features.VFX
{
    public class ParticleManager : MonoBehaviour, IParticleManager
    {
        [SerializeField] [NotNull] private ParticleSystem _particleSystem;

        private Action _onParticleDespawn;

        public bool IsPlaying => _particleSystem.isPlaying;
        public GameObject GameObject => gameObject;

        public void Initialize([NotNull] Action onParticleDespawn)
        {
            _onParticleDespawn = onParticleDespawn;
        }

        public void StartParticles(Vector2 position)
        {
            transform.position = position;
            _particleSystem.Play();
        }

        public void StopParticles()
        {
            if (IsPlaying)
                _particleSystem.Stop();
        }

        public void SetState(bool state)
        {
            if (gameObject.activeSelf != state)
                gameObject.SetActive(state);
        }

        public void Despawn()
        {
            StopParticles();
            _onParticleDespawn?.Invoke();
        }

        public void Dispose()
        {
            _onParticleDespawn = null;
        }
    }
}
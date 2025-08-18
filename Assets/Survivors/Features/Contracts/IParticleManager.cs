using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Survivors.Features.Contracts
{
    public interface IParticleManager
    {
        bool IsPlaying { get; }
        GameObject GameObject { get; }

        void Initialize([NotNull] Action onParticleDespawn);
        void StartParticles(Vector2 position);
        void StopParticles();

        void SetState(bool state);

        void Despawn();
        void Dispose();
    }
}
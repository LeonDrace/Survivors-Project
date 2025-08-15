using UnityEngine;

namespace Survivors.Features.Contracts
{
    public interface IParticleManager
    {
        bool IsPlaying { get; }

        void StartParticles(Vector2 position);
        void StopParticles();

        void SetState(bool state);

        void Despawn();
        void Dispose();
    }
}
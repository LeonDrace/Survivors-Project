using Survivors.Features.Contracts;
using UniRx;

namespace Survivors.Features.UI.Restart_Panel
{
    public class RestartScreenModel
    {
        private readonly IPlayerHealthData m_PlayerHealthData;

        public ReactiveProperty<bool> IsDead => m_PlayerHealthData.IsDead;

        public RestartScreenModel(IPlayerHealthData playerHealthData)
        {
            m_PlayerHealthData = playerHealthData;
        }
    }
}
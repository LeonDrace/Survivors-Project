namespace Survivors.Features.Enemies.Components
{
    public struct EnemyVitals
    {
        public float CurrentHealth;
        public float MaxHealth;

        public EnemyVitals(float maxHealth)
        {
            CurrentHealth = maxHealth;
            MaxHealth = maxHealth;
        }

        public float PercentageHealth => CurrentHealth / MaxHealth;
    }
}
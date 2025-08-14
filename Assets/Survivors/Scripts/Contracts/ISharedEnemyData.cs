namespace Survivors.Scripts.Contracts
{
    public interface ISharedEnemyData
    {
        float AttackRange { get; }
        float Damage { get; }
        float AttackSpeed { get; }
        float Speed { get; }
    }
}
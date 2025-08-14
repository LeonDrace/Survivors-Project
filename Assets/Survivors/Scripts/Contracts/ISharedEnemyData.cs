namespace Survivors.Scripts.Contracts
{
    public interface ISharedEnemyData
    {
        string ConfigId { get; }
        float AttackRange { get; }
        float Damage { get; }
        float AttackSpeed { get; }
        float Speed { get; }
    }
}
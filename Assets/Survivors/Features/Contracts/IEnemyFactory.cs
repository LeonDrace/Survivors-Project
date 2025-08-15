using Survivors.Features.Enemies.Components;
using Survivors.Features.Enemies.Contexts;
using Survivors.Features.Enemies.Core;
using UnityEngine;

namespace Survivors.Features.Contracts
{
    public interface IEnemyFactory
    {
        EnemyComponentContext CreateEnemy(IEnemyComponentManager componentComponentManager, Vector2 position);
    }
}
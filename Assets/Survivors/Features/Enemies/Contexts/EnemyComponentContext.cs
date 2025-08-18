using Survivors.Features.Contracts;
using Survivors.Features.Enemies.Components;

namespace Survivors.Features.Enemies.Contexts
{
    public struct EnemyComponentContext
    {
        public readonly EnemyTransform Transform;
        public readonly EnemyVitals Vitals;
        public readonly IEnemyView View;
        public readonly int DataIndex;

        public EnemyComponentContext(
            EnemyTransform transform,
            EnemyVitals vitals,
            IEnemyView view,
            int dataIndex)
        {
            Transform = transform;
            Vitals = vitals;
            View = view;
            DataIndex = dataIndex;
        }
    }
}
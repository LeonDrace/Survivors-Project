using JetBrains.Annotations;
using UniRx;

namespace Survivors.Features.UI.Kill_Count
{
    [UsedImplicitly]
    public class EnemyKillCounterPresenter
    {
        public EnemyKillCounterPresenter(EnemyKillCounterModel model, EnemyKillCounterView view,
            CompositeDisposable disposables)
        {
            model.KilledEnemies
                .Subscribe(value => view.KilledTextField.text = value.ToString())
                .AddTo(disposables);
        }
    }
}
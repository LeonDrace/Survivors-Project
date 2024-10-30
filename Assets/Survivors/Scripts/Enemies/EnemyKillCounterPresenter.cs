using Survivors.Data;
using UniRx;

namespace Survivors.Enemy
{
	public class EnemyKillCounterPresenter
	{
		public EnemyKillCounterPresenter(EnemyData model, EnemyKillCounterView view, CompositeDisposable disposables)
		{
			model.Enemies
				.ObserveRemove()
				.Subscribe(_ => model.KilledEnemies.Value++)
				.AddTo(disposables);
			
			model.KilledEnemies
				.Subscribe(value => view.KilledTextField.text = value.ToString())
				.AddTo(disposables);
		}
	}
}

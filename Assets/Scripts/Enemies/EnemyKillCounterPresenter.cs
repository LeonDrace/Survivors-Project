using Survivors.Data;
using UniRx;

namespace Survivors.Enemy
{
	public class EnemyKillCounterPresenter
	{
		private readonly EnemyKillCounterView _view;
		private readonly EnemyData _model;

		public EnemyKillCounterPresenter(EnemyData data, EnemyKillCounterView view, CompositeDisposable disposables)
		{
			_model = data;
			_view = view;

			//Update killed enemies.
			_model.Enemies
				.ObserveRemove()
				.Subscribe(_ => _model.KilledEnemies.Value++)
				.AddTo(disposables);

			//Update killed enemy counter text.
			_model.KilledEnemies
				.Subscribe(value => _view.KilledTextField.text = value.ToString())
				.AddTo(disposables);
		}

	}
}

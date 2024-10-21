using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Survivors.Enemy
{
	public class EnemyKillCounterPresenter
	{
		private readonly EnemyKillCounterView _view;
		private readonly EnemiesModel _model;

		public EnemyKillCounterPresenter(EnemiesModel model, EnemyKillCounterView view, CompositeDisposable disposables)
		{
			_model = model;
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

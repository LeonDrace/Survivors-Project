using Survivors.Data;
using UniRx;

namespace Survivors.Enemy
{
	public class EnemyKillCounterPresenter
	{
		private readonly EnemyKillCounterView m_View;
		private readonly EnemyData m_Model;

		public EnemyKillCounterPresenter(EnemyData model, EnemyKillCounterView view, CompositeDisposable disposables)
		{
			m_Model = model;
			m_View = view;

			//Update killed enemies.
			m_Model.Enemies
				.ObserveRemove()
				.Subscribe(_ => m_Model.KilledEnemies.Value++)
				.AddTo(disposables);

			//Update killed enemy counter text.
			m_Model.KilledEnemies
				.Subscribe(value => m_View.KilledTextField.text = value.ToString())
				.AddTo(disposables);
		}

	}
}

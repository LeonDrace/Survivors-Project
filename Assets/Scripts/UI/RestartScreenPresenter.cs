using UniRx;

namespace Survivors.UI
{
	public class RestartScreenPresenter
	{
		private readonly RestartScreenModel m_Model;
		private readonly RestartScreenView m_View;

		public RestartScreenPresenter(RestartScreenModel model, RestartScreenView view, CompositeDisposable disposables)
		{
			m_Model = model;
			m_View = view;

			m_Model.IsDead
				.Where(isDead => isDead == true)
				.Subscribe(x =>
				{
					m_View.RestartScreen.gameObject.SetActive(true);
				})
				.AddTo(disposables);
		}
	}
}

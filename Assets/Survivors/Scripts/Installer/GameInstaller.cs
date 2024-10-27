using Survivors.Extensions;
using Survivors.Player;
using UniRx;
using Zenject;

namespace Survivors.Installer
{
	public class GameInstaller : MonoInstaller<GameInstaller>
	{
		private readonly CompositeDisposable m_Disposer = new();

		public override void InstallBindings()
		{
			Container.BindInstance(m_Disposer);
			Container.InstallGameState();
			Container.InstallPlayer();
			Container.InstallEnemy();
			Container.InstallRestart();
		}

		private void OnDestroy()
		{
			m_Disposer.Dispose();
		}
	}
}

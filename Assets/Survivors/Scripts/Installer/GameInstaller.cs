using Survivors.Extensions;
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
			Container.InstallPlayer();
			Container.InstallWeapons();
			Container.InstallEnemy();
			Container.InstallRestart();
		}

		private void OnDestroy()
		{
			m_Disposer.Dispose();
		}
	}
}

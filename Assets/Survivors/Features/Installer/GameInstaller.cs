using Survivors.Features.Extensions;
using UniRx;
using Zenject;

namespace Survivors.Features.Installer
{
	public class GameInstaller : MonoInstaller<GameInstaller>
	{
		private readonly CompositeDisposable _disposer = new();

		public override void InstallBindings()
		{
			Container.BindInstance(_disposer);
			Container.InstallPlayer();
			Container.InstallWeapons();
			Container.InstallEnemy();
			Container.InstallRestart();
		}

		private void OnDestroy()
		{
			_disposer.Dispose();
		}
	}
}

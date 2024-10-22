
namespace Survivors.Enemy
{
	public interface IEnemy
	{
		public void OnTick();
		public bool IsDead();
		public void Destroy();
	}
}

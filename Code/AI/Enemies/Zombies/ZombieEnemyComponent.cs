using Sandbox;

public sealed class ZombieEnemyComponent : EnemyBaseComponent
{
	protected override void MoveTowardsPlayer()
	{
		// Example: Zombies move slower but groan!
		Log.Info( "Zombie is moving!" );
		base.MoveTowardsPlayer();
	}

	protected override void Die()
	{
		// Play zombie death animation!
		Log.Info( "Zombie has died!" );
		base.Die();
	}
}

using Sandbox;
using System;

[Title( "Zombie Component" )]
[Category( "Enemies" )]
[Icon( "mood_bad" )]
public sealed class ZombieBaseComponent : Component
{
	[Property] public float MoveSpeed { get; set; } = 80f;
	[Property] public float Health { get; set; } = 100f;
	[Property] public float AttackDamage { get; set; } = 10f;
	[Property] public float AttackRate { get; set; } = 1.5f;
	[Property] public float AttackRange { get; set; } = 60f;

	private TimeUntil _nextAttackTime;
	private GameObject target;

	protected override void OnUpdate()
	{
		FindTarget();

		if ( target != null )
		{
			MoveToTarget();
			TryAttack();
		}
	}

	private void FindTarget()
	{
		target = PlayerManagerComponent.Instance?.GetNearestPlayer( WorldPosition );
	}

	private void MoveToTarget()
	{
		if ( target == null ) return;

		var direction = (target.WorldPosition - WorldPosition).Normal;
		WorldPosition += direction * MoveSpeed * Time.Delta;
		WorldRotation = Rotation.LookAt( direction, Vector3.Up );
	}

	private void TryAttack()
	{
		if ( target == null ) return;
		if ( _nextAttackTime > 0 ) return;

		float distance = WorldPosition.Distance( target.WorldPosition );
		if ( distance < AttackRange )
		{
			Log.Info( "Zombie attacks!" );
			_nextAttackTime = AttackRate;
		}
	}


	public void TakeDamage( float amount )
	{
		Health -= amount;
		if ( Health <= 0 )
		{
			Die();
		}
	}

	private void Die()
	{
		GameObject.Destroy();
	}
}


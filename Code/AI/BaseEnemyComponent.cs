using Sandbox;

[Title( "Enemy Base" )]
[Category( "AI" )]
public class EnemyBaseComponent : Component
{
	[Property] public float MaxHealth { get; set; } = 100f;
	[Property] public float MoveSpeed { get; set; } = 150f;

	[Property] public float AttackForce { get; set; } = 50f;

	protected float CurrentHealth;
	protected GameObject Player;

	protected override void OnStart()
	{
		CurrentHealth = MaxHealth;
		Player = Scene.GetAllComponents<PlayerController>().FirstOrDefault()?.GameObject;
	}

	protected override void OnUpdate()
	{
		if ( Player == null ) return;
		if ( CurrentHealth <= 0f ) return;

		MoveTowardsPlayer();
	}

	protected virtual void MoveTowardsPlayer()
	{
		var direction = (Player.WorldPosition - WorldPosition).Normal;
		WorldPosition += direction * MoveSpeed * Time.Delta;
	}

	public virtual void TakeDamage( float amount )
	{
		CurrentHealth -= amount;

		if ( CurrentHealth <= 0f )
		{
			Die();
		}
	}

	protected virtual void Die()
	{
		GameObject.Destroy();
	}
}

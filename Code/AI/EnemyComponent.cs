using Sandbox;

public sealed class EnemyComponent : Component
{
	[Property] public float defaultSpeed { get; set; } = 150f;
	[Property] public float runMultiplificator { get; set; } = 1.5f;
	[Property] public float MaxHealth { get; set; } = 100f;


	private float currentHealth;
	private GameObject player;

	protected override void OnStart()
	{
		currentHealth = MaxHealth;
		player = Scene.GetAllComponents<PlayerController>().FirstOrDefault()?.GameObject;
	}

	protected override void OnUpdate()
	{
		if ( player == null ) return;

		var direction = (player.Transform.Position - Transform.Position).Normal;
		Transform.Position += direction * defaultSpeed * Time.Delta;
	}

	public void TakeDamage( float amount )
	{
		currentHealth -= amount;

		if ( currentHealth <= 0f )
		{
			GameObject.Destroy();
		}
	}
}

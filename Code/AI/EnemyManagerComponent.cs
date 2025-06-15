using Sandbox;

public sealed class EnemyManagerComponent : Component
{
	[Property] public SpawnerComponent Spawner { get; set; }

	[Property] public int EnemiesPerFloor { get; set; } = 5;
	[Property] public int EnemyIncrementPerFloor { get; set; } = 2;

	[Property] public float EnemySpeedIncrement { get; set; } = 20f;

	public int currentFloor { get; private set; } = 1; 

	protected override void OnStart()
	{
		SpawnFloor();
	}

	protected override void OnUpdate()
	{
		var enemies = Scene.GetAllComponents<EnemyComponent>().ToList();

		if ( enemies.Count == 0 )
		{
			Log.Info( $"Floor {currentFloor} cleared!" );
			currentFloor++;
			SpawnFloor();
		}
	}

	private void SpawnFloor()
	{
		int totalEnemies = EnemiesPerFloor + (EnemyIncrementPerFloor * (currentFloor - 1));

		for ( int i = 0; i < totalEnemies; i++ )
		{
			var offset = Vector3.Random * 10f;
			offset.z = 0;

			/*var enemy = Spawner.EnemyPrefab.Clone( Spawner.Transform.Position + offset );
			var enemyAI = enemy.Components.Get<EnemyComponent>();
			enemyAI.MoveSpeed = enemySpeed;*/
		}

		Log.Info( $"Spawning Floor {currentFloor}: {totalEnemies}" );
	}
}

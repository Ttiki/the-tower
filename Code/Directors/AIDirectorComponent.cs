using Sandbox;

public sealed class AIDirector : Component
{
	[Property]
	public List<GameObject> EnemyPrefabs { get; set; } = new();

	[Property]
	public List<GameObject> SpawnPoints { get; set; } = new();

	public List<GameObject> SpawnedEnemies { get; private set; } = new();
	public Queue<string> KillOrder { get; private set; } = new();

	public void SpawnEnemy( int prefabIndex, Vector3 position )
	{
		if ( prefabIndex < 0 || prefabIndex >= EnemyPrefabs.Count )
		{
			Log.Warning( "Invalid prefab index." );
			return;
		}

		var prefab = EnemyPrefabs[prefabIndex];

		// Clone at desired position
		GameObject enemy = prefab.Clone( position, Rotation.Identity );
		SpawnedEnemies.Add( enemy );
	}

	public void RegisterEnemyDeath( GameObject enemy )
	{
		if ( SpawnedEnemies.Remove( enemy ) )
		{
			KillOrder.Enqueue( enemy.Name );
		}
	}

	public void KillAllEnemies()
	{
		foreach ( var enemy in SpawnedEnemies )
		{
			enemy.Destroy();
		}
		SpawnedEnemies.Clear();
	}
}

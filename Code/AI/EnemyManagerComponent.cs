using Sandbox;
using System.Collections.Generic;
using System.Linq;

[Title( "Enemy Manager" )]
[Category( "Managers" )]
public sealed class EnemyManagerComponent : Component
{
	// Zombie prefabs and spawn points
	[Property] public List<GameObject> SpawnPrefabs { get; set; } = new();
	[Property] public List<GameObject> SpawnPoints { get; set; } = new();

	[Property] public int MaxEnemies = 10;
	[Property] public float SpawnInterval = 5f;

	private TimeUntil _nextSpawnTime;
	private List<GameObject> ActiveEnemies = new();

	protected override void OnStart()
	{
		_nextSpawnTime = 0f;
	}

	protected override void OnUpdate()
	{
		CleanDeadEnemies();

		if ( ActiveEnemies.Count >= MaxEnemies )
			return;

		if ( _nextSpawnTime > 0 )
			return;

		SpawnEnemy();
		_nextSpawnTime = SpawnInterval;
	}

	private void SpawnEnemy()
	{
		if ( SpawnPrefabs.Count == 0 || SpawnPoints.Count == 0 )
			return;

		// Pick random prefab & spawn point
		var prefab = Game.Random.FromList( SpawnPrefabs );
		var spawnPoint = Game.Random.FromList( SpawnPoints );

		var enemy = prefab.Clone( spawnPoint.WorldPosition, spawnPoint.WorldRotation );
		ActiveEnemies.Add( enemy );
	}

	private void CleanDeadEnemies()
	{
		ActiveEnemies.RemoveAll( e => !e.IsValid() );
	}
}

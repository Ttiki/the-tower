using Sandbox;
using Sandbox.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Directors
{
	public class EnemyDirector
	{
		public List<GameObject> AliveEnemies { get; private set; } = new();

		public List<GameObject> SpawnPoints { get; set; } = new();

		public class EnemySpawnProfile
		{
			[Property] public string EnemyType { get; set; }
			[Property] public List<GameObject> Prefabs { get; set; } = new();
			[Property] public int MinWave { get; set; } = 1;
			[Property] public float BaseWeight { get; set; } = 1.0f;
			[Property] public float GrowthPerWave { get; set; } = 0.0f;
		}

		public List<EnemySpawnProfile> EnemyProfiles { get; set; } = new();

		public int EnemiesToSpawn { get; private set; } = 0;
		public int EnemiesKilled { get; private set; } = 0;

		private float _spawnTimer = 0f;
		private const float SpawnInterval = 1.0f; // seconds between spawns

		public void Update()
		{
			// Cleanup dead enemies
			AliveEnemies.RemoveAll( e => e.IsValid == false || e.IsDestroyed );

			// Spawning enemies
			if ( EnemiesToSpawn > 0 )
			{
				_spawnTimer -= Time.Delta;

				if ( _spawnTimer <= 0f )
				{
					SpawnEnemy();
					EnemiesToSpawn--;
					_spawnTimer = SpawnInterval;
				}
			}
		}

		public void StartWave()
		{
			int waveNumber = GameDirector.Instance.CurrentWave;

			EnemiesToSpawn = ComputeEnemyCountForWave( waveNumber );
			EnemiesKilled = 0;
			_spawnTimer = 0f;

			Log.Info( $"[EnemyDirector] Wave {waveNumber}: preparing to spawn {EnemiesToSpawn} enemies" );
		}

		public bool IsWaveFinished()
		{
			return (EnemiesToSpawn == 0 && AliveEnemies.Count == 0);
		}

		private int ComputeEnemyCountForWave( int wave )
		{
			return 5 + (wave - 1) * 2;
		}

		public void SpawnEnemy()
		{
			int waveNumber = GameDirector.Instance.CurrentWave;

			var profile = PickEnemyProfileForWave( waveNumber );

			if ( profile == null || profile.Prefabs.Count == 0 )
			{
				Log.Warning( "[EnemyDirector] No valid enemy profile!" );
				return;
			}

			var rnd = new System.Random();

			var prefab = profile.Prefabs[rnd.Next( profile.Prefabs.Count )];

			var spawnPoint = SpawnPoints[rnd.Next( SpawnPoints.Count )];
			var enemy = prefab.Clone( spawnPoint.Transform.World );
			AliveEnemies.Add( enemy );

			Log.Info( $"[EnemyDirector] Spawned enemy '{enemy.Name}' at {spawnPoint.WorldPosition}" );
		}

		public void OnEnemyKilled( GameObject enemy )
		{
			AliveEnemies.Remove( enemy );
			EnemiesKilled++;

			Log.Info( $"[EnemyDirector] Enemy killed. Alive: {AliveEnemies.Count}, Killed: {EnemiesKilled}" );
		}

		private EnemySpawnProfile PickEnemyProfileForWave( int wave )
		{
			var availableProfiles = EnemyProfiles.Where( p => wave >= p.MinWave ).ToList();

			if ( availableProfiles.Count == 0 )
				return null;

			float totalWeight = 0f;
			foreach ( var p in availableProfiles )
			{
				totalWeight += p.BaseWeight + p.GrowthPerWave * (wave - p.MinWave);
			}

			float choice = Random.Shared.Float( 0, totalWeight );
			float cumulative = 0f;

			foreach ( var p in availableProfiles )
			{
				float weight = p.BaseWeight + p.GrowthPerWave * (wave - p.MinWave);
				cumulative += weight;

				if ( choice < cumulative )
					return p;
			}

			return availableProfiles.First();
		}
	}
}

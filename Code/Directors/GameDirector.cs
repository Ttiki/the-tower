using Game.Directors;
using Sandbox;
using System.Collections.Generic;
using System.Linq;
using static Game.Directors.EnemyDirector;

[Title( "Game Director" )]
[Category( "Directors" )]
public sealed class GameDirector : Component
{
	public static GameDirector Instance { get; private set; }

	private AIDirector _aiDirector;
	private EnemyDirector _enemyDirector;
	private MusicDirector _musicDirector;

	public bool IsWaveRunning { get; private set; } = false;
	public bool IsGameOver { get; private set; } = false;

	[Property] public List<EnemySpawnProfile> EnemyProfiles { get; set; } = new();
	[Property] public List<GameObject> SpawnPoints { get; set; } = new();

	public int CurrentWave { get; private set; } = 0;

	protected override void OnAwake()
	{
		Instance = this;

		_aiDirector = new AIDirector();
		_enemyDirector = new EnemyDirector();
		_musicDirector = new MusicDirector();

		_enemyDirector.EnemyProfiles = EnemyProfiles;
		_enemyDirector.SpawnPoints = SpawnPoints;

		Log.Info( "[GameDirector] Ready" );
	}

	protected override void OnUpdate()
	{
		_aiDirector.Update();
		_enemyDirector.Update();
		_musicDirector.Update();
	}

	public void StartNextWave()
	{
		CurrentWave++;

		Log.Info( $"[GameDirector] Starting wave {CurrentWave}" );

		_enemyDirector.StartWave();
	}

	public void SetWaveRunning( bool running )
	{
		IsWaveRunning = running;
	}

	[ConCmd( "start_wave" )]
	public static void CmdStartWave()
	{
		Instance?.StartNextWave();
	}

	[ConCmd( "spawn_enemy" )]
	public static void CmdSpawnEnemy()
	{
		Instance?._enemyDirector.SpawnEnemy();
	}

	public EnemyDirector EnemyDirector => _enemyDirector;

	public void GameLose()
	{
		if ( IsGameOver )
			return;

		IsGameOver = true;
		Log.Info( "[GameDirector] YOU LOSE!" );

		// TODO: show UI, stop game, music...
	}
}

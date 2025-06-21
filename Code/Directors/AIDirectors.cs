using Sandbox;

namespace Game.Directors
{
	public class AIDirector
	{
		private float _waveDelayTimer = 0f;
		private const float WaveDelay = 5.0f; // seconds between waves
		private bool _waitingForNextWave = false;

		public void Update()
		{
			// Check if wave is finished (if no enemies alive and nothing left to spawn)
			if ( GameDirector.Instance.IsWaveRunning &&
			     GameDirector.Instance.EnemyDirector.IsWaveFinished() &&
			     !_waitingForNextWave )
			{
				// End current wave
				GameDirector.Instance.SetWaveRunning( false );

				// Start wave delay
				_waitingForNextWave = true;
				_waveDelayTimer = WaveDelay;

				Log.Info( "[AIDirector] Wave cleared! Starting next wave in 5 seconds..." );
			}

			// Countdown to next wave
			if ( _waitingForNextWave )
			{
				_waveDelayTimer -= Time.Delta;

				if ( _waveDelayTimer <= 0f )
				{
					StartNextWave();
					_waitingForNextWave = false;
				}
			}
		}

		private void StartNextWave()
		{
			GameDirector.Instance.StartNextWave();

			Log.Info( $"[AIDirector] Starting wave {GameDirector.Instance.CurrentWave}" );
		}
	}
}

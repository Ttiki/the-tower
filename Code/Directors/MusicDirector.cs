using Sandbox;

namespace Game.Directors
{
	public class MusicDirector
	{
		private enum MusicState
		{
			Calm,
			Action,
			Intense
		}

		private MusicState _currentState = MusicState.Calm;

		public void Update()
		{
			var enemyDirector = GameDirector.Instance.EnemyDirector;

			// Decide state
			if ( enemyDirector.AliveEnemies.Any( e => e.Name.Contains( "Boss", System.StringComparison.OrdinalIgnoreCase ) ) )
			{
				SetMusicState( MusicState.Intense );
			}
			else if ( enemyDirector.IsWaveFinished() )
			{
				SetMusicState( MusicState.Action );
			}
			else
			{
				SetMusicState( MusicState.Calm );
			}
		}

		private void SetMusicState( MusicState state )
		{
			if ( state == _currentState )
				return;

			_currentState = state;

			// TODO: play actual music track here!
			Log.Info( $"[MusicDirector] Changed to: {_currentState}" );
		}
	}
}

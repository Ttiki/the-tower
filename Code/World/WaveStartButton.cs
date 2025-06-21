using Sandbox;

[Title( "Start Wave Button" )]
public sealed class StartWaveButton : Component
{
	[Property] public float MaxUseDistance { get; set; } = 200.0f;

	protected override void OnUpdate()
	{
		if ( !Input.Pressed( "use" ) )
			return;

		var tr = Scene.Trace
			.Ray( Camera.Main.Position, Camera.Main.Position + Camera.Main.Rotation.Forward * MaxUseDistance )
			.WithoutTags( "player" )
			.Run();

		if ( tr.Hit && tr.GameObject == GameObject )
		{
			Log.Info( "[StartWaveButton] Pressed! Starting wave." );

			GameDirector.Instance?.StartNextWave();
		}
	}
}

using Sandbox;

[Title( "Player Component" )]
[Category( "Player" )]
public sealed class TheTowerPlayerComponent : Component
{
	protected override void OnAwake()
	{
		PlayerManagerComponent.Instance?.RegisterPlayer( GameObject );
	}

	protected override void OnDestroy()
	{
		PlayerManagerComponent.Instance?.UnregisterPlayer( GameObject );
	}
}

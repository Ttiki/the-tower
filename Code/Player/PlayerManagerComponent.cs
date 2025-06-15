using Sandbox;
using System.Collections.Generic;
using System.Linq;

[Title( "Player Manager" )]
[Category( "Managers" )]
public sealed class PlayerManagerComponent : Component
{
	public static PlayerManagerComponent Instance { get; private set; }

	public List<GameObject> Players { get; private set; } = new();

	protected override void OnAwake()
	{
		Instance = this;
	}

	public void RegisterPlayer( GameObject player )
	{
		if ( !Players.Contains( player ) )
			Players.Add( player );
	}

	public void UnregisterPlayer( GameObject player )
	{
		Players.Remove( player );
	}

	public GameObject GetNearestPlayer( Vector3 position, float maxDistance = 10000f )
	{
		return Players
			.OrderBy( p => (p.Transform.World.Position - position).Length )
			.FirstOrDefault( p => (p.Transform.World.Position - position).Length < maxDistance );
	}
}

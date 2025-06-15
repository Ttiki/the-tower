using Sandbox;
using System.Collections.Generic;

[Title( "Spawn Profile Component" )]
[Category( "Spawning" )]
public sealed class SpawnProfileComponent : Component
{
	[Property] public int Floor { get; set; }
	[Property] public int MaxEnemies { get; set; }
	[Property] public float SpawnInterval { get; set; } = 5f;

	[Property] public List<SpawnEntry> SpawnEntries { get; set; } = new();
}

[System.Serializable]
public class SpawnEntry
{
	[Property] public GameObject Prefab { get; set; }
	[Property] public int Weight { get; set; } = 1;
}

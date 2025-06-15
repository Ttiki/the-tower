using Sandbox;
using Sandbox.UI;
using static System.Net.Mime.MediaTypeNames;

public sealed class FloorUI : Panel
{
	public int CurrentFloor { get; set; } = 1;

	public override void Tick()
	{
		/*Text = $"Floor: {CurrentFloor}";*/
	}
}

using Sandbox;

public sealed class GunComponent : Component
{
	[Property] public float FireRate { get; set; } = 0.2f;
	[Property] public float Damage { get; set; } = 50f;
	[Property] public float Range { get; set; } = 1000f;

	private float nextFireTime;

	protected override void OnUpdate()
	{
		if (Input.Pressed("Attack1") && Time.Now >= nextFireTime)
		{
			nextFireTime = Time.Now + FireRate;

			var camPos = Scene.Camera.Transform.Position;
			var camRot = Scene.Camera.Transform.Rotation;

			var tr = Scene.Trace.Ray(
				camPos,
				camPos + camRot.Forward * Range
			).UseHitboxes().Run();

			if ( tr.Hit)
			{
				Log.Info($"Hit {tr.GameObject}");

				var enemy = tr.GameObject.Components.Get<EnemyComponent>(FindMode.EverythingInSelfAndDescendants);
				if (enemy != null)
				{
					enemy.TakeDamage(Damage);
				}
			}
		}
	}
}

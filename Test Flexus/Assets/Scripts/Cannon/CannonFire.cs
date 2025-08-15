using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private CannonControlsSO cannonControlsSO;
    [SerializeField] private Transform StartPosition;
	[SerializeField] private CameraEffectsInvoker cameraEffectsInvoker;
	[SerializeField, Range(0.0f, 100.0f)] public float force;
	[SerializeField] private float forceMultiplier = 1;
	[SerializeField] private Projectile projectile;
	private TrajectoryPredictor trajectoryPredictor;

	private void OnEnable()
	{
		trajectoryPredictor = GetComponent<TrajectoryPredictor>();
        if (StartPosition == null)
            StartPosition = transform;
		cannonControlsSO.FireEvent += Fire;
	}

	private void OnDisable()
	{
		cannonControlsSO.FireEvent -= Fire;
	}

	void LateUpdate()
	{
		trajectoryPredictor.PredictTrajectory(ProjectileData());
	}

    ProjectileProperties ProjectileData()
    {
        ProjectileProperties properties = new ProjectileProperties();

        properties.direction = StartPosition.forward;
        properties.initialPosition = StartPosition.position;
        properties.initialSpeed = force * forceMultiplier;
        properties.mass = projectile.properties.mass;
        properties.drag = projectile.properties.drag;

        return properties;
    }

	private void Fire()
	{
		animator.SetTrigger("Fire");
		cameraEffectsInvoker.Shake();
		var shotProjectile = Instantiate(projectile, StartPosition.position, Quaternion.identity);
		shotProjectile.properties = ProjectileData();
	}
}

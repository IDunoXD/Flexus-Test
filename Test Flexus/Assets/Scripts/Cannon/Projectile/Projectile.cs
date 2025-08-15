using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public ProjectileProperties properties;
	[SerializeField] private float mass;
	[SerializeField] private float drag;
	[SerializeField] private float bounce = 1;
	[SerializeField] private float numberOfBounces = 1;
	[SerializeField] private GameObject hitMarker;
	[SerializeField] private GameObject explosionVFX;
	[SerializeField] private float hitMarkeOffset = 0.01f;
	[SerializeField] private float explosionVFXOffset = 0.01f;
	private float timeFlying = 0;
	private float timeFalling = 0;
	private Vector3 velocity;
	private Vector3 nextPosition;

	private void OnValidate()
	{
		properties.mass = mass;
		properties.drag = drag;
	}

	private void Start()
	{
		velocity = properties.direction * properties.initialSpeed;
		transform.position = properties.initialPosition;
	}

	private void FixedUpdate()
	{

		velocity = (velocity * Mathf.Clamp01(1f - properties.drag * timeFlying)) + (Physics.gravity * mass * timeFalling);
		nextPosition = transform.position + velocity;

		if (Physics.Raycast(transform.position, velocity.normalized, out RaycastHit hit, Vector3.Distance(transform.position, nextPosition)))
		{
			Instantiate(hitMarker, hit.point + hit.normal * hitMarkeOffset, Quaternion.LookRotation(hit.normal, Vector3.up));
			velocity += hit.normal * velocity.magnitude * (Vector3.Dot(hit.normal, -velocity.normalized) + bounce);
			transform.position = hit.point;
			numberOfBounces--;
			timeFalling = 0;
			if (numberOfBounces == -1)
			{
				var vfx = Instantiate(explosionVFX, hit.point + hit.normal * explosionVFXOffset, Quaternion.LookRotation(hit.normal, Vector3.up));
				Destroy(vfx, 2);
				Destroy(gameObject);
			}
		}
		else
		{
			transform.position = nextPosition;
		}

		timeFlying += Time.fixedDeltaTime;
		timeFalling += Time.fixedDeltaTime;
	}
}

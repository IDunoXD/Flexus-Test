using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{
    private LineRenderer trajectoryLine;
    [SerializeField, Tooltip("The marker will show where the projectile will hit")] private Transform hitMarker;
    [SerializeField, Range(10, 100), Tooltip("The maximum number of points the LineRenderer can have")] private int maxPoints = 50;
	[SerializeField] private float hitMarkerOffset = 0.025f;
	private float time;

    private void Start()
    {
        if (trajectoryLine == null)
            trajectoryLine = GetComponent<LineRenderer>();

        SetTrajectoryVisible(true);
    }

    public void PredictTrajectory(ProjectileProperties projectile)
    {
		time = 0; 
        Vector3 velocity = projectile.direction * projectile.initialSpeed;
        Vector3 position = projectile.initialPosition;
        Vector3 nextPosition;

        UpdateLineRender(maxPoints, (0, position));

        for (int i = 1; i < maxPoints; i++)
        {
            velocity = (velocity * Mathf.Clamp01(1f - projectile.drag * time)) + (Physics.gravity * projectile.mass * time);
            nextPosition = position + velocity;
			
            if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, Vector3.Distance(position, nextPosition)))
			{
				UpdateLineRender(i, (i - 1, hit.point));
				MoveHitMarker(hit);
				break;
			}

            hitMarker.gameObject.SetActive(false);
            position = nextPosition;
			time += Time.fixedDeltaTime;
            UpdateLineRender(maxPoints, (i, position));
        }
    }
	
    private void UpdateLineRender(int count, (int point, Vector3 pos) pointPos)
	{
		trajectoryLine.positionCount = count;
		trajectoryLine.SetPosition(pointPos.point, pointPos.pos);
	}

    private void MoveHitMarker(RaycastHit hit)
    {
        hitMarker.gameObject.SetActive(true);
        hitMarker.position = hit.point + hit.normal * hitMarkerOffset;
        hitMarker.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
    }

    public void SetTrajectoryVisible(bool visible)
    {
        trajectoryLine.enabled = visible;
        hitMarker.gameObject.SetActive(visible);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
	[SerializeField] private CannonControlsSO cannonControlsSO;
	[SerializeField] private GameObject cannonBase;
	[SerializeField] private GameObject cannonBarrel;
	[SerializeField] private float baseRotationSpeed = 1;
	[SerializeField] private float barrelElevationSpeed = 1;
	[SerializeField] private float barrelElevationUpLimit = 30;
	[SerializeField] private float barrelElevationDownLimit = 10;
	[SerializeField] private float barrelAngleX = 0;
	private Vector3 baseVector;
	private Vector3 barrelVector;

	private void OnEnable()
	{
		cannonControlsSO.MoveEvent += CannonMoveDirection;
		cannonControlsSO.SetCannonControls(true);
	}

	private void OnDisable()
	{
		cannonControlsSO.MoveEvent -= CannonMoveDirection;
	}

	private void Update()
	{
		if (baseVector != Vector3.zero)
		{
			cannonBase.transform.Rotate(baseVector, Space.Self);
		}
		if (barrelVector != Vector3.zero)
		{
			if (-barrelElevationDownLimit <= barrelAngleX && barrelAngleX <= barrelElevationUpLimit)
			{
				cannonBarrel.transform.Rotate(barrelVector, Space.Self);
				barrelAngleX += barrelVector.x;
			}

			if (-barrelElevationDownLimit > barrelAngleX)
				barrelAngleX = -barrelElevationDownLimit;

			if (barrelAngleX > barrelElevationUpLimit)
				barrelAngleX = barrelElevationUpLimit;

			cannonBarrel.transform.localRotation = Quaternion.Euler(barrelAngleX, 0, 0);
		}
	}

	private void CannonMoveDirection(Vector2 vector)
	{
		baseVector = new Vector3(0, vector.x * baseRotationSpeed * Time.deltaTime, 0);
		barrelVector = new Vector3(vector.y * barrelElevationSpeed * Time.deltaTime, 0, 0);
	}
}

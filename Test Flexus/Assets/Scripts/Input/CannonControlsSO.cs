using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "CannonControls")]
public class CannonControlsSO : ScriptableObject, CannonControls.ICannonActions
{
	private CannonControls inputActions;

	public event Action<Vector2> MoveEvent;
	public event Action FireEvent;

	private void OnEnable()
	{
		if (inputActions == null)
		{
			inputActions = new CannonControls();
			inputActions.Cannon.SetCallbacks(this);
		}
	}

	public void SetCannonControls(bool value)
	{
		if (value)
		{
			inputActions.Cannon.Enable();
		}
		else
		{
			inputActions.Cannon.Disable();
		}
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		if(context.ReadValue<Vector2>().x != 0 || context.ReadValue<Vector2>().y != 0)
		{
			MoveEvent?.Invoke(context.ReadValue<Vector2>());
		}
		if(context.canceled)
		{
			MoveEvent?.Invoke(Vector2.zero);
		}
	}

	public void OnFire(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			FireEvent?.Invoke();
		}
	}
}
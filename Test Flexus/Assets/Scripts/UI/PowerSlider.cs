using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSlider : MonoBehaviour
{
	[SerializeField] private CannonFire cannonFire;
	public void UpdateValue(float value)
	{
		cannonFire.force = value;
	}
}

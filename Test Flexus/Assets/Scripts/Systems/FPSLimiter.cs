using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class FPSLimiter : MonoBehaviour
{
	[SerializeField] private int FPS = 30;
	public int fps
	{
		get
		{
			return FPS;
		}
		set
		{
			FPS = value;
			Application.targetFrameRate = value;
		}
	}

	void OnValidate()
	{
		fps = FPS;
	}
}

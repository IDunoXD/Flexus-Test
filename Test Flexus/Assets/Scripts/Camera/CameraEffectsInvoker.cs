using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraEffectsInvoker : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float duration;
	[SerializeField] private float pstrength;
	[SerializeField] private float rstrength;
	[SerializeField] private int vibratio;
	[SerializeField] private float randomness;
	private Vector3 TargetStartPositon;
	private Quaternion TargetStartRotation;

	private void Awake()
	{
		TargetStartPositon = target.localPosition;
		TargetStartRotation = target.localRotation;
	}

	// private void Start()
	// {
	// 	Debug.Log(TargetStartPositon);
	// 	Debug.Log(TargetStartRotation);		
	// }

	public void Shake()
	{
		DOTween.Kill(target.gameObject);
		target.localPosition = TargetStartPositon;
		target.localRotation = TargetStartRotation;
		target.DOShakePosition(duration, pstrength, vibratio, randomness, false).SetId(target.gameObject);
		target.DOShakeRotation(duration, rstrength, vibratio, randomness, false).SetId(target.gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRandomizer : MonoBehaviour
{
	[SerializeField] private float randomnesMagnitude = 1;
    private Mesh mesh;
	Vector3[] vertecies;

	private void Start()
	{
		mesh = GetComponent<MeshFilter>().mesh;
		vertecies = mesh.vertices;
		for (int i = 0; i < vertecies.Length; i++)
			vertecies[i] += Random.onUnitSphere * randomnesMagnitude;
		mesh.vertices = vertecies;
	}
}

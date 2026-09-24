using UnityEngine;
using static VectorExtensions;
using System.Collections.Generic;

public class CubesManager : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private GameObject _premiumCube;
    private List<GameObject> _cubes = new List<GameObject>();

    [SerializeField] private int _cubesCount;
    [Range(0f, 200f), 
	SerializeField] private float _cubesSpeed;

	[SerializeField] private RotationDirection _rotationDirection;

	private void Awake()
	{
		for (var i = 0; i < _cubesCount; i++)
        {
            var currCube = Instantiate(_cube, transform.position, Quaternion.identity);
			var currCubePos = GenerateVectorWithRandomMagnitude(5, 20);
			currCube.transform.position = currCubePos;
			currCube.transform.LookAt(transform.position);
			currCube.transform.Rotate(new Vector3(0f, 0f, Random.Range(0, 360f)));

			currCube.GetComponent<CubeMover>().InitializeRotationSettings(
				transform.position, currCube.transform.right, Random.Range(_cubesSpeed * 0.7f, _cubesSpeed * 1.4f), 5);

			_cubes.Add(currCube);
        }
	}

	private void OnValidate()
	{
		
	}

	private enum RotationDirection
	{
		Left,
		Right
	}
}

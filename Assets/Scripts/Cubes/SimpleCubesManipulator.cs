using System.Collections.Generic;
using UnityEngine;

public class SimpleCubesManipulator : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
	[SerializeField] private int _cubesCount;
	private List<GameObject> _cubes = new List<GameObject>();

    [Range(0f, 100f), Tooltip("Spins count in 10 secs"),
	SerializeField] private float _speed = 20f;
    [Range(0f, 60f),
	SerializeField] private float _radius = 10f;
    [Range(0f, 360f),
	SerializeField] private float _circleSectorAngle = 360f;
	[SerializeField] private bool _rotateClockwise = true;

	private float _timePassed;
	private const float TimeOfBasicSpin = 10f; //in seconds

	private void Awake()
	{
		for (var i = 0; i < _cubesCount; i++)
			_cubes.Add(Instantiate(_cube, transform));
		SetCubesPosition();
	}

	private void FixedUpdate()
	{
		if (_timePassed >= TimeOfBasicSpin)
			_timePassed = 0;

		SetCubesPosition();

		if (_rotateClockwise)
			_timePassed += Time.fixedDeltaTime * _speed;
		else
			_timePassed -= Time.fixedDeltaTime * _speed;
	}

	private void SetCubesPosition()
	{
		for (var i = 0; i < _cubesCount; i++)
		{
			var cubeAngle = Mathf.Deg2Rad * (
				360f * _timePassed / TimeOfBasicSpin + _circleSectorAngle * ((float)(i + 1) / (float)_cubesCount));
			_cubes[i].transform.position =
				new Vector3(Mathf.Sin(cubeAngle), 0f, Mathf.Cos(cubeAngle)) * _radius;
			_cubes[i].transform.LookAt(transform.position);
		}
	}
}

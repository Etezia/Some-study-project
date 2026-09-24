using UnityEngine;
using static VectorExtensions;
using System.Collections.Generic;

//да простит меня Бог за это... если его нет, то прощён явно не буду
public class CubesManager : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private GameObject _premiumCube;
    private List<CubeMover> _cubeMovers = new List<CubeMover>();

    [SerializeField] private int _cubesCount;
    [Range(0f, 50f), 
	SerializeField] private float _speedModifier = 1f;
	[SerializeField] private float _minCubesSpeed = 30f;
	[SerializeField] private float _maxCubesSpeed = 50f;
	
	[Range(0.001f, 20f),
	SerializeField] private float _radiusModifier = 1f;
	[SerializeField] private float _minRadius = 5f;
	[SerializeField] private float _maxRadius = 15f;

	[SerializeField] private bool _isRotationInversive = false;

	private const float PremiumCubeChance = 0.00777f;

	private void Awake()
	{
		for (var i = 0; i < _cubesCount; i++)
			SpawnAndAdjustCube();
	}

	private void OnValidate()
	{
		var currCubesSpeed = _speedModifier;

		if (_isRotationInversive)
			currCubesSpeed = -_speedModifier;

		foreach (var cubeMover in _cubeMovers)
			cubeMover.MultiplyValues(currCubesSpeed, _radiusModifier);
	}

	private void SpawnAndAdjustCube()
	{
		var currCube = Instantiate(
				Random.Range(0f, 1f) <= PremiumCubeChance ? _premiumCube : _cube, transform.position, Quaternion.identity);
		var currCubePos = GenerateVectorWithRandomMagnitude(_minRadius, _maxRadius);
		currCube.transform.position = currCubePos;
		currCube.transform.LookAt(transform.position);
		currCube.transform.Rotate(new Vector3(0f, 0f, Random.Range(0, 360f)));

		var currCubeMover = currCube.GetComponent<CubeMover>();
		currCubeMover.InitializeRotationSettings(
			transform.position, currCube.transform.right, Random.Range(_minCubesSpeed, _maxCubesSpeed), currCubePos.magnitude);
		currCubeMover.MultiplyValues(_speedModifier, _radiusModifier);

		_cubeMovers.Add(currCubeMover);
	}
}
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    private float _speed;
	private float _initialRadius;
    private float _radius;

    private Vector3 _rotationCenter;
    private Vector3 _rotationAxis;

	private void FixedUpdate()
	{
		transform.RotateAround(_rotationCenter, _rotationAxis, _speed * Time.fixedDeltaTime);
	}

	public void InitializeRotationSettings(Vector3 rotationCenter, Vector3 rotationAxis, float speed, float radius)
	{
		_rotationAxis = rotationAxis;
		_rotationCenter = rotationCenter;
		_speed = speed;
		_initialRadius = radius;
	}

	public void SetValues(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
    }
}

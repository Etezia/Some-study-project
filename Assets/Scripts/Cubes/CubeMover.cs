using UnityEngine;

public class CubeMover : MonoBehaviour
{
    private float _initialSpeed;
	private float _initialRadius;
	private float _currSpeed;

    private Vector3 _rotationCenter;
    private Vector3 _rotationAxis;

	private void FixedUpdate()
	{
		transform.RotateAround(_rotationCenter, _rotationAxis, _currSpeed * Time.fixedDeltaTime);
	}

	public void InitializeRotationSettings(Vector3 rotationCenter, Vector3 rotationAxis, float speed, float radius)
	{
		_rotationAxis = rotationAxis;
		_rotationCenter = rotationCenter;
		_initialSpeed = speed;
		_currSpeed = speed;
		_initialRadius = radius;
	}

	public void SetValues(float speed, float radius)
    {
        _currSpeed = speed;
		transform.position = _rotationCenter + (transform.position - _rotationCenter).normalized * radius;
    }

	public void MultiplyValues(float speedMult, float raduisMult) =>
		SetValues(_initialSpeed * speedMult, _initialRadius * raduisMult);
}

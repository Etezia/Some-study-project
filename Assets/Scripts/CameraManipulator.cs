using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManipulator : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

	private const float MinCamDistance = 15f;
	private const float MaxCamDistance = 50f;

    private InputSystem_Actions _inputSystem;
	private const float MoveSpeed = 24f;
	private const float ZoomSpeed = 32f;

	private void Awake() => _inputSystem = new InputSystem_Actions();

	private void Update()
	{
		var moveInputScaled = 
			MoveSpeed * Time.deltaTime * _inputSystem.Camera.Move.ReadValue<Vector2>();
		var zoomInputScaled = 
			ZoomSpeed * Time.deltaTime * _inputSystem.Camera.Zoom.ReadValue<float>();

		transform.Rotate(transform.up, -moveInputScaled.x);
		_cameraTransform.RotateAround(transform.position, transform.right, moveInputScaled.y);

		// --- Old variant ---
		var distance = (_cameraTransform.position - transform.position).magnitude;
		if (zoomInputScaled > 0 && distance > MinCamDistance
			|| zoomInputScaled < 0 && distance < MaxCamDistance)
			_cameraTransform.Translate(new Vector3(0f, 0f, zoomInputScaled));

		//_cameraTransform.localPosition = new Vector3(_cameraTransform.localPosition.x, _cameraTransform.localPosition.y,
		//	Math.Clamp(_cameraTransform.localPosition.z + zoomInputScaled, -MaxCamDistance, -MinCamDistance));
	}

	private void OnDisable() => _inputSystem.Camera.Disable();

	private void OnEnable() => _inputSystem.Camera.Enable();
}
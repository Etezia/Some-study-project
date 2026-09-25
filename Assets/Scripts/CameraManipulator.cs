using UnityEngine;

public class CameraManipulator : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

	private const float MinCamDistance = 10f;
	private const float MaxCamDistance = 70f;

    private InputSystem_Actions _inputSystem;
	private const float MoveSpeed = 28f;
	private const float ZoomSpeed = 40f;

	private void Awake() => _inputSystem = new InputSystem_Actions();

	private void Update()
	{
		var moveInputScaled = 
			MoveSpeed * Time.deltaTime * _inputSystem.Camera.Move.ReadValue<Vector2>();
		var zoomInputScaled = 
			ZoomSpeed * Time.deltaTime * _inputSystem.Camera.Zoom.ReadValue<float>();

		transform.Rotate(transform.up, -moveInputScaled.x);
		_cameraTransform.RotateAround(transform.position, transform.right, moveInputScaled.y);

		var distance = (_cameraTransform.position - transform.position).magnitude;
		if (zoomInputScaled > 0 && distance > MinCamDistance
			|| zoomInputScaled < 0 && distance < MaxCamDistance)
			_cameraTransform.Translate(new Vector3(0f, 0f, zoomInputScaled));	//idk, maybe there's better way to clamp zoom
	}

	private void OnDisable() => _inputSystem.Camera.Disable();

	private void OnEnable() => _inputSystem.Camera.Enable();
}
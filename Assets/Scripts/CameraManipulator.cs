using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManipulator : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    private InputSystem_Actions _inputSystem;
	private const float MoveSpeed = 18f;
	private const float ZoomSpeed = 15f;


	private void Awake() => _inputSystem = new InputSystem_Actions();

	private void Update()
	{
		var moveInputScaled = 
			MoveSpeed * Time.deltaTime * _inputSystem.Camera.Move.ReadValue<Vector2>();
		var zoomInputScaled = 
			ZoomSpeed * Time.deltaTime * _inputSystem.Camera.Zoom.ReadValue<float>();

		transform.Rotate(new Vector3(moveInputScaled.y, -moveInputScaled.x, 0f));
		_cameraTransform.Translate(new Vector3(0, 0, zoomInputScaled), transform);
	}

	private void OnDisable() => _inputSystem.Camera.Disable();

	private void OnEnable() => _inputSystem.Camera.Enable();
}
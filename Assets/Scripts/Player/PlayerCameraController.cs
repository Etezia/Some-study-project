using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private GameObject playerBody;
    [SerializeField] private float cameraRotationSpeed;
    [SerializeField] private float playerRotationSpeed;

    private InputSystem_Actions inputSystem;

	private void Awake()
    {
        inputSystem = new InputSystem_Actions();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
    {
        var rotationVect = inputSystem.PlayerCamera.Rotate.ReadValue<Vector2>();

        transform.Rotate(transform.right, rotationVect.y * Time.deltaTime * cameraRotationSpeed);
        playerBody.transform.Rotate(
            playerBody.transform.up, rotationVect.x * Time.deltaTime * playerRotationSpeed);
    }

	private void OnEnable() => inputSystem.Enable();

	private void OnDisable() => inputSystem.Disable();

	private void Reset()
	{
		playerBody = transform.parent.gameObject;
	}
}

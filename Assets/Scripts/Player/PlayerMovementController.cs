using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private CharacterController charController;

    [SerializeField] private float straightSpeed;
    [SerializeField] private float sideSpeed;
    [SerializeField] private float runSpeedMult;
    [SerializeField] private float crouchingSpeedMult;
    private Vector2 moveCoeffVect;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityCoeff;

    [SerializeField] private bool holdToRun = false;
    [SerializeField] private bool holdToCrouch = false;

    public bool isRunning = false;   //public only for tests. change visibility
    public bool isCrouching = false; //public only for tests. change visibility

	private InputSystem_Actions inputSystem;

	private void Awake()
    {
		inputSystem = new InputSystem_Actions();
        moveCoeffVect = new Vector2(sideSpeed, straightSpeed);
	}

	private void Update()
    {
        CheckMoveApproach();

        var currPlayerVelZX = GetCurrPlayerVelocity();
        charController.Move(new Vector3(currPlayerVelZX.x, 0f, currPlayerVelZX.y));

	}

	private void Reset()
	{
        charController = GetComponent<CharacterController>();
	}

    private Vector2 GetCurrPlayerVelocity()
    {
        //if (!charController.isGrounded)
        //    return new Vector2();

        var currVelocity = Time.deltaTime * 
            inputSystem.PlayerMovement.Move.ReadValue<Vector2>() * moveCoeffVect;

		if (isRunning && currVelocity.y > 0)
            currVelocity.y *= runSpeedMult;

        return currVelocity;
    }

    private void CheckMoveApproach()
    {
		if (inputSystem.PlayerMovement.Run.WasPressedThisFrame())
		{
            if (holdToRun)
			{
                isRunning = true;
                isCrouching = false;
            }
            else
            {
				isRunning = !isRunning;
				isCrouching = isRunning == true ? false : true;
			}
		}
	}

	private void OnEnable() => inputSystem.Enable();

	private void OnDisable() => inputSystem.Disable();
}

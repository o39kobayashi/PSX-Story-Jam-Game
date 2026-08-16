using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private InputActionAsset inputActions;

    private InputAction playerMoveAction;
    private InputAction playerRunAction;

    private const string PLAYER_GROUND = "Player_Ground";
    private const string PLAYER_MOVEMENT = "Movement";
    private const string PLAYER_RUN = "Run";
    private const string PLAYER_FIRE = "Fire";

    private Vector2 playerMovementInput;
    private Vector3 playerMovement;
    private Vector3 runMovement;
    
    
    private bool isMovementPressed;
    private bool isRunning;

    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private float walkMultiplier;
    [SerializeField]
    private float runMultiplier;
    [SerializeField]
    private float rotationFactor;

    [SerializeField]
    private Camera camera;


    void Awake() {

        playerMoveAction = inputActions.FindAction(PLAYER_MOVEMENT);

        playerMoveAction.started += onMovementInput;
        playerMoveAction.performed += onMovementInput;
        playerMoveAction.canceled += onMovementInput;

        playerRunAction = inputActions.FindAction(PLAYER_RUN);

        playerRunAction.started += onRun;
        playerRunAction.canceled += onRun;

    }

    void Start()
    {
        


    }

    void Update()
    {

        handleRotation();
        handleGravity();

        if (isRunning) {

            characterController.Move(runMovement * Time.deltaTime);

        } else {

            characterController.Move(playerMovement * Time.deltaTime);

        }

    }

    void onMovementInput(InputAction.CallbackContext context) {

        playerMovementInput = context.ReadValue<Vector2>();

        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;

        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 normalizedDirection = (cameraForward * playerMovementInput.y) + (cameraRight * playerMovementInput.x);


        playerMovement.x = normalizedDirection.x * walkMultiplier;
        playerMovement.z = normalizedDirection.z * walkMultiplier;

        runMovement.x = normalizedDirection.x * runMultiplier;
        runMovement.z = normalizedDirection.z * runMultiplier;


        if (playerMovementInput.x != 0 || playerMovementInput.y != 0)
        {

            isMovementPressed = true;

        }
        else
        {

            isMovementPressed = false;

        }

    }

    void onRun(InputAction.CallbackContext context) {

        isRunning = context.ReadValueAsButton();
    
    }

    void handleRotation() {

        Vector3 targetPosition;

        targetPosition.x = playerMovement.x;
        targetPosition.y = 0.0f;
        targetPosition.z = playerMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed) {

            Quaternion targetRotation = Quaternion.LookRotation(targetPosition);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor);

        }
    
    }

    void handleGravity() {

        if (characterController.isGrounded)
        {

            float groundedGravity = -0.05f;
            playerMovement.y = groundedGravity;
            runMovement.y = groundedGravity;

        }
        else {

            float gravity = -9.8f;
            playerMovement.y += gravity;
            runMovement.y += gravity;
        
        }
    
    
    }

    void OnEnable() => inputActions.FindActionMap(PLAYER_GROUND).Enable();

    void OnDisable() => inputActions.FindActionMap(PLAYER_GROUND).Disable();

    
}

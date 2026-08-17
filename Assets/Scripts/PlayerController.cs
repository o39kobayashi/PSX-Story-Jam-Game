using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    public InputActionAsset inputActions;

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
    private GameObject cameraPos;

    [SerializeField]
    private Transform rotationalCameraTransform;

    // aiming camera variables

    private InputAction cameraAimAction;

    private const string CAMERA_AIM = "Aim";

    [SerializeField]
    public float horizontalOffset;
    [SerializeField]
    public float depthOffset;
    [SerializeField]
    public float aimRotationalFactor;

    [SerializeField]
    private float aimMultiplier;

    private Vector3 aimMovement;

    public bool isAiming;


    private void Awake() {

        playerMoveAction = inputActions.FindAction(PLAYER_MOVEMENT);

        playerMoveAction.started += onMovementInput;
        playerMoveAction.performed += onMovementInput;
        playerMoveAction.canceled += onMovementInput;

        playerRunAction = inputActions.FindAction(PLAYER_RUN);

        playerRunAction.started += onRun;
        playerRunAction.canceled += onRun;

        cameraAimAction = inputActions.FindAction(CAMERA_AIM);

        cameraAimAction.started += onAim;
        cameraAimAction.canceled += onAim;

    }

    void Start()
    {

    }

    void Update()
    {


        handleMovement();
        handleRotation();
        handleGravity();

        MovePlayer();

    }

    private void MovePlayer() {

        if (isAiming) {

            characterController.Move(aimMovement * Time.deltaTime);

        } else if (isRunning) {

            characterController.Move(runMovement * Time.deltaTime);

        } else {

            characterController.Move(playerMovement * Time.deltaTime);

        }


    }

    private void onMovementInput(InputAction.CallbackContext context) {

        playerMovementInput = context.ReadValue<Vector2>();

    }

    private void onAim(InputAction.CallbackContext context) {

        isAiming = context.ReadValueAsButton();

    }

    private void handleMovement() {

        Vector3 cameraForward = cameraPos.transform.forward;
        Vector3 cameraRight = cameraPos.transform.right;

        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * playerMovementInput.y) + (cameraRight * playerMovementInput.x);


        playerMovement.x = moveDirection.x * walkMultiplier;
        playerMovement.z = moveDirection.z * walkMultiplier;

        runMovement.x = moveDirection.x * runMultiplier;
        runMovement.z = moveDirection.z * runMultiplier;

        aimMovement.x = moveDirection.x * aimMultiplier;
        aimMovement.z = moveDirection.z * aimMultiplier;


        if (playerMovementInput.x != 0 || playerMovementInput.y != 0)
        {

            isMovementPressed = true;

        }
        else
        {

            isMovementPressed = false;

        }

    }

    private void onRun(InputAction.CallbackContext context) {

        isRunning = context.ReadValueAsButton();
    
    }

    private void handleRotation() {

        Vector3 cameraForward = cameraPos.transform.forward;
        Vector3 cameraRight = cameraPos.transform.right;

        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 targetDirection = (cameraForward * playerMovementInput.y) + (cameraRight * playerMovementInput.x);

        targetDirection.y = 0.0f;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed) {

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor * Time.deltaTime);

        }
    
    }

    private void handleGravity() {

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

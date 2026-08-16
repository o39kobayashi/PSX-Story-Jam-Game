using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private InputActionAsset inputActions;

    private InputAction playerMoveAction;

    private const string PLAYER_GROUND = "Player_Ground";
    private const string PLAYER_MOVEMENT = "Movement";
    private const string PLAYER_FIRE = "Fire";

    private Vector2 playerMovementInput;
    private Vector3 playerMovement;
    private bool isMovementPressed;

    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private float playerWalkSpeed;

    [SerializeField]
    private float rotationFactor = 1.0f;


    void Awake() {

        playerMoveAction = inputActions.FindAction(PLAYER_MOVEMENT);

        playerMoveAction.started += onMovementInput;
        playerMoveAction.performed += onMovementInput;
        playerMoveAction.canceled += onMovementInput;

    }

    void Start()
    {
        


    }

    void Update()
    {

        handleRotation();

        characterController.Move(playerMovement * Time.deltaTime);

    }

    void onMovementInput(InputAction.CallbackContext context) {

        playerMovementInput = context.ReadValue<Vector2>();

        playerMovement.x = playerMovementInput.x;

        playerMovement.z = playerMovementInput.y;

        if (playerMovementInput.x != 0 || playerMovementInput.y != 0)
        {

            isMovementPressed = true;

        }
        else
        {

            isMovementPressed = false;

        }

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

    void OnEnable() => inputActions.FindActionMap(PLAYER_GROUND).Enable();

    void OnDisable() => inputActions.FindActionMap(PLAYER_GROUND).Disable();

    
}

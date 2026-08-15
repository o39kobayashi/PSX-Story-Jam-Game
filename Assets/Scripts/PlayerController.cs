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


    void Awake() {

        playerMoveAction = inputActions.FindAction(PLAYER_MOVEMENT);

        // playerMoveAction.started += context => { Debug.Log("started");  };


        // lambda function => stores player input and assigns it to movement vector
        playerMoveAction.started += context => {

            playerMovementInput = context.ReadValue<Vector2>();
            
            playerMovement.x = playerMovementInput.x;
            
            playerMovement.z = playerMovementInput.y;

            Debug.Log("movement vector: " + playerMovement);

            if (playerMovementInput.x != 0 || playerMovementInput.y != 0) {
                
                isMovementPressed = true;

            } else {

                isMovementPressed = false;

            }
        };

    }

    void Start()
    {
        


    }

    void Update()
    {

        characterController.Move(playerMovement * Time.deltaTime);

    }

    void OnEnable() => inputActions.FindActionMap(PLAYER_GROUND).Enable();

    /*
    void OnEnable()
    {
        InputActionMap map = inputActions.FindActionMap(PLAYER_GROUND);

        print("Map: " + map);
        print("Map enabled before: " + map.enabled);

        map.Enable();

        print("Map enabled after: " + map.enabled);
        print("Movement enabled after: " + inputActions.FindAction(PLAYER_MOVEMENT).enabled);
    }
    */
    void OnDisable() => inputActions.FindActionMap(PLAYER_GROUND).Disable();

    
}

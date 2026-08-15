using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody rigidBody;

    [SerializeField]
    float moveSpeed = 10;

    private Vector2 moveDirection;

    public InputActionReference movePlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        moveDirection = movePlayer.action.ReadValue<Vector2>();

    }

    void FixedUpdate() {


        rigidBody.linearVelocity = new Vector3(moveDirection.x * moveSpeed, 0f, moveDirection.y * moveSpeed);
    
    
    }
}

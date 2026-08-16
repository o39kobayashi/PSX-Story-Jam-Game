using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private PlayerController playerController;
    //private InputActionAsset inputActions;
    private InputAction cameraInput;

    private const string CAMERA_MOVEMENT = "Camera";

    private Vector2 cameraMovementInput;

    private float cameraHorizontalInput;
    private float cameraVerticalInput;

    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private Transform cameraPivot;

    private Vector3 cameraSmoothVelocity = Vector3.zero;
    private float cameraSpeed = 0.2f;

    [SerializeField]
    private float cameraLookSpeed;
    [SerializeField]
    private float cameraPivotSpeed;

    private float lookAngle;
    private float pivotAngle;
    private float minimumPivotAngle = -35.0f;
    private float maximumPivotAngle = 35.0f;


    void Start() {

        cameraInput = playerController.inputActions.FindAction(CAMERA_MOVEMENT);

        cameraInput.performed += onCameraInput;

    }

    void LateUpdate() {

        FollowTarget();
        RotateCamera();

    }

    private void onCameraInput(InputAction.CallbackContext context) {

        cameraMovementInput = context.ReadValue<Vector2>();

        cameraHorizontalInput = cameraMovementInput.x;
        cameraVerticalInput = cameraMovementInput.y;
    
    }

    private void FollowTarget() {

        transform.position = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraSmoothVelocity, cameraSpeed);

    }

    private void RotateCamera() { 
    
        lookAngle = lookAngle + (cameraHorizontalInput * cameraLookSpeed);
        pivotAngle = pivotAngle + (cameraVerticalInput * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        Vector3 rotation = Vector3.zero;

        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;

        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    
    }
}

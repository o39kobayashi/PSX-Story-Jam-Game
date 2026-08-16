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

    [SerializeField]
    private Transform cameraTransform;
    private float defaultPosition;
    [SerializeField]
    private float cameraCollisionRadius;

    public LayerMask collisionLayers;

    private Vector3 cameraVectorPosition;

    [SerializeField]
    private float cameraCollisionOffset;
    [SerializeField]
    private float minimumCollisionOffset;

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


    private InputAction cameraAimAction;

    private const string CAMERA_AIM = "Aim";

    [SerializeField]
    private float horizontalOffset;
    [SerializeField]
    private float depthOffset;
    [SerializeField]
    private float aimRotationalFactor;
    private bool isAiming;




    void Start() {

        cameraInput = playerController.inputActions.FindAction(CAMERA_MOVEMENT);

        cameraInput.performed += onCameraInput;

        cameraAimAction = playerController.inputActions.FindAction(CAMERA_AIM);

        cameraAimAction.started += onAimInput;
        cameraAimAction.canceled += onAimInput;

        defaultPosition = cameraTransform.localPosition.z;
    }

    void LateUpdate() {

        if (isAiming) {

            FollowTargetAiming();

        } else {

            FollowTarget();

        }
        
        RotateCamera();
        CameraCollision();

    }

    private void onCameraInput(InputAction.CallbackContext context) {

        cameraMovementInput = context.ReadValue<Vector2>();

        cameraHorizontalInput = cameraMovementInput.x;
        cameraVerticalInput = cameraMovementInput.y;
    
    }

    private void onAimInput(InputAction.CallbackContext context) {

        isAiming = context.ReadValueAsButton();
    
    }

    private void FollowTarget() {

        transform.position = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraSmoothVelocity, cameraSpeed * Time.deltaTime);

    }

    private void FollowTargetAiming() {

        Vector3 offsetTargetPosition = targetTransform.position;
        offsetTargetPosition.x += horizontalOffset;
        offsetTargetPosition.z += depthOffset;

        transform.position = Vector3.SmoothDamp(transform.position, offsetTargetPosition, ref cameraSmoothVelocity, cameraSpeed * Time.deltaTime);

    }

    private void CameraCollision() {

        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers)) {

            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffset);

        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffset) {

            targetPosition = targetPosition - minimumCollisionOffset;

        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);

        cameraTransform.localPosition = cameraVectorPosition;



    }

    private void RotateCamera() {

        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = lookAngle + (cameraHorizontalInput * cameraLookSpeed);
        pivotAngle = pivotAngle + (cameraVerticalInput * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    
    }
}

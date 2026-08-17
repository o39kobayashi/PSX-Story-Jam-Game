using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Camera generalCamera;
    [SerializeField]
    private Camera aimCamera;

    [SerializeField]
    private Vector3 offsetVector;
    [SerializeField]
    private Transform relPlayerTransform;

    private InputAction aimCameraInput;

    private const string CAMERA_AIM = "Aim";

    private bool isAiming;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        generalCamera.enabled = true;
        aimCamera.enabled = false;

        aimCameraInput = playerController.inputActions.FindAction(CAMERA_AIM);

        aimCameraInput.started += onAim;
        aimCameraInput.canceled += onAim;

    }

    // Update is called once per frame
    private void LateUpdate()
    {

        Vector3 rotatedOffset = relPlayerTransform.rotation * offsetVector;

        // aimCamera.transform.position = relPlayerTransform.position + offsetVector;

        aimCamera.transform.position = relPlayerTransform.position + rotatedOffset;

        aimCamera.transform.LookAt(relPlayerTransform.position);

    }

    private void SwitchCameras() {

        generalCamera.enabled = !generalCamera.enabled;
        aimCamera.enabled = !aimCamera.enabled;
    
    }

    private void onAim(InputAction.CallbackContext context) {

        SwitchCameras();

        isAiming = context.ReadValueAsButton();

    }
}

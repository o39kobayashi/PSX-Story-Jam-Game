using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMechanics : MonoBehaviour
{

    [SerializeField]
    private PlayerController playerController;

    private InputAction fireAction;

    private const string REVOLVER_FIRE = "Fire";

    [SerializeField]
    private Revolver revolver;


    private void Awake()
    {

        fireAction = playerController.inputActions.FindAction(REVOLVER_FIRE);

        fireAction.performed += ctx => revolver.Shoot();

    }

    void Update()
    {

    }

}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMechanics : MonoBehaviour
{

    [SerializeField]
    private PlayerController playerController;

    private InputAction fireAction;
    private InputAction reloadAction;

    private const string REVOLVER_FIRE = "Fire";
    private const string REVOLVER_RELOAD = "Reload";

    [SerializeField]
    private Revolver revolver;


    private void Awake()
    {

        fireAction = playerController.inputActions.FindAction(REVOLVER_FIRE);

        fireAction.performed += ctx => revolver.Shoot();

        reloadAction = playerController.inputActions.FindAction(REVOLVER_RELOAD);

        reloadAction.performed += _ctx => revolver.Reload();

    }

    void Update()
    {

    }

}

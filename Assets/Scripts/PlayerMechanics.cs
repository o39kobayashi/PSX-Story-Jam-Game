using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMechanics : MonoBehaviour
{

    [SerializeField]
    private PlayerController playerController;

    private InputAction fireAction;
    private InputAction reloadAction;
    private InputAction switchAction;

    private const string REVOLVER_FIRE = "Fire";
    private const string REVOLVER_SWITCH = "Switch Bullet";
    private const string REVOLVER_RELOAD = "Reload";

    [SerializeField]
    private Revolver revolver;

    private const float MAX_HEALTH = 150.0f;
    public float currentHealth;

    private const float MAX_AETHER_METER = 100.0f;
    public float currentAetherMeter = 0.0f;


    private void Awake()
    {

        fireAction = playerController.inputActions.FindAction(REVOLVER_FIRE);

        fireAction.performed += ctx => revolver.Shoot();

        reloadAction = playerController.inputActions.FindAction(REVOLVER_RELOAD);

        reloadAction.performed += ctx => revolver.Reload();

        switchAction = playerController.inputActions.FindAction(REVOLVER_SWITCH);

        switchAction.performed += ctx => revolver.SwitchBulletType();

    }

    void Start() {


        currentHealth = MAX_HEALTH;
    
    
    }

    void Update()
    {

    }

    public void IncreaseAetherMeter(float essenceTaken) {

        if (currentAetherMeter < MAX_AETHER_METER) {

            currentAetherMeter += essenceTaken;

        }
    
    }

    public void TakeDamage(float damageTaken) {

        currentHealth -= damageTaken;

        if (currentHealth <= 0) {

            Die();
        }
    
    }

    public void SpendEssence() {

        currentAetherMeter = 0.0f;
    
    }

    void Die() {

        Debug.Log("YOU ARE DEAD");
    
    }


}

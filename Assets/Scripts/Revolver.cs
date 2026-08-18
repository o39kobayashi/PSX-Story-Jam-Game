using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Revolver : MonoBehaviour
{


    [SerializeField]
    private GameObject player;

    [SerializeField]
    private RangedEnemy rangedEnemy;

    [SerializeField]
    private Camera revolverCamera;

    [SerializeField]
    private Vector3 bulletStartingPoint;
    
    [SerializeField]
    private float fireRate;

    [SerializeField]
    private LayerMask hitscanLayers;

    [SerializeField]
    private int currentAmmo;

    private float timeLastShot;

    private const string RANGED_ENEMY_TAG = "RangedEnemy";
    private const string MELEE_ENEMY_TAG = "MeleeEnemy";

    private const string AETHER_ORB_TAG = "AetherOrb";
    private const string NORMAL_ORB_TAG = "NormalOrb";

    private const string CRIT_ESSENCE_TAG = "CriticalEssence";

    private const float HITSCAN_RANGE = 1000.0f;
    private const int MAX_AMMO = 6;

    private const float NORMAL_DMG = 10.0f;
    private const float CRIT_DMG = 50.0f;

    private const float ESSENCE_TAKEN = 25.0f;

    private bool isAetherBullet;
    void Start() {

        isAetherBullet = false;
    
    }

    public void Shoot() {

        if (timeLastShot + fireRate < Time.time) {

            if (currentAmmo <= 0) {

                // play revolve click sound
                Debug.Log("Out of ammo");
                return;
            
            }

            Ray hitscanRay = revolverCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

            if (Physics.Raycast(hitscanRay, out RaycastHit hit, HITSCAN_RANGE, hitscanLayers)) {

                Debug.Log("HIT: " + hit.collider.gameObject.name);

                if (!isAetherBullet)
                {

                    if (hit.collider.CompareTag(RANGED_ENEMY_TAG))
                    {

                        // RangedEnemy enemy = hit.collider.GetComponent<RangedEnemy>();
                        rangedEnemy.TakeDamage(NORMAL_DMG);


                    } else if (hit.collider.CompareTag(MELEE_ENEMY_TAG)) {

                        Debug.Log("Hit melee enemy");

                    }

                } else {

                    PlayerMechanics playerMechanics = player.GetComponent<PlayerMechanics>();

                    if (hit.collider.CompareTag(AETHER_ORB_TAG))
                    {

                        ProjectileBehavior aetherOrb = hit.collider.GetComponent<ProjectileBehavior>();
                        aetherOrb.GetDestroyed();

                        playerMechanics.IncreaseAetherMeter(ESSENCE_TAKEN);


                    } else if (hit.collider.CompareTag(CRIT_ESSENCE_TAG)) {

                        EssenceController essence = hit.collider.GetComponent<EssenceController>();
                        essence.GetDestroyed();

                        
                        rangedEnemy.TakeDamage(CRIT_DMG);
                        rangedEnemy.essenceAvailable = false;

                        playerMechanics.SpendEssence();

                    }

                
                }
            
            }

            Debug.Log("AMMO: " + currentAmmo);
            currentAmmo--;

            timeLastShot = Time.time;
        
        }
    
    }

    // make a coroutine if have time to implement a legit reload function
    public void Reload() {


        if (currentAmmo  == MAX_AMMO) {

            // maybe play differnt audio sound for click
            Debug.Log("CANNOT RELOAD ==> MAX AMMO");
            return;

        } else {

            Debug.Log("RELOADED");
            currentAmmo = MAX_AMMO;

        }
    
    }

    public void SwitchBulletType() {

        isAetherBullet = !isAetherBullet;

        if (isAetherBullet) {

            Debug.Log("Aether Bullet");

        } else {

            Debug.Log("Rusted Bullet");
        
        }
    
    }


}

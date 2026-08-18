using Unity.VisualScripting;
using UnityEngine;

public class Revolver : MonoBehaviour
{


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
    private const float HITSCAN_RANGE = 1000.0f;
    private const int MAX_AMMO = 6;

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

                        RangedEnemy enemy = hit.collider.GetComponent<RangedEnemy>();
                        enemy.TakeDamage();


                    } else if (hit.collider.CompareTag(MELEE_ENEMY_TAG))
                    {

                        Debug.Log("Hit melee enemy");

                    }

                } else {


                    Debug.Log("Aether Bullet Shot: aether logic");
                
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

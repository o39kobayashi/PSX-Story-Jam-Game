using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ProjectileBehavior : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemyObject;

    [SerializeField]
    private RangedEnemy rangedEnemy;

    private NavMeshAgent orb;

    private const string PLAYER_TAG = "Player";
    private const string RANGED_ENEMY_TAG = "RangedEnemy";

    private const string AETHER_ORB_TAG = "AetherOrb";
    private const string NORMAL_ORB_TAG = "NormalOrb";

    private const float START_UP_TIME = 1.5f;
    private float timer;

    private const float AETHER_ORB_DMG = 45.0f;
    private const float NORMAL_ORB_DMG = 15.0f;

    private bool alreadyShot = false;

    private Vector3 lastKnownPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player = GameObject.FindWithTag(PLAYER_TAG);

        enemyObject = GameObject.FindWithTag(RANGED_ENEMY_TAG);

        rangedEnemy = enemyObject.GetComponent<RangedEnemy>();

        timer = START_UP_TIME;

        orb = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.CompareTag(NORMAL_ORB_TAG)) {

            

            if (!alreadyShot)
            {

                ShootPlayer();

            }
            else {

                CheckDestination();
            
            }
        
        }

        if (gameObject.CompareTag(AETHER_ORB_TAG)) {

            MoveToPlayer();

        }

    }

    public void MoveToPlayer() {

        orb.destination = player.transform.position;
    
    }

    public void ShootPlayer() {

        timer -= Time.deltaTime;

        if (timer <= 0) {

            lastKnownPosition = player.transform.position;
            orb.destination = lastKnownPosition;

            alreadyShot = true;

        }
    
    }

    public void GetDestroyed() {

        rangedEnemy.currentOrbs--;
        Destroy(gameObject);
    
    }

    public void CheckDestination() {

        if (!orb.hasPath) {

            rangedEnemy.currentOrbs--;
            Destroy(gameObject);

        }
    
    }

    private void OnTriggerEnter(Collider other) {

        if (other.GetComponent<CharacterController>() != null) {

            PlayerMechanics playerMechanics = player.GetComponent<PlayerMechanics>();

            if (gameObject.CompareTag(NORMAL_ORB_TAG)) {
                
                playerMechanics.TakeDamage(NORMAL_ORB_DMG);

            } else {

                playerMechanics.TakeDamage(AETHER_ORB_DMG);
            
            }
        
        }

        Destroy(gameObject);

    }
}

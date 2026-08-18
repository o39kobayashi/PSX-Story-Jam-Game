using UnityEngine;
using UnityEngine.AI;
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

            Debug.Log("shouldve shot");

            lastKnownPosition = player.transform.position;
            orb.destination = lastKnownPosition;

            alreadyShot = true;

        }
    
    }

    public void GetDestroyed() {

        rangedEnemy.currentOrbs--;
        Debug.Log("Aether Orb destroyed");
        Destroy(gameObject);
    
    }

    public void CheckDestination() {

        if (!orb.hasPath) {

            rangedEnemy.currentOrbs--;
            Debug.Log("Aether Orb destroyed");
            Destroy(gameObject);

        }
    
    
    }
}

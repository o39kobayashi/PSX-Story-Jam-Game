using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class RangedEnemy : MonoBehaviour
{


    private NavMeshAgent enemy;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float verticalDistance;

    [SerializeField]
    private LayerMask mask;


    private float currentDistance;

    private float currentHealth;

    [SerializeField]
    private float maxDistance;

    private const float MAX_HEALTH = 100.0f;
    private const float MIN_RANGED_DISTANCE = 40.0f;
    private const float MAX_RANGED_DISTANCE = 25.0f;

    [SerializeField]
    private float timer = 5.0f;
    private float attackTimer;

    [SerializeField]
    private GameObject aetherOrbPrefab;

    [SerializeField]
    private Transform aetherSpawnPoint1;

    private const int MAX_NUM__ORBS = 5;
    public int currentOrbs;

    void Start()
    {

        enemy = GetComponent<NavMeshAgent>();

        attackTimer = timer;

        currentOrbs = 0;

        currentHealth = MAX_HEALTH;

    }

    void Update()
    {

        if (!InRange())
        {

            MoveToPlayer();

        }

        if (currentOrbs < MAX_NUM__ORBS) {

            FireOrb();

        }


    }

    private void FireOrb() {

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0) {

            attackTimer = timer;

            Debug.Log("Bullet should spawn");
            GameObject orbInstance = Instantiate(aetherOrbPrefab);

            orbInstance.GetComponent<NavMeshAgent>().Warp(aetherSpawnPoint1.position);

            currentOrbs++;

        }
    
    }

    private bool InRange() {

        float magnitude = Vector3.Distance(player.gameObject.transform.position, transform.position);

        float randomDistance = Random.Range(MIN_RANGED_DISTANCE, MAX_RANGED_DISTANCE);

        if (magnitude <= randomDistance) {

            return true;

        } else {

            return false;

        }
    
    }

    private void MoveToPlayer() {

        enemy.destination = Vector3.Lerp(transform.position, player.transform.position, 0.5f);
    }

    public void TakeDamage() {

        currentHealth -= 10;

        Debug.Log("Took Damage! Current Health: " + currentHealth);

        if (currentHealth <= 0.0f) {

            Die();
        
        }
    
    }

    private void Die() {

        Destroy(gameObject);
    
    }
}

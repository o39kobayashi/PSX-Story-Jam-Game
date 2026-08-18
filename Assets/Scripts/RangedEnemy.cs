using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
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
    private float timer = 1.5f;
    private float attackTimer;

    [SerializeField]
    private GameObject aetherOrbPrefab;
    [SerializeField]
    private GameObject normalOrbPrefab;
    [SerializeField]
    private GameObject essencePrefab;

    [SerializeField]
    private Transform aetherSpawnPoint1;
    [SerializeField]
    private Transform normalSpawnPoint1;
    [SerializeField]
    private Transform essenceSpawnPoint;

    private const int MAX_NUM__ORBS = 5;
    public int currentOrbs;

    private PlayerMechanics playerMechanics;

    private const float MAX_AETHER_METER = 100.0f;

    public bool essenceAvailable = false;

    void Start()
    {

        playerMechanics = player.GetComponent<PlayerMechanics>();

        enemy = GetComponent<NavMeshAgent>();

        attackTimer = timer;

        currentOrbs = 0;

        currentHealth = MAX_HEALTH;

    }

    void Update()
    {

        if (!essenceAvailable && playerMechanics.currentAetherMeter == MAX_AETHER_METER) {

            GameObject essenceInstance = Instantiate(essencePrefab, essenceSpawnPoint.position, essenceSpawnPoint.rotation);

            essenceAvailable = true;
        
        }

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

            float spawnChance = Random.Range(0.0f, 1.0f);
            GameObject orbInstance;

            if (spawnChance <= 0.60f)
            {

                orbInstance = Instantiate(aetherOrbPrefab);
                orbInstance.GetComponent<NavMeshAgent>().Warp(aetherSpawnPoint1.position);

            }
            else {

                orbInstance = Instantiate(normalOrbPrefab);
                orbInstance.GetComponent<NavMeshAgent>().Warp(normalSpawnPoint1.position);

            }

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

    public void TakeDamage(float damageTaken) {

        currentHealth -= damageTaken;

        Debug.Log("Took Damage! Current Health: " + currentHealth);

        if (currentHealth <= 0.0f) {

            Die();
        
        }
    
    }

    private void Die() {

        Destroy(gameObject);
    
    }
}

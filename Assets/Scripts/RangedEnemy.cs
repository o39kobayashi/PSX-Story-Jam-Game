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
    private const float MAX_RANGED_DISTANCE = 15.0f;


    void Start()
    {

        enemy = GetComponent<NavMeshAgent>();

        currentHealth = MAX_HEALTH;

    }

    void Update()
    {

        if (!InRange()) {

            MoveToPlayer();

        }

    }

    private bool InRange() {

        float magnitude = Vector3.Distance(player.gameObject.transform.position, transform.position);

        if (magnitude <= MAX_RANGED_DISTANCE) {

            return true;

        } else {

            return false;

        }
    
    }

    private void MoveToPlayer() {

        enemy.destination = Vector3.Lerp(transform.position, player.gameObject.transform.position, 0.5f);
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

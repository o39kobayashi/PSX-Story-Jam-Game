using UnityEngine;
using UnityEngine.AI;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player = GameObject.FindWithTag(PLAYER_TAG);

        enemyObject = GameObject.FindWithTag(RANGED_ENEMY_TAG);

        rangedEnemy = enemyObject.GetComponent<RangedEnemy>();

        orb = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

        MoveToPlayer();

    }

    public void MoveToPlayer() {

        orb.destination = player.transform.position;
    
    }

    public void GetDestroyed() {

        rangedEnemy.currentOrbs--;
        Debug.Log("Aether Orb destroyed");
        Destroy(gameObject);
    
    }
}

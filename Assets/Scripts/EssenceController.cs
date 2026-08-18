using UnityEngine;

public class EssenceController : MonoBehaviour
{

    private Vector3 rotationSpeed;
    private const float MULTIPLIER = 1.5f;

    private const string ESSENCE_SPAWN = "EssenceSpawn";

    private GameObject essenceSpawn;



    void Start() {

        essenceSpawn = GameObject.FindWithTag(ESSENCE_SPAWN);

        rotationSpeed = new Vector3(50, 50, 50);
    
    }


    void Update() {

        transform.position = essenceSpawn.transform.position;

        transform.Rotate(rotationSpeed * Time.deltaTime * MULTIPLIER);
    
    }

    public void GetDestroyed() {

        Destroy(gameObject);
    
    
    }


}

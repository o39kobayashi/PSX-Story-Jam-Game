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

    private float timeLastShot;

    private const float HITSCAN_RANGE = 1000.0f;

    public void Shoot() {

        if (timeLastShot + fireRate < Time.time) {

            Ray hitscanRay = revolverCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

            if (Physics.Raycast(hitscanRay, out RaycastHit hit, HITSCAN_RANGE, hitscanLayers)) {

                Debug.Log("HIT: " + hit.collider.gameObject.name);
            
            }

            timeLastShot = Time.time;
        
        }
    
    }


}

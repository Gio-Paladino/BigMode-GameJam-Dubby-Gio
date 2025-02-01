using UnityEngine;

public class Explosive : MonoBehaviour
{
    public GameObject explosion;
    public float explosionForce, radius;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Explosion");
            GameObject _explosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(_explosion, 3);
            Blast();
            Destroy(gameObject);
        }
    }

    void Blast(){
        Debug.Log("Blast");
        Collider[] nearbyCollider = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in nearbyCollider){
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if(rb != null){
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }
    }
}

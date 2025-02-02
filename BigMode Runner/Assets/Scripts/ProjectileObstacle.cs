using UnityEngine;

public class ProjectileObstacle : MonoBehaviour
{
    [SerializeField]
    private Rigidbody projectileRigidbody;
    [SerializeField]
    private GameObject projectileObject;
    [SerializeField]
    private float speed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            projectileRigidbody.AddForce(projectileObject.transform.TransformDirection(Vector3.forward) * speed, ForceMode.Impulse);
        }
    }
}

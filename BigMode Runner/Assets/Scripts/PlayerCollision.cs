using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public delegate void Crash();
    public event Crash OnCrash;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Obstacle struck");

            OnCrash?.Invoke();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            OnCrash?.Invoke();
        }
    }
}

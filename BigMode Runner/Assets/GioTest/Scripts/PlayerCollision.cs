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
}

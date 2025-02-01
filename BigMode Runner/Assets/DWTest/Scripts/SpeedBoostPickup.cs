using UnityEngine;

public class SpeedBoostPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerDW>();

        if (player != null )
        {
            player.GainBoost();
            gameObject.SetActive(false);
        }
    }
}

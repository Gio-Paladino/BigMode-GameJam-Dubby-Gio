using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum pickupType
    {
        Boost,
        Jump
    }

    [SerializeField]
    pickupType type;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerBase>();

        if (player != null )
        {
            if (type == pickupType.Boost)
            {
                player.GainBoost();
            }
            else if (type == pickupType.Jump)
            {
                player.GainJump();                
            }
            gameObject.SetActive(false);
        }
    }
}

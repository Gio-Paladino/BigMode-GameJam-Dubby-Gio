using UnityEngine;

public class EffectPad : MonoBehaviour
{
    public enum effectType
    {
        Boost,
        jump,
        jumpBoost,
        deathPad
    }

    [SerializeField]
    private effectType type;
    [SerializeField]
    private float boostAmmount;
    [SerializeField]
    private float boostDuration;
    [SerializeField]
    private float jumpForce;

    private Vector3 boostDirection;

    private void Start()
    {
        boostDirection = transform.TransformDirection(Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerBase>();

        if (player != null )
        {
            switch (type)
            {
                case effectType.Boost:
                    player.DoSpeedBoost(boostDuration, boostAmmount);
                    break;
                case effectType.jump:
                    player.DoJump(jumpForce, Vector3.up);
                    break;
                case effectType.jumpBoost:
                    player.DoSpeedBoost(boostDuration, boostAmmount);
                    player.DoJump(jumpForce, Vector3.up);
                    break;
                case effectType.deathPad:
                    player.DoJump(jumpForce, Vector3.up);
                    player.Die();
                    Animator ani = GetComponent<Animator>();
                    if (ani != null){
                        ani.SetBool("Spring", true);
                    }
                    break;
            }
        }
    }
}

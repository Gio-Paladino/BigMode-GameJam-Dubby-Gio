using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBase : MonoBehaviour
{
    [Header("Basic Parameters")]

    [SerializeField]
    private float maxForwardSpeed;
    [SerializeField]
    private float maxHorizontalSpeed;
    [SerializeField]
    private float sideForce;
    [SerializeField]
    private float forwardforce;
    [SerializeField]
    private float slideStopForce;

    [Header("Powerup Parameters")]

    [SerializeField]
    private float boostAmmount;
    [SerializeField]
    private float boostDuration;
    [SerializeField]
    private float jumpForce;

    [Header("Components")]
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform CeilingCheckCastOrigin;
    [SerializeField]
    private GameObject PlayerModel;
    [SerializeField]
    private GameObject DeadText;

    private Rigidbody rb;
    private CapsuleCollider col;
    private PlayerCollision collisionEvents;

    private bool sliding = false;
    private bool tryToStopSliding = false;
    private bool Dead = false;
    private bool hasBoost = false;
    private bool hasJump = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

    }

     private void Awake()
    {
        collisionEvents = GetComponent<PlayerCollision>();    
    }

    private void OnEnable()
    {
        collisionEvents.OnCrash += Die;
    }
    private void OnDisable()
    {
        collisionEvents.OnCrash -= Die;
    }

    public void Die()
    {
        if (!Dead){
            Debug.Log("Spawn ragdoll");
            GameObject ragdoll = Instantiate(Resources.Load<GameObject>("Ragdoll"), transform);
            ragdoll.transform.GetChild(0).GetChild(1).GetChild(9).GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity;
            PlayerModel.SetActive(false);
            cam.GetComponent<CameraScript>().StartDeathCam(ragdoll);
            DeadText.SetActive(true);
            Dead = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dead){
            ToggleSlide();
            ApplyForwardForce();
            HorizontalMovement();
            PowerupCheck();
            CheckGround();
        }else{
            RestartOnPress();
        }

        
    }

    private void ToggleSlide()
    {
        if (!sliding && Input.GetKeyDown(KeyCode.S))
        {
            col.height = col.height / 2f;
            col.center += Vector3.down * 0.5f;
            sliding = true;
            PlayerModel.GetComponent<Animator>().SetBool("Sliding", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            tryToStopSliding = true;
        }

        if (tryToStopSliding)
        {
            Debug.Log("tryToStopSliding = " + tryToStopSliding);
            Debug.DrawRay(CeilingCheckCastOrigin.position, transform.TransformDirection(Vector3.up) * 2f, Color.red, 2f, false);
            RaycastHit hit;
            if(!Physics.Raycast(CeilingCheckCastOrigin.position, transform.TransformDirection(Vector3.up), out hit, 1f))
            {
                col.height = col.height * 2f;
                col.center += Vector3.up * 0.5f;
                sliding = false;
                tryToStopSliding = false;
                PlayerModel.GetComponent<Animator>().SetBool("Sliding", false);
            }
        }
    }

    private void ApplyForwardForce()
    {
        if (rb.linearVelocity.magnitude < maxForwardSpeed)
            rb.AddForce(transform.TransformDirection(Vector3.forward) * forwardforce * Time.deltaTime);
    }

    private void HorizontalMovement()
    {
        if (Input.GetKey(KeyCode.A) && !sliding)
        {
            rb.AddForce(transform.TransformDirection(Vector3.left) * sideForce * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) && !sliding)
        {
            rb.AddForce(transform.TransformDirection(Vector3.right) * sideForce * Time.deltaTime);
        }

        if (sliding)
        {
           rb.AddForce(new Vector3(-rb.linearVelocity.x, 0f, 0f).normalized * slideStopForce * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    private void CheckGround(){
        RaycastHit hit;
        if(Physics.Raycast(CeilingCheckCastOrigin.position, transform.TransformDirection(Vector3.down), out hit, 1.5f))
        {
            PlayerModel.GetComponent<Animator>().SetBool("Falling", false);
        }else{
            PlayerModel.GetComponent<Animator>().SetBool("Falling", true);
        }
    }
    
    private void RestartOnPress(){
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GainBoost()
    {
        hasBoost = true;
    }
    public void GainJump()
    {
        hasJump = true;
    }



    public void PowerupCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift) && hasBoost)
        {
            DoSpeedBoost(boostDuration, boostAmmount);
            rb.AddForce(transform.TransformDirection(Vector3.forward) * boostAmmount, ForceMode.Impulse);
            hasBoost = false;
        }

        if (Input.GetKey(KeyCode.Space) && hasJump)
        {
            DoJump(jumpForce, Vector3.up);
            hasJump = false;
        }
    }

    public void DoJump(float force, Vector3 direction)
    {
        rb.AddForce(transform.TransformDirection(direction) * force, ForceMode.Impulse);
    }

    public void DoSpeedBoost(float duration, float ammount)
    {
        StartCoroutine(BoostMaxSpeed(duration, ammount));
    }

    IEnumerator BoostMaxSpeed(float duration, float ammount)
    {
        maxForwardSpeed += ammount;
        PlayerModel.GetComponent<Animator>().SetBool("Boosting", true);
        yield return new WaitForSeconds(duration);
        PlayerModel.GetComponent<Animator>().SetBool("Boosting", false);
        maxForwardSpeed -= ammount;
    }
}

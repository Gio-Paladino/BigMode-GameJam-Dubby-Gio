using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGio : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider col;
    private PlayerCollision collisionEvents;
    [SerializeField]
    private Camera cam;
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
    private bool sliding = false;
    private bool tryToStopSliding = false;
    private bool Dead = false;

    [SerializeField]
    private Transform CeilingCheckCastOrigin;

    [SerializeField]
    private GameObject PlayerModel;

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
        collisionEvents.OnCrash += InvokeRagdoll;
    }
    private void OnDisable()
    {
        collisionEvents.OnCrash -= InvokeRagdoll;
    }

    void InvokeRagdoll()
    {
        if (!Dead){
            Debug.Log("Spawn ragdoll");
            GameObject ragdoll = Instantiate(Resources.Load<GameObject>("Ragdoll"), transform);
            PlayerModel.SetActive(false);
            cam.GetComponent<CameraScript>().StartDeathCam(ragdoll);
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
    
    private void RestartOnPress(){
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

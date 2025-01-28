using UnityEngine;

public class PlayerGio : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider col;

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

    [SerializeField]
    private Transform CeilingCheckCastOrigin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        ToggleSlide();
        ApplyForwardForce();
        HorizontalMovement();
        
    }

    private void ToggleSlide()
    {
        if (!sliding && Input.GetKeyDown(KeyCode.S))
        {
            col.height = col.height / 2f;
            col.center += Vector3.down * 0.25f;
            sliding = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            tryToStopSliding = true;
        }

        if (tryToStopSliding)
        {
            Debug.Log("tryToStopSliding = " + tryToStopSliding);
            Debug.DrawRay(CeilingCheckCastOrigin.position, Vector3.up * 2f, Color.red, 2f, false);
            RaycastHit hit;
            if(!Physics.Raycast(CeilingCheckCastOrigin.position, Vector3.up, out hit, 1f))
            {
                col.height = col.height * 2f;
                col.center += Vector3.up * 0.25f;
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
}

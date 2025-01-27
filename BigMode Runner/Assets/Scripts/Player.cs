using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float maxForwardSpeed;
    [SerializeField]
    private float maxHorizontalSpeed;
    [SerializeField]
    private float sideForce;
    [SerializeField]
    private float forwardforce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyForwardForce();
        HorizontalMovement();
    }

    private void ApplyForwardForce()
    {
        if (rb.linearVelocity.magnitude < maxForwardSpeed)
            rb.AddForce(transform.TransformDirection(Vector3.forward) * forwardforce * Time.deltaTime);
    }

    private void HorizontalMovement()
    {
            if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(transform.TransformDirection(Vector3.left) * sideForce * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.TransformDirection(Vector3.right) * sideForce * Time.deltaTime);
        }
    }
}

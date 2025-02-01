using UnityEngine;

public class PlayerDW : MonoBehaviour
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
    private void FixedUpdate()
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
            rb.AddRelativeTorque(transform.TransformDirection(Vector3.down) * sideForce * Time.deltaTime);
            rb.AddForce(transform.TransformDirection(Vector3.left) * sideForce/10 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeTorque(transform.TransformDirection(Vector3.up) * sideForce * Time.deltaTime);
            rb.AddForce(transform.TransformDirection(Vector3.right) * sideForce/10 * Time.deltaTime);

        }
    }


    /*
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
    */
}

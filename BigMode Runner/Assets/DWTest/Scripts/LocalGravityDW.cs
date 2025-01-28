using UnityEngine;

public class LocalGravityDW : MonoBehaviour
{
 [SerializeField]
    private Transform castOrigin;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float gravityStrength;
    [SerializeField]
    private GameObject[] lookPath;
    [SerializeField]
    private float pointThreshold;

    private int currentPathPoint;

    private Vector3 currentGravity;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (castOrigin == null)
            castOrigin = GetComponent<Transform>();

        currentGravity = transform.TransformDirection(Vector3.down);
    }

    void Update()
    {
        rb.AddForce(currentGravity * gravityStrength * Time.deltaTime);

        Quaternion localZRotation = Quaternion.identity;
        Quaternion localXRotation = Quaternion.identity;
        Quaternion localYRotation = Quaternion.identity;
        Quaternion targetAngle = Quaternion.identity;


        RaycastHit hit;
        if (Physics.Raycast(castOrigin.position, currentGravity, out hit, 50f))
        {
            Debug.DrawRay(castOrigin.position, currentGravity * hit.distance, Color.yellow, 2f, false);
            if (-hit.normal != currentGravity)
                currentGravity = -hit.normal;

            float forwardDifference = Vector3.Angle(hit.normal, transform.TransformDirection(Vector3.forward)) - 90;
            localZRotation = Quaternion.LookRotation(transform.TransformDirection(Vector3.forward), hit.normal);
            localXRotation = Quaternion.AngleAxis(forwardDifference, Vector3.left);
        }

        float distanceToPoint = (transform.position - lookPath[currentPathPoint].transform.position).magnitude;

        if (distanceToPoint < pointThreshold)
        {
            // calculate the rotation of the y-axis from the difference from the current path point and the next path point.
        }


        targetAngle = localZRotation * localXRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * 4);


    }

    public Vector3 GetLocalUp()
    {
        return -currentGravity;
    }
}

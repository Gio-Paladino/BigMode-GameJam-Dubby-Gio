using UnityEngine;

public class RingSpin : MonoBehaviour
{
    public float speed;

    private Vector3 rotationAxis;

    void FixedUpdate()
    {
        rotationAxis += Vector3.forward * Time.deltaTime * speed;

        transform.rotation = Quaternion.Euler(rotationAxis);
    }
}

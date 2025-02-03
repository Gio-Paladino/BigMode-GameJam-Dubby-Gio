using UnityEngine;

public class MoveBlock : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update(){
        transform.Translate(-Vector3.forward * 50 * Time.deltaTime);
    }
}

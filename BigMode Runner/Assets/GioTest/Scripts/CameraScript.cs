using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private bool playerIsDead = false;
    private GameObject playerRagdoll;

   
    void Update()
    {
        if(playerIsDead) {
            LookAtRagdoll(playerRagdoll.transform.GetChild(0).GetChild(1).GetChild(9).position);
        }
    }

    private void LookAtRagdoll(Vector3 targetPosition){
        if (playerRagdoll != null){
            Quaternion lookdirection = Quaternion.LookRotation((targetPosition - transform.position).normalized);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookdirection, Time.deltaTime * 10);
            //transform.rotation = lookdirection;
        }
    }
     public void StartDeathCam(GameObject ragdoll){
        transform.SetParent(null);
        playerIsDead = true;
        playerRagdoll = ragdoll;
    }
}

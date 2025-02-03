using Unity.VisualScripting;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject WinText;
    [SerializeField]
    private GameObject Model;
    [SerializeField]
    private GameObject Player;
    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Switch hit to end level");
            Model.GetComponent<Animator>().SetBool("SwitchSwitch", true);
            WinText.SetActive(true);
            Player.GetComponent<PlayerBase>().setDead(true);
        }
    }
}

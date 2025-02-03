using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueMenu : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("Stage 2");
    }

}

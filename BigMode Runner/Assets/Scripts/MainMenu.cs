using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("StorySplashScreen");
    }

    public void QuitGame(){
        Application.Quit();
    }
}

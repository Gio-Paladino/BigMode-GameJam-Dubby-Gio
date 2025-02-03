using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private VideoPlayer vp;
    [SerializeField]
    private GameObject canvas;
    private AudioSource audioPlayer;

    void Start()
    {
        vp.loopPointReached += OnLoopPointReached;
        audioPlayer = GetComponent<AudioSource>();
    }

    void OnLoopPointReached(VideoPlayer vp)
    {
        canvas.SetActive(true);
    }

    void Update(){
       if (vp.frame >= 1000 && !audioPlayer.isPlaying){
            Debug.Log("Playing title music");
            audioPlayer.Play();
       } 
    }
   

}

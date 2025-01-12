using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public AudioSource uiSound;
    public void ReloadGame()
    {
        uiSound.Play();
        
    }

    public void QuitGame()
    {
        Application.Quit();
        uiSound.Play();

    }
}

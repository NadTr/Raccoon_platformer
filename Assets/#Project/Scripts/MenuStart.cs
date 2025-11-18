using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
       public void OnPlayButton ()
    {
        SceneManager.LoadScene("Level1");
    }
    public void OnQuitButton ()
    {
        Application.Quit();
    }
}
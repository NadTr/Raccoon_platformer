using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
       public void OnPlayButton ()
    {
        SceneManager.LoadScene("FirstLevelInitialize");
    }
    public void OnQuitButton ()
    {
        Application.Quit();
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEnd : MonoBehaviour
{
    private TMP_Text textEndGame;

    void Start()
    {
        if (this.transform.Find("EndText").TryGetComponent<TMP_Text>(out TMP_Text TMPText))
        {
            this.textEndGame = TMPText; 
            SetText();
        }
    }
    public void OnPlayButton ()
    {
        SceneManager.LoadScene("FirstLevelInitialize");
    }
    public void OnQuitButton ()
    {
        Application.Quit();
    }
    private void SetText()
    {
        string minutes = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("gameTime")).ToString("%m' min'");
        string secondes = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("gameTime")).ToString("%s' sec'");

        textEndGame.SetText($" Chestnuts caught: {PlayerPrefs.GetInt("chestnuts")}/100 \n Number of falls: {PlayerPrefs.GetInt("falls")} \n Time to complete: {minutes} and {secondes}");
    }
}
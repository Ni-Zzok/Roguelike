using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject HighscoresPopup;

    public void Start()
    {
        HighscoresPopup.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("help");
    }

    public void HighscoresOn()
    {
        HighscoresPopup.SetActive(true);
    }
    public void HighscoresOff()
    {
        HighscoresPopup.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
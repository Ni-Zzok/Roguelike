using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitPopup : MonoBehaviour
{
    public static bool isExitPopupActive;
    public GameObject exitPopup;

    void Start()
    {
        isExitPopupActive = false;
        exitPopup.SetActive(false);
    }

    public void ShowExitPopup()
    {
        isExitPopupActive = true;
        exitPopup.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void HideExitPopup()
    {
        isExitPopupActive = false;
        exitPopup.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public string nextLevelName; // ��� ���������� ������

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName); // ��������� ��������� �������
    }
}

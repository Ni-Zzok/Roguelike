using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public GameObject pauseMenu;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeHit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            animator.SetTrigger("Death");
            Invoke("StopGame", 2.1f);
            //StopGame();
        }
    }

    public void Healing(int bonusHealth)
    {
        health += bonusHealth;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void StopGame()
    {
        PauseGame();
        //Destroy(gameObject);
    }
}

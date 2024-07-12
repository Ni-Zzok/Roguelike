using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private bool roomActive = false;


    [Header("Walls")]
    public GameObject[] walls;
    //public Gameobject wallEffect;
    public GameObject door;

    [Header("Enemies")]
    public GameObject[] enemyTypes;
    public Transform[] enemySpawner;

    //[Header("Powerups")]
    //public GameObject shield;
    //public GameObject healthPotion;

    [HideInInspector] public List<GameObject> enemies; // Corrected attribute name

    private RoomVariants variants;
    private bool spawned;
    private bool wallsDestroyed=false;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !spawned)
        {
            spawned = true;
            roomActive = true;

            foreach (Transform spawner in enemySpawner)
            {
                int rand = Random.Range(0, 11);
                if (rand < 10)
                {
                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                    GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity);
                    enemy.transform.parent = transform;
                    enemies.Add(enemy);
                }
                // Uncomment and modify as needed for powerup spawning
                // else if (rand == 9)
                // {
                //     Instantiate(healthPotion, spawner.position, Quaternion.identity);
                // }
                // else if (rand == 10)
                // {
                //     Instantiate(shield, spawner.position, Quaternion.identity);
                // }
            }
        }

        StartCoroutine(CheckEnemies());
    }

    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyWalls();
    }

    public void DestroyWalls()
    {
        if (roomActive) // Destroy walls only when roomActive is true
        {
            foreach (GameObject wall in walls)
            {
                if (wall != null && wall.transform.childCount != 0)
                {
                    // Instantiate(wallEffect, wall.transform.position, Quaternion.identity); // Uncomment if wallEffect is defined
                    Destroy(wall);
                }
            }
            wallsDestroyed = true;
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (wallsDestroyed && other.CompareTag("Door"))
        {
            Destroy(other.gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 1f;
    private float nextFireTime = 0f;
    public int bulletDamage = 10;
    public GameObject player;
    public float speed = 3f;
    private Vector2 direction = Vector2.right;
    public ContactFilter2D filter;
    private Collider2D collider;
    private AddRoom room;
    public int health;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        room = GetComponentInParent<AddRoom>();

        // Ищем игрока в комнате и обновляем переменную player
        GameObject newPlayer = GameObject.FindGameObjectWithTag("Player");
        if (newPlayer != null)
        {
            player = newPlayer;
        }

        // Вызываем метод Shoot() при спавне врага
        Shoot();
    }

    void Update()
    {
        if (health <= 0)
        {
            //animator.SetTrigger("Dead");
            Destroy(gameObject, 0.4f); // Уничтожаем врага, если его здоровье меньше или равно нулю
            room.enemies.Remove(gameObject); // Удаляем врага из списка текущих врагов в комнате
        }

        // Ищем игрока в комнате и обновляем переменную player
        GameObject newPlayer = GameObject.FindGameObjectWithTag("Player");
        if (newPlayer != null && newPlayer != player)
        {
            player = newPlayer;
        }

        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Block") || col.gameObject.CompareTag("Wall") ||
            col.gameObject.CompareTag("Stone") || col.gameObject.CompareTag("Floor"))
        {
            direction = new Vector2(-direction.x, direction.y);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDamage(bulletDamage);
        }

        Vector2 direction = (player.transform.position - bulletSpawnPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletScript.GetBulletSpeed();
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Наносим урон врагу
    }
}

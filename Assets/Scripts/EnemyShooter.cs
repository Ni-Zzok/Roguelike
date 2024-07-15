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
    private new Collider2D collider;
    private AddRoom room;
    public int health;

    public Animator animator;
    private bool facingRight = true;
    private Health playerHealth;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        room = GetComponentInParent<AddRoom>();
        animator = GetComponent<Animator>();

        // ���� ������ � ������� � ��������� ���������� player
        GameObject newPlayer = GameObject.FindGameObjectWithTag("Player");
        if (newPlayer != null)
        {
            player = newPlayer;
        }

        // �������� ����� Shoot() ��� ������ �����
        Shoot();
    }

    void Update()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Dead");
            Destroy(gameObject, 0.2f); // ���������� �����, ���� ��� �������� ������ ��� ����� ����
            room.enemies.Remove(gameObject); // ������� ����� �� ������ ������� ������ � �������
        }

        // ���� ������ � ������� � ��������� ���������� player
        GameObject newPlayer = GameObject.FindGameObjectWithTag("Player");
        if (newPlayer != null && newPlayer != player)
        {
            player = newPlayer;
        }
        playerHealth = player.GetComponent<Health>();
        if (playerHealth.health <= 0)
        {
            animator.SetBool("PlayerDead", true);
        }

        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;

        if (direction.x >= 0 || direction.x < 0)
        {
            animator.SetBool("MoveX", true);
        }
        else
        {
            animator.SetBool("MoveX", false);
        }
        float moveX = 0f;
        moveX = direction.x > 0f ? 1f : -1f;
        if (moveX > 0 && !facingRight || moveX < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
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
        Invoke("ShootAnim", 0.5f);
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
        health -= damage; // ������� ���� �����
    }
    void ShootAnim()
    {
        animator.SetTrigger("Shoot");
    }
}

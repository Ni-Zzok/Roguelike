using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private float bulletSpeed = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }


    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeHit(damage);
            }
            Destroy(gameObject);
        }

        // Проверяем, попали ли мы в стену
        else if (collision.gameObject.CompareTag("Block"))
        {
            // Уничтожаем стрелу после попадания в стену
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Уничтожаем стрелу после попадания в стену
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * bulletSpeed;
    }
}

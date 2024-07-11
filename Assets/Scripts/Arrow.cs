using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage;  // Урон, который наносит стрела

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, попали ли мы во врага
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Получаем компонент Enemy у объекта, в который попали
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // Если компонент Enemy найден, наносим урон
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Уничтожаем стрелу после попадания
            Destroy(gameObject);
        }

        // Проверяем, попали ли мы в стену
        else if (collision.gameObject.CompareTag("Block"))
        {
            // Уничтожаем стрелу после попадания в стену
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            // Уничтожаем стрелу после попадания в стену
            Destroy(gameObject);
        }
    }
}

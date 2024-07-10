using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health; // Здоровье врага
    public float speed; // Скорость перемещения
    public int damage; // Урон, который враг наносит игроку
    public float damageInterval = 1.0f; // Интервал между нанесением урона
    private Transform player; // Ссылка на трансформ игрока
    private bool isAttacking = false; // Флаг, указывающий на то, идет ли атака

    private AddRoom room; // Ссылка на компонент AddRoom, чтобы управлять списком врагов

    private void Start()
    {
        // Находим объект игрока на сцене
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Получаем ссылку на родительский объект AddRoom
        room = GetComponentInParent<AddRoom>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject); // Уничтожаем врага, если его здоровье меньше или равно нулю
            room.enemies.Remove(gameObject); // Удаляем врага из списка текущих врагов в комнате
        }
        else
        {
            MoveTowardsPlayer(); // Двигаемся к игроку, если еще живы
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Вычисляем направление к игроку
            Vector2 direction = (player.position - transform.position).normalized;

            // Перемещаем врага в этом направлении с учетом скорости
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Наносим урон врагу
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, столкнулся ли враг с игроком
        if (collision.gameObject.CompareTag("Player") && !isAttacking)
        {
            // Начинаем атаку по контакту
            StartCoroutine(DealDamageOverTime(collision.gameObject.GetComponent<Player>()));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Проверяем, прекратился ли контакт с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            // Прекращаем атаку
            StopCoroutine(DealDamageOverTime(collision.gameObject.GetComponent<Player>()));
            isAttacking = false;
        }
    }

    private IEnumerator DealDamageOverTime(Player player)
    {
        isAttacking = true; // Устанавливаем флаг атаки

        while (isAttacking)
        {
            if (player != null)
            {
                player.TakeDamage(damage); // Наносим урон игроку
            }
            yield return new WaitForSeconds(damageInterval); // Ждем до следующей атаки
        }
    }
}

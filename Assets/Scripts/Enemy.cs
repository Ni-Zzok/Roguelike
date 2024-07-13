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

    public Animator animator;
    private bool facingRight = true;

    private void Start()
    {
        // Находим объект игрока на сцене
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Получаем ссылку на родительский объект AddRoom
        room = GetComponentInParent<AddRoom>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Dead");
            Destroy(gameObject, 0.4f); // Уничтожаем врага, если его здоровье меньше или равно нулю
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

            if (direction.x < 0 && facingRight)
            {
                Flip();
            }

            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }

            // Перемещаем врага в этом направлении с учетом скорости
            transform.Translate(direction * speed * Time.deltaTime);

            // Определяем, ближе к какому направлению двигается противник
            float moveX = 0f;
            float moveY = 0f;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                // Движение по горизонтали
                moveX = direction.x > 0f ? 1f : -1f;
            }
            else
            {
                // Движение по вертикали
                moveY = direction.y > 0f ? 1f : -1f;
            }
            // Устанавливаем значения moveX и moveY для анимации
            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveY", moveY);

            // Меняем направление спрайта врага
            if (moveX > 0 && !facingRight || moveX < 0 && facingRight)
            {
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
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
            Health playerHealth = player.GetComponent<Health>();
            StartCoroutine(DealDamageOverTime(playerHealth));
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        // Проверяем, прекратился ли контакт с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            // Прекращаем атаку
            Health playerHealth = player.GetComponent<Health>();
            StopCoroutine(DealDamageOverTime(playerHealth));
            isAttacking = false;
        }
    }

    private IEnumerator DealDamageOverTime(Health health)
    {
        isAttacking = true; // Устанавливаем флаг атаки

        while (isAttacking)
        {
            if (health != null)
            {
                health.TakeHit(damage); // Наносим урон игроку
            }
            yield return new WaitForSeconds(damageInterval); // Ждем до следующей атаки
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

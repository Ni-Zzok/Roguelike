using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health; // �������� �����
    public float speed; // �������� �����������
    public int damage; // ����, ������� ���� ������� ������
    public float damageInterval = 1.0f; // �������� ����� ���������� �����
    private Transform player; // ������ �� ��������� ������
    private bool isAttacking = false; // ����, ����������� �� ��, ���� �� �����

    private AddRoom room; // ������ �� ��������� AddRoom, ����� ��������� ������� ������

    public Animator animator;
    private bool facingRight = true;

    private void Start()
    {
        // ������� ������ ������ �� �����
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // �������� ������ �� ������������ ������ AddRoom
        room = GetComponentInParent<AddRoom>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Dead");
            Destroy(gameObject, 0.4f); // ���������� �����, ���� ��� �������� ������ ��� ����� ����
            room.enemies.Remove(gameObject); // ������� ����� �� ������ ������� ������ � �������
        }
        else
        {
            MoveTowardsPlayer(); // ��������� � ������, ���� ��� ����
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // ��������� ����������� � ������
            Vector2 direction = (player.position - transform.position).normalized;

            if (direction.x < 0 && facingRight)
            {
                Flip();
            }

            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }

            // ���������� ����� � ���� ����������� � ������ ��������
            transform.Translate(direction * speed * Time.deltaTime);

            // ����������, ����� � ������ ����������� ��������� ���������
            float moveX = 0f;
            float moveY = 0f;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                // �������� �� �����������
                moveX = direction.x > 0f ? 1f : -1f;
            }
            else
            {
                // �������� �� ���������
                moveY = direction.y > 0f ? 1f : -1f;
            }
            // ������������� �������� moveX � moveY ��� ��������
            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveY", moveY);

            // ������ ����������� ������� �����
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
        health -= damage; // ������� ���� �����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ���������� �� ���� � �������
        if (collision.gameObject.CompareTag("Player") && !isAttacking)
        {
            Health playerHealth = player.GetComponent<Health>();
            StartCoroutine(DealDamageOverTime(playerHealth));
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        // ���������, ����������� �� ������� � �������
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���������� �����
            Health playerHealth = player.GetComponent<Health>();
            StopCoroutine(DealDamageOverTime(playerHealth));
            isAttacking = false;
        }
    }

    private IEnumerator DealDamageOverTime(Health health)
    {
        isAttacking = true; // ������������� ���� �����

        while (isAttacking)
        {
            if (health != null)
            {
                health.TakeHit(damage); // ������� ���� ������
            }
            yield return new WaitForSeconds(damageInterval); // ���� �� ��������� �����
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

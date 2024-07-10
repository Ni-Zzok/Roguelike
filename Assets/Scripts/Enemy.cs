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

    private void Start()
    {
        // ������� ������ ������ �� �����
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // �������� ������ �� ������������ ������ AddRoom
        room = GetComponentInParent<AddRoom>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject); // ���������� �����, ���� ��� �������� ������ ��� ����� ����
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

            // ���������� ����� � ���� ����������� � ������ ��������
            transform.Translate(direction * speed * Time.deltaTime);
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
            // �������� ����� �� ��������
            StartCoroutine(DealDamageOverTime(collision.gameObject.GetComponent<Player>()));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // ���������, ����������� �� ������� � �������
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���������� �����
            StopCoroutine(DealDamageOverTime(collision.gameObject.GetComponent<Player>()));
            isAttacking = false;
        }
    }

    private IEnumerator DealDamageOverTime(Player player)
    {
        isAttacking = true; // ������������� ���� �����

        while (isAttacking)
        {
            if (player != null)
            {
                player.TakeDamage(damage); // ������� ���� ������
            }
            yield return new WaitForSeconds(damageInterval); // ���� �� ��������� �����
        }
    }
}

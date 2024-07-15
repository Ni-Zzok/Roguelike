using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage;  // ����, ������� ������� ������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ������ �� �� �� �����
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyShooter"))
        {
            // �������� ��������� Enemy ��� EnemyShooter � �������, � ������� ������
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            EnemyShooter enemyShooter = collision.gameObject.GetComponent<EnemyShooter>();

            // ���� ��������� Enemy ������, ������� ����
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            // ���� ��������� EnemyShooter ������, ������� ����
            else if (enemyShooter != null)
            {
                enemyShooter.TakeDamage(damage);
            }

            // ���������� ������ ����� ���������
            Destroy(gameObject);
        }

        // ���������, ������ �� �� � �����
        else if (collision.gameObject.CompareTag("Block"))
        {
            // ���������� ������ ����� ��������� � �����
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // ���������� ������ ����� ��������� � �����
            Destroy(gameObject);
        }
    }
}

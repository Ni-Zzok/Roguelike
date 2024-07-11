using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage;  // ����, ������� ������� ������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ������ �� �� �� �����
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // �������� ��������� Enemy � �������, � ������� ������
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // ���� ��������� Enemy ������, ������� ����
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
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
        else if (collision.gameObject.CompareTag("Door"))
        {
            // ���������� ������ ����� ��������� � �����
            Destroy(gameObject);
        }
    }
}

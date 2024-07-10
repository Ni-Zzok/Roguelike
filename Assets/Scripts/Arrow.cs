using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // ���� ����� ���������� ��� ������������ ������� � ������ �����������
    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ����������� �� ������ � ��������, ������� �� ����� ������������
        if (collision.gameObject.CompareTag("Target"))
        {
            // ���������� ������
            Destroy(gameObject);
        }
    }
}

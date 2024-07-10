using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePointUp;
    public Transform firePointDown;
    public Transform firePointLeft;
    public Transform firePointRight;
    public GameObject arrowPrefab;
    public float arrowForce = 20f;

    private Transform currentFirePoint;
    private Vector2 currentDirection;

    private void Start()
    {
        // Устанавливаем начальное направление и firePoint
        currentFirePoint = firePointRight;
        currentDirection = Vector2.right;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentFirePoint = firePointUp;
            currentDirection = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentFirePoint = firePointDown;
            currentDirection = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            currentFirePoint = firePointLeft;
            currentDirection = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentFirePoint = firePointRight;
            currentDirection = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(currentFirePoint, currentDirection);
        }
    }

    void Shoot(Transform firePoint, Vector2 direction)
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        // Поворачиваем стрелу в направлении движения
        arrow.transform.up = direction;

        // Применяем силу к стреле
        rb.AddForce(direction * arrowForce, ForceMode2D.Impulse);
    }
}

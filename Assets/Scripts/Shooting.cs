using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Animator animator;

    private void Start()
    {
        // Устанавливаем начальное направление и firePoint
        currentFirePoint = firePointRight;
        currentDirection = Vector2.right;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentFirePoint = firePointUp;
            currentDirection = Vector2.up;
            Shoot(currentFirePoint, currentDirection);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentFirePoint = firePointDown;
            currentDirection = Vector2.down;
            Shoot(currentFirePoint, currentDirection);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentFirePoint = firePointLeft;
            currentDirection = Vector2.left;
            Shoot(currentFirePoint, currentDirection);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentFirePoint = firePointRight;
            currentDirection = Vector2.right;
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

        // Вызываем анимацию стрельбы
        animator.SetTrigger("Shoot");
    }
}

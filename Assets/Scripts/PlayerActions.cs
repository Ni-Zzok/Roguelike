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

    public Animator animator;

    public float moveSpeed = 10f;
    private Rigidbody2D rb; // измените public на private

    private Vector2 moveDirection;

    private bool facingRight = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Устанавливаем начальное направление и firePoint
        currentFirePoint = firePointRight;
        currentDirection = Vector2.right;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ProcessInputs();

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
            if (facingRight) { Flip(); }
            Shoot(currentFirePoint, currentDirection);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentFirePoint = firePointRight;
            currentDirection = Vector2.right;
            if (!facingRight) { Flip(); }
            Shoot(currentFirePoint, currentDirection);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("horizontalMove", Mathf.Abs(moveX));
        animator.SetFloat("verticalMove", moveY);

        if (moveX < 0 && facingRight)
        {
            Flip();
        }

        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Move()
    {
        if (rb != null)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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

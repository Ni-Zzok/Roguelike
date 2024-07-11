using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb; // измените public на private

    private Vector2 moveDirection;

    public Animator animator;

    private bool facingRight = true;

    void Start()
    {
        // Автоматическое присвоение компонента Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        //// Проверка наличия Rigidbody2D
        //if (rb == null)
        //{
        //    Debug.LogError("Rigidbody2D not found on the player!");
        //}
        //else
        //{
        //    Debug.Log("Rigidbody2D successfully assigned.");
        //}

        //// Проверка наличия Collider2D
        //Collider2D col = GetComponent<Collider2D>();
        //if (col == null)
        //{
        //    Debug.LogError("Collider2D not found on the player!");
        //}
        //else
        //{
        //    Debug.Log("Collider2D successfully assigned.");
        //}
    }

    void Update()
    {
        ProcessInputs();


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
}

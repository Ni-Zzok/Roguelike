using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int collisionDamage = 10;
    public string collisionTag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == collisionTag)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.TakeHit(collisionDamage);
        }
    }
}

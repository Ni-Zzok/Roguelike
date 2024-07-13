using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsDamage : MonoBehaviour
{
    public int damage;
    public float damageInterval;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            damageCoroutine = StartCoroutine(DealDamageOverTime(other.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(damageCoroutine);
        }
    }

    private IEnumerator DealDamageOverTime(GameObject target)
    {
        while (true)
        {
            target.GetComponent<Health>().TakeHit(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}

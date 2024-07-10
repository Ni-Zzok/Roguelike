using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Этот метод вызывается при столкновении объекта с другим коллайдером
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулась ли стрела с объектом, который мы хотим обрабатывать
        if (collision.gameObject.CompareTag("Target"))
        {
            // Уничтожаем стрелу
            Destroy(gameObject);
        }
    }
}

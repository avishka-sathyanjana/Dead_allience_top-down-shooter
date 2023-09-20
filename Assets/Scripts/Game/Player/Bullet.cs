using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>()) //collision happening to an enemy
        {
            Destroy(collision.gameObject); //destroy enemy

            Destroy(gameObject); //destroy bullet
        }
    }
}
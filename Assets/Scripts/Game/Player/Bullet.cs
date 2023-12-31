using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;    
    }

    private void Update()
    {
        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>()) //collision happening to an enemy 
        {
            HealthController healthController = collision.GetComponent<HealthController>(); //get the health controller of the enemy
            healthController.TakeDamage(10);//destroy enemy
            Destroy(gameObject); //destroy bullet
        }

        // //if collision happening to a wall
        // if (collision.GetComponent<Wall>())
        // {
        //     Destroy(gameObject); //destroy bullet
        // }
    }

     private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);  //get the screen position of the bullet and convert it to screen cordinate

        if (screenPosition.x < 0 ||
            screenPosition.x > _camera.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > _camera.pixelHeight)
        {
            //then bullet is offscreen
            Destroy(gameObject); //destroy bullet
        }
    }
}
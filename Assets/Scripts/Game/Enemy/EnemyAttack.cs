using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;  

    private void OnCollisionStay2D(Collision2D collision) // call on every frame when enemy i
    {
        if (collision.gameObject.GetComponent<PlayerMovement>()) //get the player movement component from the collision object, then we can knoow
                                                                 //it is the player.
        {
            var healthController = collision.gameObject.GetComponent<HealthController>(); //get the players health controller

            healthController.TakeDamage(_damageAmount); // call the take damage function from the health controller
        }
    }
}

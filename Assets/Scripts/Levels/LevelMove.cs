using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {   
            //print in the colcole that the player has entered the door
            // Debug.Log("Player has entered the door");
            // SceneManager.LoadScene(sceneBuildIndex);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);

            //set is trigger false
            GetComponent<Collider2D>().isTrigger = false;                
        }
    }
}

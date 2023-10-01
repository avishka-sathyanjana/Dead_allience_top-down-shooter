using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    public int sceneBuildIndex;
    public KeyManager km;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {   
            Debug.Log("Player has entered the door");
            //dispaly the key count in console
            Debug.Log("Key Count: " + km.keyCount);
            // SceneManager.LoadScene(sceneBuildIndex);
            if(km.keyCount >= 3 ){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
           
           
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);

            //set is trigger false
            // GetComponent<Collider2D>().isTrigger = false;                
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Level_Manager : MonoBehaviour
{
    public static Level_Manager manager;

    public GameObject deathScreen;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private EnemySpawner enemySpawner; // Reference to the EnemySpawner script

    public static int score;

    public SaveData data;

    public TextMeshProUGUI pointText;

    public AudioClip winningAudio; // Assign the winning audio clip in the Unity Editor
    public AudioClip introAudio; // Assign the losing audio clip in the Unity Editor
    private AudioSource _audioSource;

    private bool hasGameOverBeenCalled = false;

    void Update()
    {
        //Debug.Log("Points: " + score.ToString());
        if (pointText != null)
        {
            pointText.text = "Points: " + score.ToString();
        }

          // Check if the active scene is "GameOver" and GameOver() has not been called yet
        if (SceneManager.GetActiveScene().name == "GameOver" && !hasGameOverBeenCalled)
        {
            // Call GameOver() function from Level_Manager
            GameOver();
            
            // Play Game Over audio if available
            if (winningAudio != null)
            {
                AudioSource.PlayClipAtPoint(winningAudio, Camera.main.transform.position);
            }

            // Set the flag to true to ensure GameOver() is not called again in the same frame
            hasGameOverBeenCalled = true;
        }
        

       
    }

    private void Awake()
    {
        manager = this;
        enemySpawner = FindObjectOfType<EnemySpawner>(); // Find the EnemySpawner script in the scene
        SaveSystem.Initialize();
        data = new SaveData(0);

        _audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

       
    }

    public void GameOver()
    {
        deathScreen.SetActive(true);
        if(pointText != null){
            pointText.text = "";
        }
        
        scoreText.text = "Score: " + score.ToString();

       
        // Stop the enemy spawner when the game is over
        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }

        string loadedData = SaveSystem.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<SaveData>(loadedData);
        }

        if (data.highscore < score)
        {
            data.highscore = score;
        }

        highScoreText.text = "Highscore: " + data.highscore.ToString();

        string saveData = JsonUtility.ToJson(data);
        SaveSystem.Save("save", saveData);

    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("afterAnimations"); //reload the current scene
        //set score to 0
        score = 0;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        //show score in console
        // Debug.Log("Score: " + score.ToString());
    }
}

[System.Serializable]
public class SaveData
{
    public int highscore;
    public SaveData(int _hs)
    {
        highscore = _hs;
    }
}

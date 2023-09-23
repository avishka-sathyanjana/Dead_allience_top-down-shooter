using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Level_Manager : MonoBehaviour
{
    public static Level_Manager manager;

    public GameObject deathScreen;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private EnemySpawner enemySpawner; // Reference to the EnemySpawner script

    public int score;

    public SaveData data;

    private void Awake() {
        manager = this;
        enemySpawner = FindObjectOfType<EnemySpawner>(); // Find the EnemySpawner script in the scene
        SaveSystem.Initialize();
        data = new SaveData(0);
    }

    public void GameOver(){
        deathScreen.SetActive(true);
        scoreText.text = "Score: " + score.ToString();

        // Stop the enemy spawner when the game is over
        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }

        string loadedData = SaveSystem.Load("save");
        if(loadedData != null){
            data = JsonUtility.FromJson<SaveData>(loadedData);
        }

        if(data.highscore < score){
            data.highscore = score;
        }

        highScoreText.text = "Highscore: " + data.highscore.ToString();

        string saveData = JsonUtility.ToJson(data);
        SaveSystem.Save("save", saveData);
        
    }

    public void ReplayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reload the current scene
    }

    public void IncreaseScore(int amount){
        score += amount;
        //show score in console
        Debug.Log("Score: " + score.ToString());
    }
}

[System.Serializable]
public class SaveData
{
    public int highscore;
    public SaveData(int _hs){
        highscore = _hs;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _minimumSpawnTime;

    [SerializeField]
    private float _maximumSpawnTime;

    private float _timeUntilSpawn;
    private bool isSpawning = true;

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        if (isSpawning)
        {
            _timeUntilSpawn -= Time.deltaTime; //updating time to make the next spawn

            if (_timeUntilSpawn <= 0)
            {
                Instantiate(_enemyPrefab, transform.position, Quaternion.identity); //create enemy
                SetTimeUntilSpawn();
            }
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime); //a random value between minim and mx
    }

    public void StopSpawning()  //to stop spawning when the game is over
    {
        isSpawning = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDestroyContrroller : MonoBehaviour
{
    public void DestroyEnemy(float delay)
    {
        Destroy(gameObject, delay);
        Level_Manager.manager.IncreaseScore(1);
    }
}

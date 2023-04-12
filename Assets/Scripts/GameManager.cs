using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] List<StageSO> stageList;

    EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        StartCoroutine(HandleStages());
    }


    public IEnumerator HandleStages()
    {
        foreach(StageSO stage in stageList) {
                NextState(stage);
                do { yield return null; }
                while (enemySpawner.spawningEnemies || GameObject.FindGameObjectsWithTag("Enemy").Length > 0);
        }
    
        Debug.Log("baller");
    }

    void NextState(StageSO stage)
    {

        enemySpawner.waveList = stage.GetWaves();
        StartCoroutine(enemySpawner.SpawnWaves());

    }
}

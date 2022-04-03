using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public int currentRoundNR = -1;
    public bool complete;
    public List<RoundInfo> roundList;
    public RoundInfo currentRound;

    public int roundEnemyCount;
    public int enemiesLeft;
    public int roundKills;
    public int spawnedCount;

    public static RoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }


    public void StartNextRound()
    {
        currentRoundNR++;
        complete = false;
        currentRound = roundList[currentRoundNR];
        roundKills = 0;
        spawnedCount = 0;
        enemiesLeft = currentRound.enemyCount;
        roundEnemyCount = currentRound.enemyCount;
        UIManager.Instance.UpdateUI();
        InvokeRepeating("TrySpawn", 0f, 1f);
    }

    private void TrySpawn()
    {
        EnemySpawner.Instance.SpawnEnemy(currentRound.enemyIDs[spawnedCount]);
        spawnedCount++;
        if(spawnedCount >= roundEnemyCount)
        {
            complete = true;
            CancelInvoke();
        }
    }

    public void EnemyDespawned()
    {
        enemiesLeft--;
        UIManager.Instance.UpdateUI();
    }
}

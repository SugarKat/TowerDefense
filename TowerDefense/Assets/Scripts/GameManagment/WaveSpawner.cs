using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    public Transform startPoint;

    public WavesInfo wavesInfo;

    private int waveN = 0;
    private bool waveSpawning = false;
    public List<GameObject> spawnedEntities = new List<GameObject>(); 

    public int AliveEnemies { get { return spawnedEntities.Count; } }
    public int GetRound { get { return waveN; } }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
    void Start()
    {
        spawnedEntities = new List<GameObject>();
    }
    public void StartNextWave()
    {
        if (waveSpawning)
        {
            return;
        }
        waveSpawning = true;
        StartCoroutine("StartWaveSpawn");

    }
    private IEnumerator StartWaveSpawn()
    {
        int groups = wavesInfo.waves[waveN].groups.Length;
        for (int i = 0; i < groups; i++)
        {
            int groupSize = wavesInfo.waves[waveN].groups[i].groupSize;
            int groupEnemyID = 0;
            for (int j = 0; j < groupSize; j++)
            {
                if (wavesInfo.waves[waveN].groups[i].enemies.Length <= 0)
                {
                    Debug.LogError("No enemies defined");
                    break;
                }
                spawnedEntities.Add(Instantiate(wavesInfo.waves[waveN].groups[i].enemies[groupEnemyID], startPoint.position, startPoint.rotation));
                UIManager.Instance.UpdateUI();
                groupEnemyID++;
                if (groupEnemyID >= wavesInfo.waves[waveN].groups[i].enemies.Length)
                {
                    groupEnemyID = 0;
                }
                yield return new WaitForSeconds(wavesInfo.waves[waveN].groups[i].spawnIntervals);
            }
            yield return new WaitForSeconds(wavesInfo.waves[waveN].groups[i].timeTillNextGroup);
        }

        waveSpawning = false;
        waveN++;
    }
    public void RemoveEnemy(GameObject obj)
    {
        spawnedEntities.Remove(obj);
        UIManager.Instance.UpdateUI();
    }
}

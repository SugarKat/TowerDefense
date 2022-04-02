using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform startPoint;

    public WavesInfo wavesInfo;

    private int waveN = 0;
    private bool waveSpawning = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
                    Debug.Log(j);
                if (wavesInfo.waves[waveN].groups[i].enemies.Length <= 0)
                {
                    Debug.LogError("No enemies defined");
                    break;
                }
                Instantiate(wavesInfo.waves[waveN].groups[i].enemies[groupEnemyID], startPoint.position, startPoint.rotation);
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
}

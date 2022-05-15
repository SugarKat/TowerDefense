using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    public Transform hostPoint;
    public Transform clientPoint;

    public WavesInfo wavesInfo;

    public Waypoints hostWaypoints;
    public Waypoints clientWaypoints;

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
        if (NetworkManager.instance != null)
        {
            NetworkManager.instance.NextWaveSignal(waveN);
        }
        waveSpawning = true;
        StartCoroutine("StartWaveSpawn");
    }
    public void StartNextWaveNT(int waveID)
    {
        if (waveSpawning)
        {
            return;
        }
        waveSpawning = true;
        StartCoroutine(StartWaveSpawn(waveID));
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
                spawnedEntities.Add(Instantiate(wavesInfo.waves[waveN].groups[i].enemies[groupEnemyID], hostPoint.position, hostPoint.rotation));

                //UIManager.Instance.UpdateUI();

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
    private IEnumerator StartWaveSpawn(int waveID)
    {
        bool host = NetworkManager.instance.host;
        int groups = wavesInfo.waves[waveID].groups.Length;
        for (int i = 0; i < groups; i++)
        {
            int groupSize = wavesInfo.waves[waveID].groups[i].groupSize;
            int groupEnemyID = 0;
            for (int j = 0; j < groupSize; j++)
            {
                if (wavesInfo.waves[waveID].groups[i].enemies.Length <= 0)
                {
                    Debug.LogError("No enemies defined");
                    break;
                }
                if (host)
                {
                    spawnedEntities.Add(Instantiate(wavesInfo.waves[waveID].groups[i].enemies[groupEnemyID], hostPoint.position, hostPoint.rotation));
                }
                else
                {
                    spawnedEntities.Add(Instantiate(wavesInfo.waves[waveID].groups[i].enemies[groupEnemyID], clientPoint.position, clientPoint.rotation));
                }

                //UIManager.Instance.UpdateUI();

                groupEnemyID++;
                if (groupEnemyID >= wavesInfo.waves[waveID].groups[i].enemies.Length)
                {
                    groupEnemyID = 0;
                }
                yield return new WaitForSeconds(wavesInfo.waves[waveID].groups[i].spawnIntervals);
            }
            yield return new WaitForSeconds(wavesInfo.waves[waveID].groups[i].timeTillNextGroup);
        }

        waveSpawning = false;
    }
    public void RemoveEnemy(GameObject obj)
    {
        spawnedEntities.Remove(obj);

        //UIManager.Instance.UpdateUI();

    }
    public Transform GetNextWaypoint(int currentWaypoint)
    {
        if (NetworkManager.instance.host)
        {
            return hostWaypoints.GetNextWaypoint(currentWaypoint);
        }
        else
        {
            return clientWaypoints.GetNextWaypoint(currentWaypoint);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemyList;
    public Waypoints waypoints;
    public List<GameObject> spawnedEntities;


    public static EnemySpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(int enemyID)
    {
        enemyList[enemyID].GetComponent<EnemyMovement>().waypoints = waypoints;
        GameObject spawned = GameObject.Instantiate(enemyList[enemyID], waypoints.waypoints[0].transform);
        spawnedEntities.Add(spawned);
    }

    public void RemoveEnemy(GameObject obj)
    {
        spawnedEntities.Remove(obj);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public bool showGizmos = false;

    public static Waypoints instance;

    public Transform[] waypoints;

    [SerializeField] private GameObject enemySpawn;

    public GameObject enemy;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }


    private void OnDrawGizmos()
    {
        if(!showGizmos)
        {
            return;
        }
        foreach(Transform t in transform)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(t.position, 0.15f);
        }

        for(int i=0; i< transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
        }
    }

    public Transform GetNextWaypoint(int currentWaypoint)
    {
        if(currentWaypoint >= waypoints.Length)
        {
            return null;
        }
        return waypoints[currentWaypoint];
    }
    
}

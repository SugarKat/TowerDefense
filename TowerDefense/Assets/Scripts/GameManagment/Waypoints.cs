using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private GameObject enemySpawn;

    private void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            Spawn();
        }
    }


    private void OnDrawGizmos()
    {
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

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);
        }

        if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        else
        {
            return null;
        }
    }
    public void Spawn()
    {
        GameObject newObject = Instantiate(enemySpawn) as GameObject;
        newObject.GetComponent<EnemyMovement>().waypoints = this;
        newObject.GetComponent<EnemyMovement>().moveSpeed = 2f;
    }
}

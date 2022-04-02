using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] public Waypoints waypoints;

    [SerializeField] public float moveSpeed = 1f;

    [SerializeField] public float radius = 0.4f;

    private int currentWaypoint = 0;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // Get next waypoint
        target = waypoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
            if (Vector3.Distance(transform.position, target.position) < radius)
            {
                target = waypoints.GetNextWaypoint(currentWaypoint);

                currentWaypoint++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}

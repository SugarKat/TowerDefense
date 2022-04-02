using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Waypoints waypoints;

    [SerializeField] public float moveSpeed = 1f;

    [SerializeField] public float radius = 0.4f;

    private int nextWaypoint = 0;
    private Transform target;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        waypoints = Waypoints.instance;        
        target = waypoints.GetNextWaypoint(nextWaypoint);
        nextWaypoint++;
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
                target = waypoints.GetNextWaypoint(nextWaypoint);

                nextWaypoint++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}

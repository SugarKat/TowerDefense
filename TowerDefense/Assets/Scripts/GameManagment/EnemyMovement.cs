using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] public Waypoints waypoints;

    [SerializeField] public float moveSpeed = 1f;

    [SerializeField] public float radius = 0.4f;

    [SerializeField] public int money = 20;

    public float health = 100;
    private int currentWaypoint = 0;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // Get next waypoint
        target = Waypoints.instance.GetNextWaypoint(currentWaypoint++);
        transform.LookAt(target);
    }

    public void TakeDamage(float dmg)
    {
        health = health - dmg;
        if(health < 0)
        {
            Kill();
        }
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
                target = Waypoints.instance.GetNextWaypoint(currentWaypoint);

                currentWaypoint++;
            }
        }
        else
        {
            Despawn();
        }
    }

    void Kill()
    {
        WaveSpawner.instance.RemoveEnemy(this.gameObject);
        PlayerStats.Instance.AddMoney(money);
        //RoundManager.Instance.EnemyDespawned();
        Destroy(this.gameObject);
    }

    void Despawn()
    {
        WaveSpawner.instance.RemoveEnemy(this.gameObject);
        PlayerStats.Instance.TakeDamage(1);
        //RoundManager.Instance.EnemyDespawned();
        Destroy(this.gameObject);
    }

}

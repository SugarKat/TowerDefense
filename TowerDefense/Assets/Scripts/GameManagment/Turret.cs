using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject target;
    [Header("Model Info")]
    public GameObject model;
    public GameObject bulletPrefab;
    public Transform rotationPoint;
    public Transform firePoint;

    [Header("Attributes")]
    public float fireRate = 1f;
    private float delay = 0f;
    public float range = 20f;

    // Start is called before the first frame update
    void Start()
    {
        delay = 0;
        model = this.gameObject;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }


    void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach(GameObject enemy in WaveSpawner.instance.spawnedEntities)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if(closestEnemy != null && shortestDistance <= range)
        {
            target = closestEnemy;
        }
        else
        {
            target = null;
        }
    }


    void ShootAtTarget()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet _bullet = bullet.GetComponent<Bullet>();
        if(_bullet != null)
        {
            _bullet.Seek(target);
        }
        Vector3.MoveTowards(bullet.transform.localPosition, target.transform.localPosition, 1);
    }


    void Update()
    {
        if(target == null)
        {
            return;
        }
        Vector3 direction = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotationPoint.rotation, lookRotation, Time.deltaTime*10).eulerAngles;
        rotationPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        if(delay <= 0f)
        {
            ShootAtTarget();
            delay = 1f / fireRate;
        }

        delay -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

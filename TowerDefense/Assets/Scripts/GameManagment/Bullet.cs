using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    public float speed = 10f;
    public float damage = 25f;
    public GameObject impactEffect;

    public void Seek(GameObject targetGO)
    {
        target = targetGO;
    }


    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.transform.position - transform.position;
        float distance = speed * Time.deltaTime;

        if(direction.magnitude <= distance)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distance, Space.World);

    }

    private void HitTarget()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        target.GetComponent<EnemyMovement>().TakeDamage(damage);
        Destroy(effect, 2f);
        Destroy(gameObject);
        return;
    }
}

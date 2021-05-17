using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float bulletSpeed = 50f;

    public int bulletDamage = 1;

    public GameObject bulletEffect;

    public float explosionRad = 0f;

    public void FindTarget(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceFrame = bulletSpeed * Time.deltaTime;

        if(dir.magnitude <= distanceFrame)
        {
            BulletHit();
            return;
        }

        transform.Translate(dir.normalized * distanceFrame, Space.World);

        // 타겟을 향해 회전
        transform.LookAt(target);      
    }

    void BulletHit()
    {
        GameObject newEffect = (GameObject)Instantiate(bulletEffect, transform.position, transform.rotation);
        Destroy(newEffect, 5f);

        if(explosionRad > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
       
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRad);

        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        EnemyFSM e = enemy.GetComponent<EnemyFSM>();

        if(e != null)
        {
            e.GetDamage(bulletDamage);
        }
    }
}

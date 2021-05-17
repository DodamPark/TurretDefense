using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 포탑은 사거리 내의 가장 가까운 타겟을 찾고 싶다.
// 2. 타겟을 찾으면 타겟을 향해 포탑을 회전시키고 싶다.
// 3. 공격 대기시간 동안 잠시 기다린다.
// 4. 대기시간이 끝나면 타겟을 향해 총알을 쏘고 싶다.
public class Turret : MonoBehaviour
{
    // 타겟 트랜스폼
    private Transform target;
    private EnemyFSM targetE;
    

    [Header("포탑 속성")]

    // 공격 속도
    public float attackSpeed = 1f;

    // 공격 대기시간
    private float attackDelay = 0f;

    // 포탑 사거리
    public float range = 10f;

    [Header("레이저 포탑")]
    public bool isLazer = false;
    public float dps = 1;
    public float slowPer = 0.25f;

    public LineRenderer lineRenderer;
    public ParticleSystem ps;
    public Light lazerLight;

    [Header("유니티 설정")]

    // 에너미 태그
    public string enemyTag = "Enemy";

    // 회전축 변수
    public Transform rotateTurret;

    // 회전 속도
    public float rotSpeed = 5f;

    public GameObject bullet;

    public Transform firePos;

    // Start is called before the first frame update
    void Start()
    {
        // 타겟 업데이트 함수를 0.5초마다 반복 실행한다.
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (isLazer)
            {
                if (lineRenderer.enabled)
                {
                    LazerOff();
                }
            }
            return;
        }

        LockOn();

        if (isLazer)
        {
            if(targetE != null)
            {
                if (targetE.isDead == false)
                    Lazer();
                else
                {
                    LazerOff();
                }
            }
        }
        else
        {
            if(targetE != null)
            {
                if (attackDelay <= 0f)
                {
                    if (targetE.isDead == true)
                        return;
                    else if (targetE.isDead == false)
                    {
                        Fire();
                        attackDelay = 1f / attackSpeed;
                    }
                }
                attackDelay -= Time.deltaTime;
            }
        }
    }

    void LockOn()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion rotateToTarget = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotateTurret.rotation, rotateToTarget, rotSpeed * Time.deltaTime).eulerAngles;
        rotateTurret.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Lazer()
    {
        targetE.GetDamage(dps * Time.deltaTime);
        targetE.SlowEnemy(slowPer);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            ps.Play();
            lazerLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePos.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePos.position - target.position;

        ps.transform.position = target.position + dir.normalized;
        
        ps.transform.rotation = Quaternion.LookRotation(dir);
    }

    void LazerOff()
    {
        lineRenderer.enabled = false;
        ps.Stop();
        lazerLight.enabled = false;
    }

    void UpdateTarget()
    {
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemyArray)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if(closestEnemy != null && shortestDistance <= range)
        {
            target = closestEnemy.transform;
            targetE = closestEnemy.GetComponent<EnemyFSM>();
        }
        else
        {
            target = null;
        }
    }

    void Fire()
    {
        GameObject bulletFactory = (GameObject)Instantiate(bullet, firePos.position, firePos.rotation);
        Bullet newBullet = bulletFactory.GetComponent<Bullet>();

        if (newBullet != null)
            newBullet.FindTarget(target);
    }

    // 기즈모를 이용하여 포탑의 사거리를 표시한다.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

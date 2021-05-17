using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 에너미의 방향을 정하고 계속 이동시키고 싶다.
// 2. 첫번째 목적지에 도착하면
// 3. 목적지가 마지막 목적지인지 확인해서 마지막이면 에너미를 제거한다.
// 4. 그 외에는 다음 목적지를 찾는다
public class EnemyFSM : MonoBehaviour
{
    // 에너미 이동속도
    private float moveSpeed;

    // 에너미 이동속도 디폴트
    public float originSpeed = 10f;

    // 에너미 체력
    public float enemyHealth = 10;

    // 에너미 처치 보상
    public int reward = 1;

    // 에너미 목적지 타겟
    private Transform target;

    // 웨이포인트 인덱스
    private int wayPointIndex = 0;

    // 에너미 죽음 이펙트
    public GameObject dieEffect;

    public bool isDead = false;

    // 웨이브스폰 컴포넌트
    WaveSpawn waveSpawn;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = originSpeed;

        anim = transform.GetComponentInChildren<Animator>();

        // 웨이포인트의 첫번째 배열을 목적지로 설정한다.
        target = WayPoint.wayPointArray[0];

        // 웨이브 스폰 컴포넌트를 받아온다.
        waveSpawn = GameObject.Find("GameManager").GetComponent<WaveSpawn>();

        // 웨이브가 지날수록 에너미의 체력이 강화된다.
        enemyHealth = 10 + 2 * (waveSpawn.waveNum - 1);
    }

    // Update is called once per frame
    void Update()
    {
        //1.에너미의 방향을 정하고 계속 이동시키고 싶다.  
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);

        transform.forward = dir;    

        // 2. 첫번째 목적지에 도착하면 
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextPos();
        }

        moveSpeed = originSpeed;
    }

    void GetNextPos()
    {
        // 3. 목적지가 마지막 목적지인지 확인해서 마지막이면 에너미를 제거한다.
        if (wayPointIndex >= WayPoint.wayPointArray.Length - 1)
        {
            EnemyEnd();
            return;
        }
        // 4. 그 외에는 다음 목적지를 찾는다
        else
        {
            wayPointIndex++;
            target = WayPoint.wayPointArray[wayPointIndex];
        } 
    }

    public void GetDamage(float damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void SlowEnemy(float percent)
    {
        moveSpeed = originSpeed * (1f - percent);
    }

    void Die()
    {
        isDead = true;
        originSpeed = 0;
        Player.goldAmount += reward;
        anim.SetTrigger("MoveToDie");
        GameObject e = (GameObject)Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(e, 1f);

        Destroy(gameObject, 1.5f);
        
    }


    void EnemyEnd()
    {
        Player.playerLife--;
        Destroy(gameObject);
    }
}

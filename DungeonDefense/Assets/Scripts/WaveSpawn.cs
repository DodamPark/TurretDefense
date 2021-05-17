using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 1. 기준 시간이 되었으니까
// 2. 에너미 웨이브를 발생시키고
// 3. 표기 텍스트를 카운트 다운으로 맞추고 싶다.
// 4. 에너미를 생성했으면 다음 웨이브는 에너미를 증가시키고 싶다.
public class WaveSpawn : MonoBehaviour
{
    // 에너미 트랜스폼
    public Transform enemyPrefab;

    // 에너미 생성 위치
    public Transform spawnPos;

    // 생성 재사용 대기시간
    public float waveCoolTime = 5.5f;

    // 카운트다운 변수
    private float countDown = 2.5f;

    // 웨이브 카운트 다운 표시 텍스트
    public Text waveCountText;

    // 에너미 웨이브 번호
    public int waveNum = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 1. 기준 시간이 되었으니까 
        if (countDown <= 0f)
        {
            // 2. 에너미 웨이브를 발생시키고
            StartCoroutine(SpawnEnemy());
            countDown = waveCoolTime;
        }

        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        // 3. 표기 텍스트를 카운트 다운으로 맞추고 싶다.
        waveCountText.text = string.Format("{0:00.00}", countDown);
    }

    IEnumerator SpawnEnemy()
    {
        Player.waveRounds++;
        // 4. 에너미를 생성했으면 다음 웨이브는 에너미를 증가시키고 싶다.
        for (int i = 0; i < waveNum; i++)
        {
            Instantiate(enemyPrefab, spawnPos.position, spawnPos.rotation);
            yield return new WaitForSeconds(0.5f);
        }
        waveNum++;      
    }
}

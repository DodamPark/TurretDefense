using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 1. 에너미의 목적지를 결정하는 웨이 포인트를 배열로 관리하고 싶다.
public class WayPoint : MonoBehaviour
{
    // 웨이 포인트 배열 전역변수
    public static Transform[] wayPointArray;

    void Awake()
    {
        // 웨이 포인트 배열에 자식 오브젝트의 트랜스폼을 받아서 저장하고 싶다.
        wayPointArray = new Transform[transform.childCount];

        for(int i = 0; i < wayPointArray.Length; i++)
        {
            wayPointArray[i] = transform.GetChild(i);
        }
    }
}

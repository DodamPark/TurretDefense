using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 웨이 포인트를 유니티 씬 창에서 선명하게 표시하고 싶다.
public class WayPointGizmo : MonoBehaviour
{
    // 웨이포인트를 붉은색 와이어 스피어로 표시한다.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f);
    }
}

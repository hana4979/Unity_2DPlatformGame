using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public Transform exitPotal; // 탈출 포탈

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") // 플레이어와 충돌하면
        {
            other.transform.position = exitPotal.position + new Vector3(1.5f, 0, 0); // 플레이어의 위치를 나가는 포탈 위치로 변경
            GameManager.instance.inBossStage = true; // 보스 스테이지 진입
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject pfPumkin;

    private float spawnTime = 6.0f;
    private float currentTime;

    void Update()
    {
        // 보스 스테이지 진입 하지 않았거나
        // 게임 오버 상태라면 스폰하지 않음
        if (!GameManager.instance.inBossStage || GameManager.instance.isGameover || GameManager.instance.isClear)
        {
            return;
        }

        currentTime += Time.deltaTime;

        if(currentTime >= spawnTime)
        {
            Instantiate(pfPumkin, transform.position, transform.rotation);
            currentTime = 0;
        }
    }
}

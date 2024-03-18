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
        // ���� �������� ���� ���� �ʾҰų�
        // ���� ���� ���¶�� �������� ����
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

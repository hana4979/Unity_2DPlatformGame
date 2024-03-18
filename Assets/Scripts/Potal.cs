using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public Transform exitPotal; // Ż�� ��Ż

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") // �÷��̾�� �浹�ϸ�
        {
            other.transform.position = exitPotal.position + new Vector3(1.5f, 0, 0); // �÷��̾��� ��ġ�� ������ ��Ż ��ġ�� ����
            GameManager.instance.inBossStage = true; // ���� �������� ����
        }
    }
}

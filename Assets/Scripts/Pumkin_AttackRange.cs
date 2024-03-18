using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumkin_AttackRange : MonoBehaviour
{
    Animator pumkinAni;

    void Start()
    {
        pumkinAni = GetComponentInParent<Animator>();
    }

    // target�� �浹 �����ȿ� ������ ����
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player") // Player�� �浹 �����ȿ� ���Դٸ�
        {
            pumkinAni.SetBool("isAttack", true); // Attack �ִϸ��̼� ����
        }
        else
        {
            pumkinAni.SetBool("isAttack", false);
        }
    }

    // �浹 ������ ����ٸ� Idle ���·� ����
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
            pumkinAni.SetBool("isAttack", false);
    }
}

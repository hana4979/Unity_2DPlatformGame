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

    // target이 충돌 범위안에 들어왔을 동안
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player") // Player가 충돌 범위안에 들어왔다면
        {
            pumkinAni.SetBool("isAttack", true); // Attack 애니메이션 실행
        }
        else
        {
            pumkinAni.SetBool("isAttack", false);
        }
    }

    // 충돌 범위를 벗어났다면 Idle 상태로 진입
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
            pumkinAni.SetBool("isAttack", false);
    }
}

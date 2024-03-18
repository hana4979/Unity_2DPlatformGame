using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid; // 리지드바디 컴포넌트
    SpriteRenderer rend; // 스프라이트 렌더러 컴포넌트

    private PumkinData pumkinData;

    public int nextMove = -1;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        pumkinData = GetComponent<PumkinData>();

        // Invoke : 주어진 시간이 지난 뒤, 지정된 함수를 실행하는 함수
        Invoke("Think", 5f);
    }

    void Update()
    {
        if (!(pumkinData.isLive) || GameManager.instance.isGameover || GameManager.instance.isClear)
        {
            rigid.velocity = Vector2.zero; // 움직임 멈춤
            return;
        }
    }

    void FixedUpdate()
    {
        // Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // Platform Check
        Vector2 frontVect = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVect, Vector3.down, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVect, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke(); // 현재 작동중인 Invoke 중지
            Invoke("Think", 2);
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2); // -1부터 2미만까지 포함해서 랜덤값 생성
        
        if(nextMove > 0)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;
        }

        float nextThinkTime = Random.Range(2.0f, 5.0f);
        Invoke("Think", nextThinkTime);
    }
}

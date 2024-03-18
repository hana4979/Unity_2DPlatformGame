using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid; // ������ٵ� ������Ʈ
    SpriteRenderer rend; // ��������Ʈ ������ ������Ʈ

    private PumkinData pumkinData;

    public int nextMove = -1;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        pumkinData = GetComponent<PumkinData>();

        // Invoke : �־��� �ð��� ���� ��, ������ �Լ��� �����ϴ� �Լ�
        Invoke("Think", 5f);
    }

    void Update()
    {
        if (!(pumkinData.isLive) || GameManager.instance.isGameover || GameManager.instance.isClear)
        {
            rigid.velocity = Vector2.zero; // ������ ����
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
            CancelInvoke(); // ���� �۵����� Invoke ����
            Invoke("Think", 2);
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2); // -1���� 2�̸����� �����ؼ� ������ ����
        
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

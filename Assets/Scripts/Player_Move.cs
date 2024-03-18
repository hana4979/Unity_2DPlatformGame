using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    private float moveLR;
    private float speed = 4.0f;
    private int jumpCount = 0; // 누적 점프 횟수

    private float poisonDamage = 5; // 독 데미지
    private float currentTime = 0; // 현재시간
    private float poisonDamageTime = 1f; // 독데미지 입는 시간

    Animator playerAni;
    Rigidbody2D playerRigid;
    SpriteRenderer playerRend;
    private AudioSource audioSource;

    void Start()
    {
        playerAni = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerRend = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.instance.isGameover || GameManager.instance.isClear)
        {
            return;
        }

        //.. 캐릭터 좌우 움직임
        // 키보드 좌우 버튼 (방향키/wasd), A : -1 / D : 1
        moveLR = Input.GetAxisRaw("Horizontal");
        // transform.right : 로컬축으로 변경
        transform.Translate(transform.right * moveLR * Time.deltaTime * speed); // 좌우 움직임

        //.. 캐릭터 점프 움직임
        if (Input.GetKeyDown(KeyCode.W) && jumpCount < 2)
        {
            jumpCount++; // 점프 횟수 증가
            playerRigid.velocity = Vector2.zero; // 점프 직전 속도를 순간적으로 제로로 변경
            playerRigid.AddForce(new Vector2(0, 5.0f), ForceMode2D.Impulse);
            playerAni.SetBool("isJump", true); // Jump 동작 실행
        }
        // w버튼에서 손을 떼는 순간 && 속도의 y 값이 양수라면 (위로 상승 중)
        else if (Input.GetKeyUp(KeyCode.W) && playerRigid.velocity.y > 0)
        {
            playerRigid.velocity = playerRigid.velocity * 0.5f; // 현재 속도를 절반으로 변경
        }
        else
        {
            playerAni.SetBool("isJump", false);

            if (playerRigid.velocity.y != 0)
            {
                playerAni.SetBool("isJump", true);
            }
        }

        //.. 캐릭터 달리기 애니메이션
        if (moveLR == -1 || moveLR == 1) // 왼쪽 or 오른쪽 이동
        {
            playerAni.SetBool("isRun", true); // Run 동작 실행

            if (playerRigid.velocity.y != 0) // 점프 상태라면
            {
                playerAni.SetBool("isJump", true); // Jump 동작 실행
                playerAni.SetBool("isRun", false);
            }
        }
        else
        {
            playerAni.SetBool("isRun", false);
        }

        //.. 캐릭터 좌우 반전
        if (moveLR == -1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (moveLR == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //&& collision.contacts[0].normal.y > 0.7f
        if (collision.gameObject.tag == "Ground") // 땅에 닿았다면
        {
            jumpCount = 0; // 점프 횟수 초기화
        }

        if (collision.gameObject.tag == "Enemy") // 적과 충돌 시
        {
            onDamage(GameManager.instance.pumkinDamage);

            if (GameManager.instance.playerHealth <= 0) // 플레이어 체력이 다 닳으면
            {
                playerDead();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Key") // 사탕(열쇠)와 충돌 시
        {
            GameManager.instance.getCandy++;
            audioSource.Play();
            Heal(); // 체력 증가
            GameManager.instance.AddScore(5); // 점수 추가
            Destroy(other.gameObject); // 먹은 사탕 파괴

            if(GameManager.instance.getCandy == 5)
            {
                GameManager.instance.hasCandy = true;
                GameManager.instance.ActivePotal(); // 포탈 활성화
            }
        }

        if(other.tag == "Bone") // 뼈와 충돌 시
        {
            onDamage(GameManager.instance.boneDamage);

            if (GameManager.instance.playerHealth <= 0) // 플레이어 체력이 다 닳으면
            {
                playerDead();
            }
        }

        if(other.tag == "DeadZone") // 데드존과 충돌 시
        {
            playerDead();
        }

        if (other.tag == "Poison") // 독과 충돌 시
        {
            onDamage(poisonDamage);

            if (GameManager.instance.playerHealth <= 0) // 플레이어 체력이 다 닳으면
            {
                playerDead();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Poison") // 독과 충돌하고 있을 경우
        {
            currentTime += Time.deltaTime;
            if(currentTime > poisonDamageTime) // 1초에 한 번씩 독데미지
            {
                onDamage(poisonDamage);
                currentTime = 0;
            }

            if (GameManager.instance.playerHealth <= 0) // 플레이어 체력이 다 닳으면
            {
                playerDead();
            }
        }
    }

    // 플레이어 사망 시
    private void playerDead()
    {
        GameManager.instance.EndGame();
    }

    // 데미지를 입을 시 이펙트
    IEnumerator DamageEffect()
    {
        playerRend.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        playerRend.material.color = Color.white;
        yield return new WaitForSeconds(0.2f);
    }

    // 데미지를 입으면 변화하는 체력과 UI, Effect 관리
    private void onDamage(float damage)
    {
        GameManager.instance.playerHealth -= damage; // damage만큼 체력 감소
        GameManager.instance.uiManager.PlayerHp(); // HP UI 변경
        StartCoroutine(DamageEffect()); // 충돌 후 이펙트 실행
    }

    // 사탕(키) 먹을 경우 체력 회복
    private void Heal()
    {
        if(GameManager.instance.playerHealth >= 90) // 체력이 90 이상이라면
        {
            GameManager.instance.playerHealth = 100; // 체력 100으로 고정
            GameManager.instance.uiManager.PlayerHp(); // HP UI 변경
        }
        else
        {
            GameManager.instance.playerHealth += 10; // 체력 10 증가
            GameManager.instance.uiManager.PlayerHp(); // HP UI 변경
        }
    }
}

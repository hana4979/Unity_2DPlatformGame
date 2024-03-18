using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    private float currentTime = 0f;
    private float offsetTime = 3f;

    public GameObject pfBone; // 뼈 프리펩
    public GameObject throwSpawn; // 던지는 위치

    private bool isLive = true; // 생존 여부
    private float bossHealth; // 보스 현재 체력
    private float bossMaxHealth = 30; // 보스 최대 체력

    private AudioSource audioSource;
    public Animator bossAni;
    public UIManager uiManager;

    void Awake()
    {
        uiManager = uiManager.GetComponent<UIManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        isLive = true;
        bossHealth = bossMaxHealth;
    }

    void Update()
    {
        // 보스 스테이지 진입 하지 않았으면 실행하지 않음
        if (!GameManager.instance.inBossStage)
        {
            return;
        }

        if (!isLive)
        {
            bossAni.SetBool("isDead", true); // 죽음 애니메이션 실행
            audioSource.Play();
            Destroy(gameObject, 2f); // 2초 뒤 파괴
            uiManager.SetActiveGameclearUI(true); // 게임클리어 UI 활성화
        }

        currentTime += Time.deltaTime;
        if(currentTime > offsetTime)
        {
            StartCoroutine(BossAttack()); // 공격 코루틴 실행
            currentTime = 0;
        }
    }

    IEnumerator BossAttack()
    {
        bossAni.SetBool("isAttack", true);
        yield return new WaitForSeconds(1.0f);
        bossAni.SetBool("isAttack", false);
        yield return null;
    }

    void MakeBone()
    {
        Instantiate(pfBone, throwSpawn.transform.position, throwSpawn.transform.rotation); // 뼈 생성
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLive && other.tag == "Bullet") // 총알과 충돌 시
        {
            TakeDamage(); // 데미지 받았을 시 체력바
            
            if (bossHealth <= 0) // 보스 체력이 0 밑으로 내려갔다면
            {
                GameManager.instance.AddScore(100); // 점수 추가
                isLive = false;
                GameManager.instance.isClear = true;
            }
        }
    }

    public void TakeDamage()
    {
        //pumkinHp.ActiveHealthBar(true); // 체력바 활성화
        bossHealth--; // 체력 1 감소
        //pumkinHp.UpdateHealthBar(pumkinHealth, pumkinMaxHealth); // 체력바 업데이트
    }
}

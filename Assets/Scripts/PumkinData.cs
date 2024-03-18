using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumkinData : MonoBehaviour
{
    public GameObject pfPKExplosion;

    private Animator pumkinAni;
    private AudioSource audioSource;

    public bool isLive; // 생존 여부
    private float pumkinHealth; // 호박 체력
    private float pumkinMaxHealth; // 호박 최대 체력
    private PumkinHp pumkinHp;
    private int pumkinScore; // 호박 처치 점수

    enum PumkinType
    {
        Red, // 0
        Blue // 1
    };

    [SerializeField] private PumkinType type;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        pumkinMaxHealth = (type == PumkinType.Red) ? 5 : 7;
        pumkinScore = (type == PumkinType.Red) ? 10 : 20;
    }

    void OnEnable()
    {
        isLive = true;
        pumkinHealth = pumkinMaxHealth;
    }

    void Start()
    {
        pumkinAni = GetComponent<Animator>();
        pumkinHp = GetComponentInChildren<PumkinHp>();
        pumkinHp.UpdateHealthBar(pumkinHealth, pumkinMaxHealth);

        isLive = 
            (type == PumkinType.Red) ?
            (GameManager.instance.redIsLive) : (GameManager.instance.blueIsLive);
    }

    void Update()
    {
        if (GameManager.instance.isGameover)
        {
            return;
        }

        if (!isLive) // 호박이 죽었다면
        {
            pumkinHp.ActiveHealthBar(false); // 체력바 비활성화
            pumkinAni.SetBool("isDead", true); // Dead 애니메이션 실행
            Destroy(gameObject, 1.5f); // 1.5초 뒤 파괴
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isLive && other.tag == "Bullet") // 총알과 충돌 시
        {
            TakeDamage();

            if(pumkinHealth <= 0) // 호박 체력이 0 밑으로 내려갔다면
            {
                isLive = false;
                Instantiate(pfPKExplosion, transform.position, transform.rotation); // 폭파 이펙트 생성
                audioSource.Play();
                GameManager.instance.AddScore(pumkinScore); // 점수 추가
            }
        }
    }

    public void TakeDamage()
    {
        pumkinHp.ActiveHealthBar(true); // 체력바 활성화
        pumkinHealth--; // 체력 1 감소
        pumkinHp.UpdateHealthBar(pumkinHealth, pumkinMaxHealth); // 체력바 업데이트
    }
}

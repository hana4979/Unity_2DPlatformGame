using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 외부 스크립트에서 GameManager 참조 시 사용
    [Header("# Player Info")]
    public float playerHealth; // 플레이어 체력
    public float playerMaxHealth = 100; // 플레이어 최대 체력
    [Header("# Enemy Info")]
    public float pumkinDamage = 10; // 호박 데미지
    public float boneDamage = 10; // 뼈 데미지
    public bool redIsLive = true; // 빨간 호박 생존 여부
    public bool blueIsLive = true; // 파란 호박 생존 여부
    [Header("# Game Object")]
    public GameObject player;
    public GameObject pumkin;
    public GameObject boss;
    public GameObject potal;
    [Header("# Key Info")]
    public int getCandy = 0; // 캔디 습득 개수
    public bool hasCandy = false; // 5개의 캔디 습득 여부
    [Header("# Game Info")]
    private int score = 0; // 현재 게임 점수

    public UIManager uiManager;
    public bool isGameover = false; // 게임오버 여부
    public bool isClear = false; // 게임 클리어 여부
    public bool inBossStage = false; // 보스 스테이지 진입 여부

    void Awake()
    {
        instance = this;

        uiManager = uiManager.GetComponent<UIManager>();
    }

    void Start()
    {
        playerHealth = playerMaxHealth;
    }

    public void ActivePotal()
    {
        potal.SetActive(true); // 포탈 활성화
    }

    public void AddScore(int newScore)
    {
        // 게임오버가 아닌 상태에서만 점수 추가 가능
        if (!isGameover)
        {
            score += newScore; // 점수 추가
            uiManager.UpdateScoreText(score);
        }
    }

    public void EndGame()
    {
        isGameover = true; // 게임오버 상태 참으로 변경
        uiManager.SetActiveGameoverUI(true); // 게임오버 UI 활성화
    }
}

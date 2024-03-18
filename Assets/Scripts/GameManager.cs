using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �ܺ� ��ũ��Ʈ���� GameManager ���� �� ���
    [Header("# Player Info")]
    public float playerHealth; // �÷��̾� ü��
    public float playerMaxHealth = 100; // �÷��̾� �ִ� ü��
    [Header("# Enemy Info")]
    public float pumkinDamage = 10; // ȣ�� ������
    public float boneDamage = 10; // �� ������
    public bool redIsLive = true; // ���� ȣ�� ���� ����
    public bool blueIsLive = true; // �Ķ� ȣ�� ���� ����
    [Header("# Game Object")]
    public GameObject player;
    public GameObject pumkin;
    public GameObject boss;
    public GameObject potal;
    [Header("# Key Info")]
    public int getCandy = 0; // ĵ�� ���� ����
    public bool hasCandy = false; // 5���� ĵ�� ���� ����
    [Header("# Game Info")]
    private int score = 0; // ���� ���� ����

    public UIManager uiManager;
    public bool isGameover = false; // ���ӿ��� ����
    public bool isClear = false; // ���� Ŭ���� ����
    public bool inBossStage = false; // ���� �������� ���� ����

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
        potal.SetActive(true); // ��Ż Ȱ��ȭ
    }

    public void AddScore(int newScore)
    {
        // ���ӿ����� �ƴ� ���¿����� ���� �߰� ����
        if (!isGameover)
        {
            score += newScore; // ���� �߰�
            uiManager.UpdateScoreText(score);
        }
    }

    public void EndGame()
    {
        isGameover = true; // ���ӿ��� ���� ������ ����
        uiManager.SetActiveGameoverUI(true); // ���ӿ��� UI Ȱ��ȭ
    }
}

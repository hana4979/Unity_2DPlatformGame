using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    private float currentTime = 0f;
    private float offsetTime = 3f;

    public GameObject pfBone; // �� ������
    public GameObject throwSpawn; // ������ ��ġ

    private bool isLive = true; // ���� ����
    private float bossHealth; // ���� ���� ü��
    private float bossMaxHealth = 30; // ���� �ִ� ü��

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
        // ���� �������� ���� ���� �ʾ����� �������� ����
        if (!GameManager.instance.inBossStage)
        {
            return;
        }

        if (!isLive)
        {
            bossAni.SetBool("isDead", true); // ���� �ִϸ��̼� ����
            audioSource.Play();
            Destroy(gameObject, 2f); // 2�� �� �ı�
            uiManager.SetActiveGameclearUI(true); // ����Ŭ���� UI Ȱ��ȭ
        }

        currentTime += Time.deltaTime;
        if(currentTime > offsetTime)
        {
            StartCoroutine(BossAttack()); // ���� �ڷ�ƾ ����
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
        Instantiate(pfBone, throwSpawn.transform.position, throwSpawn.transform.rotation); // �� ����
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLive && other.tag == "Bullet") // �Ѿ˰� �浹 ��
        {
            TakeDamage(); // ������ �޾��� �� ü�¹�
            
            if (bossHealth <= 0) // ���� ü���� 0 ������ �������ٸ�
            {
                GameManager.instance.AddScore(100); // ���� �߰�
                isLive = false;
                GameManager.instance.isClear = true;
            }
        }
    }

    public void TakeDamage()
    {
        //pumkinHp.ActiveHealthBar(true); // ü�¹� Ȱ��ȭ
        bossHealth--; // ü�� 1 ����
        //pumkinHp.UpdateHealthBar(pumkinHealth, pumkinMaxHealth); // ü�¹� ������Ʈ
    }
}

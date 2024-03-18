using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumkinData : MonoBehaviour
{
    public GameObject pfPKExplosion;

    private Animator pumkinAni;
    private AudioSource audioSource;

    public bool isLive; // ���� ����
    private float pumkinHealth; // ȣ�� ü��
    private float pumkinMaxHealth; // ȣ�� �ִ� ü��
    private PumkinHp pumkinHp;
    private int pumkinScore; // ȣ�� óġ ����

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

        if (!isLive) // ȣ���� �׾��ٸ�
        {
            pumkinHp.ActiveHealthBar(false); // ü�¹� ��Ȱ��ȭ
            pumkinAni.SetBool("isDead", true); // Dead �ִϸ��̼� ����
            Destroy(gameObject, 1.5f); // 1.5�� �� �ı�
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isLive && other.tag == "Bullet") // �Ѿ˰� �浹 ��
        {
            TakeDamage();

            if(pumkinHealth <= 0) // ȣ�� ü���� 0 ������ �������ٸ�
            {
                isLive = false;
                Instantiate(pfPKExplosion, transform.position, transform.rotation); // ���� ����Ʈ ����
                audioSource.Play();
                GameManager.instance.AddScore(pumkinScore); // ���� �߰�
            }
        }
    }

    public void TakeDamage()
    {
        pumkinHp.ActiveHealthBar(true); // ü�¹� Ȱ��ȭ
        pumkinHealth--; // ü�� 1 ����
        pumkinHp.UpdateHealthBar(pumkinHealth, pumkinMaxHealth); // ü�¹� ������Ʈ
    }
}

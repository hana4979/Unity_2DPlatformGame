using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    private float moveLR;
    private float speed = 4.0f;
    private int jumpCount = 0; // ���� ���� Ƚ��

    private float poisonDamage = 5; // �� ������
    private float currentTime = 0; // ����ð�
    private float poisonDamageTime = 1f; // �������� �Դ� �ð�

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

        //.. ĳ���� �¿� ������
        // Ű���� �¿� ��ư (����Ű/wasd), A : -1 / D : 1
        moveLR = Input.GetAxisRaw("Horizontal");
        // transform.right : ���������� ����
        transform.Translate(transform.right * moveLR * Time.deltaTime * speed); // �¿� ������

        //.. ĳ���� ���� ������
        if (Input.GetKeyDown(KeyCode.W) && jumpCount < 2)
        {
            jumpCount++; // ���� Ƚ�� ����
            playerRigid.velocity = Vector2.zero; // ���� ���� �ӵ��� ���������� ���η� ����
            playerRigid.AddForce(new Vector2(0, 5.0f), ForceMode2D.Impulse);
            playerAni.SetBool("isJump", true); // Jump ���� ����
        }
        // w��ư���� ���� ���� ���� && �ӵ��� y ���� ������ (���� ��� ��)
        else if (Input.GetKeyUp(KeyCode.W) && playerRigid.velocity.y > 0)
        {
            playerRigid.velocity = playerRigid.velocity * 0.5f; // ���� �ӵ��� �������� ����
        }
        else
        {
            playerAni.SetBool("isJump", false);

            if (playerRigid.velocity.y != 0)
            {
                playerAni.SetBool("isJump", true);
            }
        }

        //.. ĳ���� �޸��� �ִϸ��̼�
        if (moveLR == -1 || moveLR == 1) // ���� or ������ �̵�
        {
            playerAni.SetBool("isRun", true); // Run ���� ����

            if (playerRigid.velocity.y != 0) // ���� ���¶��
            {
                playerAni.SetBool("isJump", true); // Jump ���� ����
                playerAni.SetBool("isRun", false);
            }
        }
        else
        {
            playerAni.SetBool("isRun", false);
        }

        //.. ĳ���� �¿� ����
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
        if (collision.gameObject.tag == "Ground") // ���� ��Ҵٸ�
        {
            jumpCount = 0; // ���� Ƚ�� �ʱ�ȭ
        }

        if (collision.gameObject.tag == "Enemy") // ���� �浹 ��
        {
            onDamage(GameManager.instance.pumkinDamage);

            if (GameManager.instance.playerHealth <= 0) // �÷��̾� ü���� �� ������
            {
                playerDead();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Key") // ����(����)�� �浹 ��
        {
            GameManager.instance.getCandy++;
            audioSource.Play();
            Heal(); // ü�� ����
            GameManager.instance.AddScore(5); // ���� �߰�
            Destroy(other.gameObject); // ���� ���� �ı�

            if(GameManager.instance.getCandy == 5)
            {
                GameManager.instance.hasCandy = true;
                GameManager.instance.ActivePotal(); // ��Ż Ȱ��ȭ
            }
        }

        if(other.tag == "Bone") // ���� �浹 ��
        {
            onDamage(GameManager.instance.boneDamage);

            if (GameManager.instance.playerHealth <= 0) // �÷��̾� ü���� �� ������
            {
                playerDead();
            }
        }

        if(other.tag == "DeadZone") // �������� �浹 ��
        {
            playerDead();
        }

        if (other.tag == "Poison") // ���� �浹 ��
        {
            onDamage(poisonDamage);

            if (GameManager.instance.playerHealth <= 0) // �÷��̾� ü���� �� ������
            {
                playerDead();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Poison") // ���� �浹�ϰ� ���� ���
        {
            currentTime += Time.deltaTime;
            if(currentTime > poisonDamageTime) // 1�ʿ� �� ���� ��������
            {
                onDamage(poisonDamage);
                currentTime = 0;
            }

            if (GameManager.instance.playerHealth <= 0) // �÷��̾� ü���� �� ������
            {
                playerDead();
            }
        }
    }

    // �÷��̾� ��� ��
    private void playerDead()
    {
        GameManager.instance.EndGame();
    }

    // �������� ���� �� ����Ʈ
    IEnumerator DamageEffect()
    {
        playerRend.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        playerRend.material.color = Color.white;
        yield return new WaitForSeconds(0.2f);
    }

    // �������� ������ ��ȭ�ϴ� ü�°� UI, Effect ����
    private void onDamage(float damage)
    {
        GameManager.instance.playerHealth -= damage; // damage��ŭ ü�� ����
        GameManager.instance.uiManager.PlayerHp(); // HP UI ����
        StartCoroutine(DamageEffect()); // �浹 �� ����Ʈ ����
    }

    // ����(Ű) ���� ��� ü�� ȸ��
    private void Heal()
    {
        if(GameManager.instance.playerHealth >= 90) // ü���� 90 �̻��̶��
        {
            GameManager.instance.playerHealth = 100; // ü�� 100���� ����
            GameManager.instance.uiManager.PlayerHp(); // HP UI ����
        }
        else
        {
            GameManager.instance.playerHealth += 10; // ü�� 10 ����
            GameManager.instance.uiManager.PlayerHp(); // HP UI ����
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // �� ������ ���� �ڵ�

public class UIManager : MonoBehaviour
{
    public Image playerHpGage; // �÷��̾��� ü�¹�
    public Text scoreText; // ���� �ؽ�Ʈ
    public GameObject gameoverUI; // ���ӿ��� �� Ȱ��ȭ�� UI
    public GameObject gameclearUI; // ����Ŭ���� �� Ȱ��ȭ�� UI

    public void PlayerHp()
    {
        float playerHealth = GameManager.instance.playerHealth; // ���� �÷��̾� ü�� �޾ƿ���
        float playerMaxHealth = GameManager.instance.playerMaxHealth;
        playerHpGage.fillAmount = playerHealth / playerMaxHealth;
    }

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

    // ���ӿ��� UI Ȱ��ȭ
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    // ����Ŭ���� UI Ȱ��ȭ
    public void SetActiveGameclearUI(bool active)
    {
        gameclearUI.SetActive(active);
    }

    // ���� �����
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

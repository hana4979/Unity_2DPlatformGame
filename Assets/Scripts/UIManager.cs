using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드

public class UIManager : MonoBehaviour
{
    public Image playerHpGage; // 플레이어의 체력바
    public Text scoreText; // 점수 텍스트
    public GameObject gameoverUI; // 게임오버 시 활성화할 UI
    public GameObject gameclearUI; // 게임클리어 시 활성화할 UI

    public void PlayerHp()
    {
        float playerHealth = GameManager.instance.playerHealth; // 현재 플레이어 체력 받아오기
        float playerMaxHealth = GameManager.instance.playerMaxHealth;
        playerHpGage.fillAmount = playerHealth / playerMaxHealth;
    }

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

    // 게임오버 UI 활성화
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    // 게임클리어 UI 활성화
    public void SetActiveGameclearUI(bool active)
    {
        gameclearUI.SetActive(active);
    }

    // 게임 재시작
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

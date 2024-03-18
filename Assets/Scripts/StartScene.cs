using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene(1); // 게임 화면 진입
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobby : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void GoToDungeon()//던전으로 이동
    {
        StartCoroutine(gameManager.LoadSceneWithBlackout("MainScene"));
    }
    public void GoToTraining()//훈련장으로 이동
    {
        //SceneManager.LoadScene("TrainingScene");
        Debug.Log("훈련장으로 이동");
    }
    public void GoToShop()//상점으로 이동
    {
        //SceneManager.LoadScene("ShopScene");
        Debug.Log("상점으로 이동");
    }
}

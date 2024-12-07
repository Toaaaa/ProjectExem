using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobby : MonoBehaviour
{
    GameManager gameManager;

    ////메인로비 UI
    public GameObject ShopUI;
    public GameObject TrainingUI;
    public GameObject PrepareUI;


    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void GoToDungeon()//던전으로 이동
    {
        StartCoroutine(gameManager.LoadSceneWithBlackout("MainScene"));
    }


    public void StartPrepare()
    {
        PrepareUI.SetActive(true);
    }
    public void EndPrepare()
    {
        PrepareUI.SetActive(false);
    }
    public void StartTraining()//훈련장으로 이동
    {
        TrainingUI.SetActive(true);
    }
    public void EndTraining()
    {
        TrainingUI.SetActive(false);
    }
    public void OpenShop()//상점으로 이동
    {
        ShopUI.SetActive(true);
    }
    public void CloseShop()
    {
        ShopUI.SetActive(false);
    }
}

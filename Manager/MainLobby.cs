using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobby : MonoBehaviour
{
    GameManager gameManager;

    ////���ηκ� UI
    public GameObject ShopUI;
    public GameObject TrainingUI;
    public GameObject PrepareUI;


    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void GoToDungeon()//�������� �̵�
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
    public void StartTraining()//�Ʒ������� �̵�
    {
        TrainingUI.SetActive(true);
    }
    public void EndTraining()
    {
        TrainingUI.SetActive(false);
    }
    public void OpenShop()//�������� �̵�
    {
        ShopUI.SetActive(true);
    }
    public void CloseShop()
    {
        ShopUI.SetActive(false);
    }
}

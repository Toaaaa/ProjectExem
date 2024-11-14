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

    public void GoToDungeon()//�������� �̵�
    {
        StartCoroutine(gameManager.LoadSceneWithBlackout("MainScene"));
    }
    public void GoToTraining()//�Ʒ������� �̵�
    {
        //SceneManager.LoadScene("TrainingScene");
        Debug.Log("�Ʒ������� �̵�");
    }
    public void GoToShop()//�������� �̵�
    {
        //SceneManager.LoadScene("ShopScene");
        Debug.Log("�������� �̵�");
    }
}

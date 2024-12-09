using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobby : MonoBehaviour
{
    GameManager gameManager;

    ////���ηκ� UI
    public GameObject ShopUI;
    public GameObject TrainingUI;
    public GameObject PrepareUI;

    public TextMeshProUGUI shopButtonText;
    public TextMeshProUGUI trainingButtonText;
    public TextMeshProUGUI prepareButtonText;


    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void GoToDungeon()//�������� �̵�
    {
        StartCoroutine(gameManager.LoadSceneWithBlackout("MainScene"));
    }

    private void Update()
    {
        shopButtonText.text = gameManager.localizationManager.GetLocalizedString("ShopButton");
        trainingButtonText.text = gameManager.localizationManager.GetLocalizedString("TrainingButton");
        prepareButtonText.text = gameManager.localizationManager.GetLocalizedString("PrepareButton");
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

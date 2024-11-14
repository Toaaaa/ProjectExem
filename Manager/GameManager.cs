using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public StageManager stageManager;
    public CharacterManager characterManager;

    public Canvas mainCanvas;//���ƿ��� �ɼǵ� ���� ���ݿ��� ���� UI���� ��� ĵ����.
    [SerializeField]
    Image BlackOut;
    [SerializeField]
    GameObject OptionUI;

    private void Awake()
    {
        //�̱���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }
    private void Start()
    {
        Color color = BlackOut.color;
        color.a = 0;
        BlackOut.color = color;
    }

    public void GameStart()
    {
        //LoadGame �Ǵ� NewGame�� �����ϴ� UI�� ��� ��ȹ ������ �ϴ��� �ٷ� ������ �����ϵ��� ����
        StartCoroutine(LoadSceneWithBlackout("Lobby"));
    }
    public void LoadGame()
    {

    }
    public void NewGame()
    {

    }
    public void OptionOpen()
    {

    }
    public IEnumerator LoadSceneWithBlackout(string sceneName)//���ƿ� ȿ���� �Բ� ���� �ε��ϴ� �Լ�.
    {
        // 1. ���ƿ� ���� (1�� ���� ��ο���)
        mainCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        yield return BlackOut.DOFade(1f, 1f).WaitForCompletion();

        // 2. �񵿱� �� �ε� ����
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // �� �ε� �� ���

        // 3. ���� �ε�� ������ ���
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f) // �ε� �Ϸ� ���� ���
                break;
            yield return null;
        }

        // 4. �� �ε� Ȱ��ȭ
        asyncLoad.allowSceneActivation = true;

        // 5. ���ƿ� ���� (1�� ���� �����ϰ� ����)
        BlackOut.DOFade(0f, 1f).OnComplete(() =>
        {
            mainCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        });
    }
    public void BlackOutScreen()//ȭ�� ���ƿ� ȿ��.
    {
        mainCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        BlackOut.DOFade(1f, 1f);
    }
    public void BlackOutEnd()//ȭ�� ���ƿ� ȿ�� ����.
    {
        BlackOut.DOFade(0f, 1f).OnComplete(() =>
        {
            mainCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        });
    }
}

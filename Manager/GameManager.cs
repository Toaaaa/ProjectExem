using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public StageManager stageManager;
    public CharacterManager characterManager;
    public PartyManager partyManager;
    public InventoryManager inventoryManager;
    public ShopUIManager shopUIManager;
    public LocalizationManager localizationManager;
    public ResolutionManager resolutionManager;
    public CardManager cardManager;

    public Canvas mainCanvas;//블랙아웃과 옵션등 게임 전반에서 쓰일 UI들이 담긴 캔버스.
    [SerializeField]
    Image BlackOut;
    [SerializeField]
    SettingUI OptionUI;

    public bool isPopupOn;
    public bool isMainUIOn;

    private void Awake()
    {
        //싱글톤
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
        if (BlackOut != null)
        {
            Color color = BlackOut.color;
            color.a = 0;
            BlackOut.color = color;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ForTheTest0();
            //inventoryManager.SaveInventory();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ForTheTest1();
            //inventoryManager.SaveInventory();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMainUIOn)
            {
                OptionUI.gameObject.SetActive(true);
            }
        }
    }
    public void ForTheTest0()
    {
        inventoryManager.bagpack.AddItem(ItemDatabase.Instance.GetItem(2), 1);
        inventoryManager.inventoryData.items[2].quantity += 1;
        #if UNITY_EDITOR
        EditorUtility.SetDirty(inventoryManager.inventoryData);
        #endif
        inventoryManager.bagUI.UpdateUI();
        //이후에는 인덱스 숫자에 선택한 아이템의.id를 넣는 방식으로 변경.
    }
    public void ForTheTest1()
    {
        inventoryManager.bagpack.AddItem(ItemDatabase.Instance.GetItem(3), 1);
        inventoryManager.inventoryData.items[3].quantity += 1;
        #if UNITY_EDITOR
        EditorUtility.SetDirty(inventoryManager.inventoryData);
        #endif
        inventoryManager.bagUI.UpdateUI();
    }
    public void GameStart()
    {
        //LoadGame 또는 NewGame을 선택하는 UI를 띄울 계획 이지만 일단은 바로 게임을 시작하도록 설정
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
        OptionUI.gameObject.SetActive(true);
    }
    public IEnumerator LoadSceneWithBlackout(string sceneName)//블랙아웃 효과와 함께 씬을 로드하는 함수.
    {
        // 1. 블랙아웃 실행 (1초 동안 어두워짐)
        mainCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        yield return BlackOut.DOFade(1f, 1f).WaitForCompletion();

        // 2. 비동기 씬 로드 시작
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // 씬 로드 후 대기

        // 3. 씬이 로드될 때까지 대기
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f) // 로드 완료 상태 대기
                break;
            yield return null;
        }

        // 4. 씬 로드 활성화
        asyncLoad.allowSceneActivation = true;

        // 5. 블랙아웃 해제 (1초 동안 투명하게 변경)
        BlackOut.DOFade(0f, 1f).OnComplete(() => //동기 방식인 waitforcompletion을 사용하니, 씬 내용물이 많은경우 제대로 작동하지 않아, 비동기 방식인 oncomplete를 사용.
        {
            mainCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        });
    }
    public void BlackOutScreen()//화면 블랙아웃 효과.
    {
        mainCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        BlackOut.DOFade(1f, 1f);
    }
    public void BlackOutEnd()//화면 블랙아웃 효과 해제.
    {
        BlackOut.DOFade(0f, 1f).OnComplete(() =>
        {
            mainCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        });
    }
}

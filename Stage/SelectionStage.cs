using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class SelectionStage : Stage // 선택지 스테이지
{
    public GameObject ImageObj; // 스토리 이미지(테두리를 포함한 이미지 오브젝트)
    public Image storyImage; // 스토리 이미지
    public Image blackBack; // 스토리 이미지가 변경될때 사용되는 검정색 이미지. (alpha값 조절로 페이드인 페이드아웃 효과)
    int selectionNum; // 현재 진행중인 선택지의 번호.
    [Header("Choice Presets")]
    public List<SelectionPreset> selectionPresets; // 선택지 프리셋 리스트
    [SerializeField]
    SelectionPreset currentSelection; // 현재 선택된 선택지


    protected override void OnEnable()
    {
        base.OnEnable();
        currentSelection = GenerateSingleChoice();// 확률에 따른 선택지 선정
        UpdateSelected();
        ButtonPlacement();

    }
    private void Update()
    {
        ButtonPlacement();
    }
    public SelectionPreset GenerateSingleChoice()
    {
        float totalChance = 0f;

        // 전체 확률 합산
        foreach (var preset in selectionPresets)
        {
            totalChance += preset.appearanceChance;
        }

        // 0부터 totalChance 사이의 랜덤 값 생성
        float randomValue = Random.value * totalChance;

        // 누적 확률로 선택지 결정
        float cumulative = 0f;
        foreach (var preset in selectionPresets)
        {
            cumulative += preset.appearanceChance;
            if (randomValue <= cumulative)
            {
                Debug.Log($"선택된 선택지: {preset.selectionID}");
                return preset; // 선택된 프리셋 반환
            }
        }

        // 확률 값이 잘못되었을 경우 null 반환
        Debug.LogError("선택지가 없습니다.");
        return null;
    }

    void ButtonPlacement()//버튼의 개수에 따른 위치 설정
    {
        if(currentSelection.isButtonThree)
        {
            //setbuttonposx(버튼번호, x좌표)
            SetButtonPosX(0, -130f);
            SetButtonPosX(1, 290f);
            SetButtonPosX(2, 705f);
            SetButtonPosX(3, 290f);
        }
        else
        {
            SetButtonPosX(0, 90f);
            SetButtonPosX(1, 510f);
            SetButtonPosX(2, 290f);
        }
    }
    void UpdateSelected()// 선택지의 텍스트와 버튼의 텍스트 설정
    {
        selectionNum = currentSelection.selectionID;
        if (currentSelection.isButtonThree)
        {
            for(int i = 0; i < 3; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].onClick.RemoveAllListeners();
            }
            buttons[0].onClick.AddListener(NextImg1);
            buttons[1].onClick.AddListener(NextImg2);
            buttons[2].onClick.AddListener(NextImg3);
            //버튼 on click 함수 리셋.
            buttons[3].gameObject.SetActive(false);
        }//버튼 활성화 비활성화.
        else
        {
            for (int i = 0; i < 2; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].onClick.RemoveAllListeners();
            }
            buttons[0].onClick.AddListener(NextImg1);
            buttons[1].onClick.AddListener(NextImg2);
            //버튼 on click 함수 리셋.
            buttons[2].gameObject.SetActive(false);
        }
        TextShow(0);
        ButtonDesc();
        if(currentSelection.isButtonDown)//버튼의 위치 아래 (텍스트 양이 많음)
        {
            SetButtonPosY(-85f);
        }//버튼의 위치 설정
        else//버튼의 위치 위 (텍스트 양이 적음)
        {
            SetButtonPosY(-35f); // 디폴트 위치
        }
        ShowFirstImage();
    }
    void SetButtonPosX(int i,float posX)//버튼의 위치 설정
    {
        Vector2 pos = buttons[i].GetComponent<RectTransform>().anchoredPosition;
        pos.x = posX;
        buttons[i].GetComponent<RectTransform>().anchoredPosition = pos;
    }
    void SetButtonPosY(float posY)//버튼의 위치 설정
    {
        for(int i = 0; i < 4; i++)
        {
            Vector2 pos = buttons[i].GetComponent<RectTransform>().anchoredPosition;
            pos.y = posY;
            buttons[i].GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
    void TextShow(int i)//하단 텍스트 출력 효과. Stage의stageDesc에 대입.
    {
        if (currentSelection.isEventDesc2)
        {
            int percent = Random.Range(0, 100);
            if(percent < currentSelection.percent)
            {
                stageDesc.text = currentSelection.EventDesc2[0];
                return;
            }
            else
            {
                stageDesc.text = currentSelection.EventDesc2[1];
                return;
            }
        }
        //추후 언어변경을 넣을 경우
        //언어 enum의 int값 * 버튼의 개수 + i를 아래에 대입해 텍스트 출력.
        stageDesc.text = currentSelection.EventDesc[i];
        //추후 텍스트 출력 효과 넣을수도 있음.
    }
    void ButtonDesc()//버튼의 텍스트 설정
    {
        if(currentSelection.isButtonThree)
        {
            for (int i = 0; i < 3; i++)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentSelection.buttonDesc[i];
            }
        }
        else
        {
            for(int i = 0; i < 2; i++)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentSelection.buttonDesc[i];
            }
        }
    }
    async void LastButton()//버튼을 선택 하였고, 다음방으로 가는 버튼으로 변경해주는 함수.(button1~3에서 사용)
    {
        if(currentSelection.instantNext)
        {
            if(currentSelection.isButtonThree)
            {
                for (int i = 0; i < 3; i++)
                {
                    buttons[i].gameObject.SetActive(false);
                }
                buttons[3].gameObject.SetActive(true);
                buttons[3].interactable = false;
                await UniTask.Delay(700);
                buttons[3].onClick.RemoveAllListeners();
                buttons[3].onClick.AddListener(SpecialNextRoom);//특수한 조건으로 다음방으로 이동하는 버튼으로 변경.
                buttons[3].interactable = true;

            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    buttons[i].gameObject.SetActive(false);
                }
                buttons[2].gameObject.SetActive(true);
                buttons[2].interactable = false;
                await UniTask.Delay(700);
                buttons[2].onClick.RemoveAllListeners();
                buttons[2].onClick.AddListener(SpecialNextRoom);//특수한 조건으로 다음방으로 이동하는 버튼으로 변경.
                buttons[2].interactable = true;
            }
            return;
        }

        if (currentSelection.isButtonThree)
        {
            for(int i = 0; i < 3; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
            buttons[3].gameObject.SetActive(true);
            buttons[3].interactable =false;
            await UniTask.Delay(700);
            buttons[3].onClick.RemoveAllListeners();
            buttons[3].onClick.AddListener(NextRoomButton);
            buttons[3].interactable = true;
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
            buttons[2].gameObject.SetActive(true);
            buttons[2].interactable = false;
            await UniTask.Delay(700);
            buttons[2].onClick.RemoveAllListeners();
            buttons[2].onClick.AddListener(NextRoomButton);
            buttons[2].interactable = true;
        }
    }
    public void Button1()
    {
        //델리게이트 처럼 사용하기.
    }
    public void Button2()
    {
        
    }
    public void Button3()
    {
        
    }

    public void NextRoomButton()// 다음방으로 이동하는 버튼
    {
        DisableImage();
        GameManager.Instance.stageManager.NextStage();
    }
    public void SpecialNextRoom()//특수한 조건으로 다음방으로 이동하는 버튼 (조건에 따라 다름?)
    {
        //case 문으로 나누어 다음 방 or 던전 탈출등..
        DisableImage();
        Debug.Log("특수한 조건으로 다음방으로 이동");
    }
    public void ShowFirstImage()//스토리 이미지의 첫 이미지 출력.
    {
        //storyimage와 image의 alpha값을 1로 천천히 올림.
        storyImage.sprite = currentSelection.choiceImages[0];//선택지의 첫 이미지를 입력.
        Image image = ImageObj.GetComponent<Image>();
        Color color = image.color;
        Color color2 = storyImage.color;
        color.a = 0;
        color2.a = 0;
        image.color = color;
        storyImage.color = color2;
        image.DOFade(1, 1f);
        storyImage.DOFade(1, 1f);
    }
    public void DisableImage()
    {
        Image image = ImageObj.GetComponent<Image>();
        Color color = image.color;
        Color color2 = storyImage.color;
        color.a = 1;
        color2.a = 1;
        image.color = color;
        storyImage.color = color2;
        image.DOFade(0, 1f);
        storyImage.DOFade(0, 1f);
    }
    void NextImage(int choiceNum)//선택지 이미지 변경
    {
        Color color = blackBack.color;
        color.a = 0;
        blackBack.color = color;
        blackBack.DOFade(1, 1.3f).OnComplete(() =>
        {
            storyImage.sprite = currentSelection.choiceImages[choiceNum];
            blackBack.DOFade(0, 1.7f);
            TextShow(choiceNum);
            LastButton();
        });
    }
    void NextImg1()
    {
        NextImage(1);
    }
    void NextImg2()
    {
        NextImage(2);
    }
    void NextImg3()
    {
        NextImage(3);
    }
}
[System.Serializable]
public class SelectionPreset
{
    public string appearanceName; // 선택지 이름

    public int selectionID; // 선택지 번호

    [Range(0f, 1f)] // 확률 값은 0~1로 제한 (0% ~ 100%)
    public float appearanceChance; // 등장 확률

    public List<Sprite> choiceImages; // 사용할 이미지 리스트
    public List<Sprite> choiceImages2; // isEventDesc2가 true일때 사용할 이미지 리스트

    [TextArea]
    public List<string> EventDesc; // 텍스트 스크립트
    public bool isEventDesc2; // 추가 텍스트가 있는지
    public int percent; // 추가 텍스트에서 1번의 확률.
    [TextArea]
    public List<string> EventDesc2; //추가 텍스트 (선택지에 따른 결과가 다를 경우)
    public List<string> buttonDesc; // 버튼 텍스트 리스트

    public bool isButtonDown;// 버튼의 위치가 아래인지 (텍스트 양이 많을때 사용)
    public bool isButtonThree;// 버튼이 3개인지
    public bool instantNext; // 즉시 다음 스테이지로 넘어가는지 (다른 버튼을 누르지 않아도 다음 스테이지로 넘어가는 경우)

}

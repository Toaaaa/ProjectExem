using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

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

    void UpdateSelected()// 선택지의 텍스트와 버튼의 텍스트 설정
    {
        selectionNum = currentSelection.selectionID;
        if (currentSelection.isButtonThree)
        {
            for(int i = 0; i < 3; i++)
            {
                buttons[i].gameObject.SetActive(true);
            }
            buttons[3].gameObject.SetActive(false);
        }//버튼 활성화 비활성화.
        else
        {
            for (int i = 0; i < 2; i++)
            {
                buttons[i].gameObject.SetActive(true);
            }
            buttons[2].gameObject.SetActive(false);
        }
        TextShow(selectionNum);
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
    void SetButtonPosY(float posY)//버튼의 위치 설정
    {
        for(int i = 0; i < 3; i++)
        {
            Vector2 pos = buttons[i].GetComponent<RectTransform>().anchoredPosition;
            pos.y = posY;
            buttons[i].GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
    void TextShow(int selectionNum)//하단 텍스트 출력 효과. Stage의stageDesc에 대입.
    {
        stageDesc.text = currentSelection.EventDesc[0];
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
    void LastButton()//버튼을 선택 하였고, 다음방으로 가는 버튼으로 변경해주는 함수.(button1~3에서 사용)
    {
        if (currentSelection.isButtonThree)
        {
            for(int i = 0; i < 3; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
            buttons[3].gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
            buttons[2].gameObject.SetActive(true);
        }
    }
    public void Button1()
    {
        NextImage(1);//기능 테스트
    }
    public void Button2()
    {
        NextImage(2);//기능 테스트
    }
    public void Button3()
    {
        NextImage(3);//기능 테스트
    }
    public void NextRoomButton()// 다음방으로 이동하는 버튼 (조건에 따라 다름)
    {

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
            blackBack.DOFade(0, 1.3f);
        });
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

    [TextArea]
    public List<string> EventDesc; // 텍스트 스크립트
    public List<string> buttonDesc; // 버튼 텍스트 리스트

    public bool isButtonDown;// 버튼의 위치가 아래인지 (텍스트 양이 많을때 사용)
    public bool isButtonThree;// 버튼이 3개인지

}

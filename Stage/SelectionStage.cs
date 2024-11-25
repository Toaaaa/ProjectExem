using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class SelectionStage : Stage // ������ ��������
{
    public GameObject ImageObj; // ���丮 �̹���(�׵θ��� ������ �̹��� ������Ʈ)
    public Image storyImage; // ���丮 �̹���
    public Image blackBack; // ���丮 �̹����� ����ɶ� ���Ǵ� ������ �̹���. (alpha�� ������ ���̵��� ���̵�ƿ� ȿ��)
    int selectionNum; // ���� �������� �������� ��ȣ.
    [Header("Choice Presets")]
    public List<SelectionPreset> selectionPresets; // ������ ������ ����Ʈ
    [SerializeField]
    SelectionPreset currentSelection; // ���� ���õ� ������


    protected override void OnEnable()
    {
        base.OnEnable();
        currentSelection = GenerateSingleChoice();// Ȯ���� ���� ������ ����
        UpdateSelected();


    }

    public SelectionPreset GenerateSingleChoice()
    {
        float totalChance = 0f;

        // ��ü Ȯ�� �ջ�
        foreach (var preset in selectionPresets)
        {
            totalChance += preset.appearanceChance;
        }

        // 0���� totalChance ������ ���� �� ����
        float randomValue = Random.value * totalChance;

        // ���� Ȯ���� ������ ����
        float cumulative = 0f;
        foreach (var preset in selectionPresets)
        {
            cumulative += preset.appearanceChance;
            if (randomValue <= cumulative)
            {
                Debug.Log($"���õ� ������: {preset.selectionID}");
                return preset; // ���õ� ������ ��ȯ
            }
        }

        // Ȯ�� ���� �߸��Ǿ��� ��� null ��ȯ
        Debug.LogError("�������� �����ϴ�.");
        return null;
    }

    void UpdateSelected()// �������� �ؽ�Ʈ�� ��ư�� �ؽ�Ʈ ����
    {
        selectionNum = currentSelection.selectionID;
        if (currentSelection.isButtonThree)
        {
            for(int i = 0; i < 3; i++)
            {
                buttons[i].gameObject.SetActive(true);
            }
            buttons[3].gameObject.SetActive(false);
        }//��ư Ȱ��ȭ ��Ȱ��ȭ.
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
        if(currentSelection.isButtonDown)//��ư�� ��ġ �Ʒ� (�ؽ�Ʈ ���� ����)
        {
            SetButtonPosY(-85f);
        }//��ư�� ��ġ ����
        else//��ư�� ��ġ �� (�ؽ�Ʈ ���� ����)
        {
            SetButtonPosY(-35f); // ����Ʈ ��ġ
        }
        ShowFirstImage();
    }
    void SetButtonPosY(float posY)//��ư�� ��ġ ����
    {
        for(int i = 0; i < 3; i++)
        {
            Vector2 pos = buttons[i].GetComponent<RectTransform>().anchoredPosition;
            pos.y = posY;
            buttons[i].GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
    void TextShow(int selectionNum)//�ϴ� �ؽ�Ʈ ��� ȿ��. Stage��stageDesc�� ����.
    {
        stageDesc.text = currentSelection.EventDesc[0];
        //���� �ؽ�Ʈ ��� ȿ�� �������� ����.
    }
    void ButtonDesc()//��ư�� �ؽ�Ʈ ����
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
    void LastButton()//��ư�� ���� �Ͽ���, ���������� ���� ��ư���� �������ִ� �Լ�.(button1~3���� ���)
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
        NextImage(1);//��� �׽�Ʈ
    }
    public void Button2()
    {
        NextImage(2);//��� �׽�Ʈ
    }
    public void Button3()
    {
        NextImage(3);//��� �׽�Ʈ
    }
    public void NextRoomButton()// ���������� �̵��ϴ� ��ư (���ǿ� ���� �ٸ�)
    {

    }
    public void ShowFirstImage()//���丮 �̹����� ù �̹��� ���.
    {
        //storyimage�� image�� alpha���� 1�� õõ�� �ø�.
        storyImage.sprite = currentSelection.choiceImages[0];//�������� ù �̹����� �Է�.
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
    void NextImage(int choiceNum)//������ �̹��� ����
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
    public string appearanceName; // ������ �̸�

    public int selectionID; // ������ ��ȣ

    [Range(0f, 1f)] // Ȯ�� ���� 0~1�� ���� (0% ~ 100%)
    public float appearanceChance; // ���� Ȯ��

    public List<Sprite> choiceImages; // ����� �̹��� ����Ʈ

    [TextArea]
    public List<string> EventDesc; // �ؽ�Ʈ ��ũ��Ʈ
    public List<string> buttonDesc; // ��ư �ؽ�Ʈ ����Ʈ

    public bool isButtonDown;// ��ư�� ��ġ�� �Ʒ����� (�ؽ�Ʈ ���� ������ ���)
    public bool isButtonThree;// ��ư�� 3������

}

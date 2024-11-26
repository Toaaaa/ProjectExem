using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

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
        ButtonPlacement();

    }
    private void Update()
    {
        ButtonPlacement();
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

    void ButtonPlacement()//��ư�� ������ ���� ��ġ ����
    {
        if(currentSelection.isButtonThree)
        {
            //setbuttonposx(��ư��ȣ, x��ǥ)
            SetButtonPosX(0, -130f);
            SetButtonPosX(1, 290f);
            SetButtonPosX(2, 705f);
            SetButtonPosX(3, 290f);
        }
        else
        {
            SetButtonPosX(0, 80f);
            SetButtonPosX(1, 520f);
            SetButtonPosX(2, 290f);
        }
    }
    void UpdateSelected()// �������� �ؽ�Ʈ�� ��ư�� �ؽ�Ʈ ����
    {
        //bool�� �ʱ�ȭ
        if(currentSelection != null)
            currentSelection.isSelectSpecial = false;
        //������ ���� ������Ʈ
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
            //��ư on click �Լ� ����.
            buttons[3].gameObject.SetActive(false);
        }//��ư Ȱ��ȭ ��Ȱ��ȭ.
        else
        {
            for (int i = 0; i < 2; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].onClick.RemoveAllListeners();
            }
            buttons[0].onClick.AddListener(NextImg1);
            buttons[1].onClick.AddListener(NextImg2);
            //��ư on click �Լ� ����.
            buttons[2].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
        }
        TextShow(0);
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
    void SetButtonPosX(int i,float posX)//��ư�� ��ġ ����
    {
        Vector2 pos = buttons[i].GetComponent<RectTransform>().anchoredPosition;
        pos.x = posX;
        buttons[i].GetComponent<RectTransform>().anchoredPosition = pos;
    }
    void SetButtonPosY(float posY)//��ư�� ��ġ ����
    {
        for(int i = 0; i < 4; i++)
        {
            Vector2 pos = buttons[i].GetComponent<RectTransform>().anchoredPosition;
            pos.y = posY;
            buttons[i].GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
    void TextShow(int i)//�ϴ� �ؽ�Ʈ ��� ȿ��. Stage��stageDesc�� ����.
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
        //���� ������ ���� ���
        //��� enum�� int�� * ��ư�� ���� + i�� �Ʒ��� ������ �ؽ�Ʈ ���.
        stageDesc.text = currentSelection.EventDesc[i];
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
    async void LastButton()//��ư�� ���� �Ͽ���, ���������� ���� ��ư���� �������ִ� �Լ�.(button1~3���� ���)
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
                //����� ���������� ���� ����
                if (currentSelection.isSelectSpecial)//����� ���������� ���ý�
                    buttons[3].onClick.AddListener(SpecialNextRoom);//Ư���� �������� ���������� �̵��ϴ� ��ư���� ����.
                else
                    buttons[3].onClick.AddListener(NextRoomButton);//���������� �̵��ϴ� ��ư���� ����.
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
                //����� ���������� ���� ����
                if (currentSelection.isSelectSpecial)//����� ���������� ���ý�
                    buttons[2].onClick.AddListener(SpecialNextRoom);//Ư���� �������� ���������� �̵��ϴ� ��ư���� ����.
                else
                    buttons[2].onClick.AddListener(NextRoomButton);//���������� �̵��ϴ� ��ư���� ����.
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
    //��������Ʈ ó�� ����� ��ư.
    public async void Button1()
    {
        await UniTask.Delay(300);

        switch (selectionNum)
        {
            case 0://��ġ���� ����
                Debug.Log("�ķ� 1ĭ �Ҹ� + ���� ��Ƽ��Ʈ ȹ�� ui");
                break;
            case 1://���ϴ� ���
                Debug.Log("ü�� -20% + ����� �� ȹ��");
                break;
            case 2://ũ���� ����
                Debug.Log("�̰��� �� ���� (�ų� �����϶� + �ǰ� ������ ����)");
                break;
            case 3://��޽����� �ິ
                Debug.Log("���ȳ����� �ິ�� ȿ�� ����");
                break;
            case 4://������ ��
                currentSelection.isSelectSpecial = true;
                break;
            case 5://���� ���� �տ�
                Debug.Log("50%�� Ȯ���� ���ȳ� ü�� -20% or 20%ü�� ȹ�� ����");
                break;
            case 6://�ϴ��� ���� ����?
                Debug.Log("50%�� Ȯ���� ��⸦ ��� �ķ� ȹ�� or 50%�� Ȯ���� ���� ü�� -30%");
                break;
            case 7://������ �ķ� �ָӴ�
                Debug.Log("�ķ� 3ĭ ȹ��");
                break;
            case 8://��� ����
                Debug.Log("�ų� �ִ�ġ���� ȸ��");
                break;
            case 9://�ո� ���� ��
                currentSelection.isSelectSpecial = true;
                break;
            case 10://��Ÿ ���丮
                Debug.Log("����");
                break;


        }
    }
    public async void Button2()
    {
        await UniTask.Delay(300);

        switch (selectionNum)
        {
            case 0://��ġ���� ����
                Debug.Log("��Ƽ ü�� - 5%");
                break;
            case 1://���ϴ� ���
                Debug.Log("�ƹ��� ����");
                break;
            case 2://ũ���� ����
                currentSelection.isSelectSpecial = true;
                break;
            case 3://��޽����� �ິ
                Debug.Log("���̿��� �ິ�� ȿ�� ����");
                break;
            case 4://������ ��
                Debug.Log("");
                break;
            case 5://���� ���� �տ�
                Debug.Log("");
                break;
            case 6://�ϴ��� ���� ����?
                Debug.Log("");
                break;
            case 7://������ �ķ� �ָӴ�
                Debug.Log("");
                break;
            case 8://��� ����
                Debug.Log("");
                break;
            case 9://�ո� ���� ��
                Debug.Log("");
                break;
            case 10://��Ÿ ���丮
                Debug.Log("����");
                break;


        }
    }
    public async void Button3() //�������� 2���� ���, switch �� ���� �ƹ��� ����� ���� ����. (�ٸ������� ���ٽ����� ��� ��)
    {
        await UniTask.Delay(300);

        switch (selectionNum)
        {
            case 0://��ġ���� ����
                Debug.Log("������");
                break;
            case 1://���ϴ� ���
                Debug.Log("����� ���� ���� ����");
                break;
            case 2://ũ���� ����
                Debug.Log("");
                break;
            case 3://��޽����� �ິ
                Debug.Log("");
                break;
            case 4://������ ��
                Debug.Log("");
                break;
            case 5://���� ���� �տ�
                Debug.Log("");
                break;
            case 6://�ϴ��� ���� ����?
                Debug.Log("");
                break;
            case 7://������ �ķ� �ָӴ�
                Debug.Log("");
                break;
            case 8://��� ����
                Debug.Log("");
                break;
            case 9://�ո� ���� ��
                Debug.Log("");
                break;
            case 10://��Ÿ ���丮
                Debug.Log("����");
                break;


        }
    }

    public void NextRoomButton()// ���������� �̵��ϴ� ��ư
    {
        DisableImage();
        GameManager.Instance.stageManager.NextStage();
    }
    public void SpecialNextRoom()//Ư���� �������� ���������� �̵��ϴ� ��ư (���ǿ� ���� �ٸ�?)
    {
        //case ������ ������ ���� �� or ���� Ż���..
        DisableImage();
        Debug.Log("Ư���� �������� ���������� �̵�");
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
    public string appearanceName; // ������ �̸�

    public int selectionID; // ������ ��ȣ

    [Range(0f, 1f)] // Ȯ�� ���� 0~1�� ���� (0% ~ 100%)
    public float appearanceChance; // ���� Ȯ��

    public List<Sprite> choiceImages; // ����� �̹��� ����Ʈ
    public List<Sprite> choiceImages2; // isEventDesc2�� true�϶� ����� �̹��� ����Ʈ

    [TextArea]
    public List<string> EventDesc; // �ؽ�Ʈ ��ũ��Ʈ
    public bool isEventDesc2; // �߰� �ؽ�Ʈ�� �ִ���
    public int percent; // �߰� �ؽ�Ʈ���� 1���� Ȯ��.
    [TextArea]
    public List<string> EventDesc2; //�߰� �ؽ�Ʈ (�������� ���� ����� �ٸ� ���)
    public List<string> buttonDesc; // ��ư �ؽ�Ʈ ����Ʈ

    public bool isButtonDown;// ��ư�� ��ġ�� �Ʒ����� (�ؽ�Ʈ ���� ������ ���)
    public bool isButtonThree;// ��ư�� 3������
    public bool instantNext; // ��� ���� ���������� �Ѿ���� (�ٸ� ��ư�� ������ �ʾƵ� ���� ���������� �Ѿ�� ���)

    public bool isSelectSpecial; //����� ���������� ���� �����ߴ���?

}

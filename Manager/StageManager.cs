using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class StageManager : MonoBehaviour
{
    public int RoomNum;//���� ��ȣ
    string RoomName;//���� �̸�
    [SerializeField]
    List<Stage> StageList;
    int stageType; //0: Tutorial, 1: Starting, 2:QuietHideout,  3: BloodCave, 4: DampCave, 5: ForkedRoad, 6: CaveAlley, 7: EndPoint.
    int lastStageType;//���� ���������� Ÿ��.
    int nextStageType;//���� ���������� Ÿ��.

    public bool duringCombat;//���������� ����.

    public float EndPercent;//���� �̵��� �������� Ȯ��.

    private System.Random random = new System.Random();//�����Լ� �õ� �ʱ�ȭ.

    private void Start()
    {
        startStage();
    }

    private void Update()
    {
        
    }

    void startStage()//ó�� ���� ����� �Ա����� ����.
    {
        RoomNum = 0;
        stageType = 1;
        EndPercent = 0;
        StartStageEvent();//���������� ��ư ��ü + �������� ���� ��� ����.
    }
    async void UpdateStage()//���� ���������� ������Ʈ (���,��ư������,�������)
    {
        Debug.Log("���� ���������� �̵�");
        //ĳ���Ͱ� ������ �ȴ� �ִϸ��̼�
        //���� �� ����� ���ƿ� �ϸ� �ڷ� ���� ����
        await UniTask.Delay(1000);
        //����� ȭ��Ʈ �ƿ� �ϸ� ���ο� ���������� ����� ������ ����.
        await UniTask.Delay(1000);
        lastStageType = stageType;
        stageType = nextStageType;
        switch (stageType)
        {
            case 0:
                RoomName = "StartingPoint";
                break;
            case 1:
                RoomName = "StartingPoint";
                break;
            case 2:
                RoomName = "QuietHideout";
                break;
            case 3:
                RoomName = "BloodCave";
                break;
            case 4:
                RoomName = "DampCave";
                break;
            case 5:
                RoomName = "ForkedRoad";
                break;
            case 6:
                RoomName = "CaveAlley";
                break;
            case 7:
                RoomName = "EndPoint";
                break;
            default:
                RoomName = "ERROR";
                break;
        }//�������� �̸� ����.
        StartStageEvent();//�������� �̺�Ʈ ����.
    }
    void NextStageRandom()//������ Ȯ���� ���� ���������� ����.
    {
        if(RoomNum >= 50)
        {
            nextStageType = random.Next(0, 100) <= EndPercent ? 7 : 0; //���� ����Ʈ Ȯ�� ����.
            if(nextStageType == 7)
            {
                return;
            }
        }
        int maxTries = 100; // ���� ���� ������ �õ� Ƚ�� ����
        nextStageType = PercentOfStage(); // ���� �������� Ÿ���� �������� ����
        // ���� ���������� �������� ������ �ʵ��� �ݺ� �õ�
        while (stageType == nextStageType && maxTries > 0)
        {
            nextStageType = PercentOfStage();
            maxTries--;
        }
        // ���� maxTries�� 0�� �������� ��쿡�� nextStageType�� stageType�� ���ٸ� ���� ó���� �߰�
        if (stageType == nextStageType)
        {
            Debug.LogWarning("Warning: Unable to find a different stage type after multiple attempts. Using the same stage type.");
        }
    }
    int PercentOfStage()//���������� Ȯ��.
    {
        int randomValue = random.Next(0, 100); // 0���� 99������ ���� ����

        if (randomValue < 15)
        {
            return 2; // 15% Ȯ���� �Ƚ�ó
        }
        else if (randomValue < 30)
        {
            return 5; // 15% Ȯ���� ������
        }
        else if (randomValue < 40)
        {
            return 6; // 10% Ȯ���� ���� ���
        }
        else if (randomValue < 65)
        {
            return 3; // 25% Ȯ���� ���� ����
        }
        else
        {
            return 4; // 35% Ȯ���� ������ ����
        }
    }
    void StartStageEvent()//���������� �̺�Ʈ ����.
    {
        //���� �������� ��Ȱ��ȭ + ���� �������� Ȱ��ȭ.
        StageList[lastStageType].gameObject.SetActive(false);
        StageList[stageType].gameObject.SetActive(true);
    }

    /////////////�������� ��ư �Լ���/////////////
    public void NextStage()//���� ���������� �̵�.
    {
        for (int i = 0; i < StageList[stageType].buttons.Count; i++)
        {
            StageList[stageType].buttons[i].interactable = false;
        }//��ư ��Ȱ��ȭ.
        RoomNum++;
        NextStageRandom();
        UpdateStage();
    }
    public void MonsterRoomNextStage()//���� �濡�� ���� ���������� �̵� �õ�.
    {
        if(stageType == 3)
        {
            StageList[3].buttons[4].interactable = false;
            StageList[3].GetComponent<BloodCave>().MonsterRoomNextStage();
        }
        else if(stageType == 4)
        {
            StageList[4].buttons[4].interactable = false;
            StageList[4].GetComponent<DampCave>().MonsterRoomNextStage();
        }
    }
    public void TryFlee()//���� �õ�.
    {
        for (int i = 0; i < StageList[stageType].buttons.Count; i++)
        {
            StageList[stageType].buttons[i].interactable = false;
        }//��ư ��Ȱ��ȭ.
        //�ӽ÷� ���� ���������� �̵� ����. ���� Ȯ���� ���� ���� or ��� ���� ���� �ֱ�.
        Debug.Log("���� (�ӽ÷� ������ ���� ����)");
        NextStage();
    }
    public void Rest()//�޽�.
    {
        Debug.Log("�޽�");
        StageList[2].GetComponent<Hideout>().buttons[0].interactable = false;
    }
    public void Search()//Ž���Ѵ�.
    {
        if(stageType == 3)
        {
            StageList[3].GetComponent<BloodCave>().Search();
            StageList[3].buttons[1].interactable = false;
        }
        else if(stageType == 4)
        {
            StageList[4].GetComponent<DampCave>().Search();
            StageList[4].buttons[1].interactable = false;
        }
    }
    public void StartCombat()//���� ����.
    {
        if(stageType == 3)
        {
            StageList[3].GetComponent<BloodCave>().StartCombat();
            StageList[3].buttons[0].interactable = false;
        }
        else if(stageType == 4)
        {
            StageList[4].GetComponent<DampCave>().StartCombat();
            StageList[4].buttons[3].interactable = false;
        }
    }
    public void EatMonsterMeat()//���� ��⸦ �Դ´�.
    {
        if(stageType == 3)
        {
            StageList[3].GetComponent<BloodCave>().EatMonsterMeat();
            StageList[3].buttons[2].interactable = false;
        }
        else if(stageType == 4)
        {
            StageList[4].GetComponent<DampCave>().EatMonsterMeat();
            StageList[4].buttons[2].interactable = false;
        }
    }
    public void GoLeft()//�����濡�� �������� �̵�.
    {
        for(int i = 0; i < StageList[5].buttons.Count; i++)
        {
            StageList[5].buttons[i].interactable = false;
        }//��ư ��Ȱ��ȭ.
        StageList[5].GetComponent<ForkedRoad>().GoLeft();
    }
    public void GoRight()//�����濡�� ���������� �̵�.
    {
        for (int i = 0; i < StageList[5].buttons.Count; i++)
        {
            StageList[5].buttons[i].interactable = false;
        }//��ư ��Ȱ��ȭ.
        StageList[5].GetComponent<ForkedRoad>().GoRight();
    }
    public void TalkToNPC()//������ ���� npc�� ��ȭ.
    {
        Debug.Log("���ΰ� ��ȭ");
    }
    public void LeaveDungeonWithP()//���� ��ǥ �޼� �� ������ ������.
    {

    }
}

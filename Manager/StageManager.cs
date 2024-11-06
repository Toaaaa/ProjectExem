using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class StageManager : MonoBehaviour
{
    int RoomNum;//���� ��ȣ
    string RoomName;//���� �̸�
    [SerializeField]
    List<Stage> StageList;
    int stageType; //0: Tutorial, 1: Starting, 2:QuietHideout,  3: BloodCave, 4: DampCave, 5: ForkedRoad, 6: CaveAlley, 7: EndPoint.
    int lastStageType;//���� ���������� Ÿ��.
    int nextStageType;//���� ���������� Ÿ��.

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
    void NextStageRandom()//������ Ȯ���� ���� ���������� ����. ����� �ӽ÷� ���� ����.
    {
        int x = random.Next(2, 7);//��������Ʈ�� 8���� �����ϰ� �׽�Ʈ.
        //���� Ȯ���� �����Ͽ� �������� ������ �缳��.
        nextStageType = x;
    }
    void StartStageEvent()//���������� �̺�Ʈ ����.
    {
        if(lastStageType == stageType)
        {
            StageList[stageType].gameObject.SetActive(false);
            StageList[stageType].gameObject.SetActive(true);
        }//������ ������ Ÿ���� ���������� ���
        else
        {
            StageList[lastStageType].gameObject.SetActive(false);
            StageList[stageType].gameObject.SetActive(true);
        }//������ �ٸ� Ÿ���� ���������� ���
    }

    /////////////�������� ��ư �Լ���/////////////
    public void NextStage()//���� ���������� �̵�.
    {
        RoomNum++;
        NextStageRandom();
        UpdateStage();
    }
    public void MonsterRoomNextStage()//���� �濡�� ���� ���������� �̵�.
    {
        //���� ���� ��� ���� óġ�Ŀ��� ������ ���������� �̵��ϳ�, óġ ������ ���� Ȯ���� ���� ������ �̵�����.
        RoomNum++;
        NextStageRandom();
        UpdateStage();
    }
    public void TryFlee()//���� �õ�.
    {

    }
    public void Rest()//�޽�.
    {

    }
    public void Search()//Ž���Ѵ�.
    {

    }
    public void StartCombat()//���� ����.
    {

    }
    public void EatMonsterMeat()//���� ��⸦ �Դ´�.
    {

    }
    public void GoLeft()//�����濡�� �������� �̵�.
    {
        StageList[5].GetComponent<ForkedRoad>().GoLeft();
    }
    public void GoRight()//�����濡�� ���������� �̵�.
    {
        StageList[5].GetComponent<ForkedRoad>().GoRight();
    }
    public void TalkToNPC()//������ ���� npc�� ��ȭ.
    {

    }
    public void LeaveDungeonWithP()//���� ��ǥ �޼� �� ������ ������.
    {

    }
}

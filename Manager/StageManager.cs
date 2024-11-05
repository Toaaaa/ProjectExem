using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class StageManager : MonoBehaviour
{
    int RoomNum;//���� ��ȣ
    string RoomName;//���� �̸�
    int stageType; //0: Tutorial, 1: Starting, 2:QuietHideout,  3: BloodCave, 4: DampCave, 5: ForkedRoad, 6: CaveAlley, 7: EndPoint.
    int nextStageType;//���� ���������� Ÿ��.

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
        StartStageEvent();//�������� �̺�Ʈ ����.
    }
    async void UpdateStage()//���� ���������� ������Ʈ (���,��ư������,�������)
    {
        //ĳ���Ͱ� ������ �ȴ� �ִϸ��̼�
        //���� �� ����� ���ƿ� �ϸ� �ڷ� ���� ����
        await UniTask.Delay(1000);
        //����� ȭ��Ʈ �ƿ� �ϸ� ���ο� ���������� ����� ������ ����.
        await UniTask.Delay(1000);
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
        int x = random.Next(2, 8);
        nextStageType = x;
    }
    void StartStageEvent()//���������� �̺�Ʈ ����.
    {

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

    }
    public void GoRight()//�����濡�� ���������� �̵�.
    {

    }
    public void TalkToNPC()//������ ���� npc�� ��ȭ.
    {

    }
    public void LeaveDungeonWithP()//���� ��ǥ �޼� �� ������ ������.
    {

    }
}

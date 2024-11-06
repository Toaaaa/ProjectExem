using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class StageManager : MonoBehaviour
{
    int RoomNum;//방의 번호
    string RoomName;//방의 이름
    [SerializeField]
    List<Stage> StageList;
    int stageType; //0: Tutorial, 1: Starting, 2:QuietHideout,  3: BloodCave, 4: DampCave, 5: ForkedRoad, 6: CaveAlley, 7: EndPoint.
    int lastStageType;//이전 스테이지의 타입.
    int nextStageType;//다음 스테이지의 타입.

    public float EndPercent;//다음 이동시 목적지일 확률.

    private System.Random random = new System.Random();//랜덤함수 시드 초기화.

    private void Start()
    {
        startStage();
    }

    private void Update()
    {
        
    }

    void startStage()//처음 던전 입장시 입구에서 시작.
    {
        RoomNum = 0;
        stageType = 1;
        EndPercent = 0;
        StartStageEvent();//스테이지의 버튼 교체 + 스테이지 전용 기믹 시작.
    }
    async void UpdateStage()//다음 스테이지로 업데이트 (배경,버튼선택지,진행사항)
    {
        //캐릭터가 앞으로 걷는 애니메이션
        //기존 뒷 배경이 블랙아웃 하며 뒤로 가는 동작
        await UniTask.Delay(1000);
        //잠시후 화이트 아웃 하며 새로운 스테이지의 배경이 나오는 동작.
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
        }//스테이지 이름 세팅.
        StartStageEvent();//스테이지 이벤트 시작.
    }
    void NextStageRandom()//일정한 확률로 다음 스테이지를 설정. 현재는 임시로 완전 랜덤.
    {
        int x = random.Next(2, 7);//엔드포인트의 8번은 제외하고 테스트.
        //추후 확률을 조정하여 정식으로 랜덤값 재설정.
        nextStageType = x;
    }
    void StartStageEvent()//스테이지의 이벤트 시작.
    {
        if(lastStageType == stageType)
        {
            StageList[stageType].gameObject.SetActive(false);
            StageList[stageType].gameObject.SetActive(true);
        }//이전과 동일한 타입의 스테이지일 경우
        else
        {
            StageList[lastStageType].gameObject.SetActive(false);
            StageList[stageType].gameObject.SetActive(true);
        }//이전과 다른 타입의 스테이지일 경우
    }

    /////////////스테이지 버튼 함수들/////////////
    public void NextStage()//다음 스테이지로 이동.
    {
        RoomNum++;
        NextStageRandom();
        UpdateStage();
    }
    public void MonsterRoomNextStage()//몬스터 방에서 다음 스테이지로 이동.
    {
        //몬스터 방의 경우 몬스터 처치후에는 무조건 다음방으로 이동하나, 처치 전에는 일정 확률로 몬스터 조우후 이동가능.
        RoomNum++;
        NextStageRandom();
        UpdateStage();
    }
    public void TryFlee()//도망 시도.
    {

    }
    public void Rest()//휴식.
    {

    }
    public void Search()//탐색한다.
    {

    }
    public void StartCombat()//전투 시작.
    {

    }
    public void EatMonsterMeat()//몬스터 고기를 먹는다.
    {

    }
    public void GoLeft()//갈림길에서 왼쪽으로 이동.
    {
        StageList[5].GetComponent<ForkedRoad>().GoLeft();
    }
    public void GoRight()//갈림길에서 오른쪽으로 이동.
    {
        StageList[5].GetComponent<ForkedRoad>().GoRight();
    }
    public void TalkToNPC()//던전의 상인 npc와 대화.
    {

    }
    public void LeaveDungeonWithP()//최종 목표 달성 후 던전을 나간다.
    {

    }
}

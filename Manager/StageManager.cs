using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine.Rendering.Universal;

public class StageManager : MonoBehaviour
{
    public BackViewManager backViewManager;
    public CharacterManager characterManager;

    public int RoomNum;//방의 번호
    string RoomName;//방의 이름
    public List<Stage> StageList;
    public int stageType; //0: Tutorial, 1: Starting, 2:QuietHideout,  3: BloodCave, 4: DampCave, 5: ForkedRoad, 6: CaveAlley, 7: EndPoint.
    int lastStageType;//이전 스테이지의 타입.
    public int nextStageType;//다음 스테이지의 타입.
    public bool duringCombat;//전투중인지 여부.
    public float EndPercent;//다음 이동시 목적지일 확률.

    public Light2D globalLight;
    private System.Random random = new System.Random();//랜덤함수 시드 초기화.

    private void Start()
    {
        startStage();
        GameManager.Instance.stageManager = this;
        GameManager.Instance.characterManager = characterManager;
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
    void UpdateStage()//다음 스테이지로 업데이트 (배경,버튼선택지,진행사항)
    {
        Debug.Log("다음 스테이지로 이동");
        backViewManager.TransitionToNextStage(async () => //async
        {
            //다음 스테이지로 이동됨.
            await UniTask.Delay(800);
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
        });
        //캐릭터가 앞으로 걷는 애니메이션
    }
    void NextStageRandom()//일정한 확률로 다음 스테이지를 설정.
    {
        if(RoomNum >= 50)
        {
            nextStageType = random.Next(0, 100) <= EndPercent ? 7 : 0; //엔드 포인트 확률 연산.
            if(nextStageType == 7)
            {
                return;
            }
        }
        int maxTries = 100; // 무한 루프 방지용 시도 횟수 제한
        nextStageType = PercentOfStage(); // 다음 스테이지 타입을 랜덤으로 설정
        // 같은 스테이지가 연속으로 나오지 않도록 반복 시도
        while (stageType == nextStageType && maxTries > 0)
        {
            nextStageType = PercentOfStage();
            maxTries--;
        }
        // 만약 maxTries가 0에 도달했을 경우에도 nextStageType이 stageType과 같다면 예외 처리를 추가
        if (stageType == nextStageType)
        {
            Debug.LogWarning("Warning: Unable to find a different stage type after multiple attempts. Using the same stage type.");
        }
    }
    int PercentOfStage()//스테이지별 확률.
    {
        int randomValue = random.Next(0, 100); // 0부터 99까지의 난수 생성

        if (randomValue < 15)
        {
            return 2; // 15% 확률로 안식처
        }
        else if (randomValue < 30)
        {
            return 5; // 15% 확률로 갈림길
        }
        else if (randomValue < 40)
        {
            return 6; // 10% 확률로 동굴 길목
        }
        else if (randomValue < 65)
        {
            return 3; // 25% 확률로 혈향 동굴
        }
        else
        {
            return 4; // 35% 확률로 축축한 동굴
        }
    }
    void StartStageEvent()//스테이지의 이벤트 시작.
    {
        //이전 스테이지 비활성화 + 다음 스테이지 활성화.
        StageList[lastStageType].gameObject.SetActive(false);
        StageList[stageType].gameObject.SetActive(true);
    }

    /////////////스테이지 버튼 함수들/////////////
    public void NextStage()//다음 스테이지로 이동.
    {
        for (int i = 0; i < StageList[stageType].buttons.Count; i++)
        {
            StageList[stageType].buttons[i].interactable = false;
        }//버튼 비활성화.
        RoomNum++;
        NextStageRandom();
        UpdateStage();
    }
    public void MonsterRoomNextStage()//몬스터 방에서 다음 스테이지로 이동 시도.
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
    public void TryFlee()//도망 시도.
    {
        for (int i = 0; i < StageList[stageType].buttons.Count; i++)
        {
            StageList[stageType].buttons[i].interactable = false;
        }//버튼 비활성화.
        //임시로 다음 스테이지로 이동 넣음. 추후 확률에 따른 도망 or 즉시 전투 진행 넣기.
        Debug.Log("도망 (임시로 무조건 도망 성공)");
        NextStage();
    }
    public void Rest()//휴식.
    {
        StageList[2].GetComponent<Hideout>().StartResting();
    }
    public void RestEnd()
    {
        StageList[2].GetComponent<Hideout>().EndResting();
    }
    public void Search()//탐색한다.
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
    public void StartCombat()//전투 시작.
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
    public void EatMonsterMeat()//몬스터 고기를 먹는다.
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
    public void GoLeft()//갈림길에서 왼쪽으로 이동.
    {
        for(int i = 0; i < StageList[5].buttons.Count; i++)
        {
            StageList[5].buttons[i].interactable = false;
        }//버튼 비활성화.
        StageList[5].GetComponent<ForkedRoad>().GoLeft();
    }
    public void GoRight()//갈림길에서 오른쪽으로 이동.
    {
        for (int i = 0; i < StageList[5].buttons.Count; i++)
        {
            StageList[5].buttons[i].interactable = false;
        }//버튼 비활성화.
        StageList[5].GetComponent<ForkedRoad>().GoRight();
    }
    public void TalkToNPC()//던전의 상인 npc와 대화.
    {
        Debug.Log("상인과 대화");
    }
    public void LeaveDungeonWithP()//최종 목표 달성 후 던전을 나간다.
    {

    }
}

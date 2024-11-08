using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DampCave : Stage
{
    public bool isMonster;//몬스터가 있는지 없는지.
    public bool monsterSpanwed;//몬스터가 등장했는지 여부 >> 전투시작
    public bool isSearch;//탐색을 했는지 여부.
    public bool isClear;//스테이지 클리어 여부.
    public float monsterEncounterRate = 60f;//몬스터 등장 확률.(처음 입장시, 탐색시, 도주시 해당 수치를 기반으로 몬스터를 등장시킴) 85%로 설정.


    public int stateCondition;//해당 스테이지 에서의 상태. 0: 처음 입장(몬스터 등장x), 1: 처음 입장(몬스터 등장o), 2:탐색후, 3:전투후 4:전투 패배.
    //public Item item;//해당 스테이지에서 탐색시 나오는 아이템.

    protected override void OnEnable()
    {
        base.OnEnable();

        isClear = false;
        isSearch = false;
        monsterSpanwed = false;
        isMonster = MonsterPossibility();
        MonsterEncounterRate();//몬스터의 존재 확인 + 몬스터
    }
    private void Update()
    {
        if(GameManager.Instance.stageManager.duringCombat)//전투중일 때
        {
            stateCondition = 4;//전투 도중
        }
        else
        {
            if (isClear)//전투가 있었고, 승리했을 때
            {
                stateCondition = 3;//전투 후
                                   //2(위),3
            }
            else//전투를 하기 전.
            {
                if (isMonster)//몬스터가 있는 방
                {
                    if (monsterSpanwed)//몬스터가 등장했을 때
                    {
                        stateCondition = 1;//처음 입장(몬스터 등장o)
                                           //1(위),3
                    }
                    else//몬스터가 등장하지 않았을 때
                    {
                        if (isSearch)//탐색을 했을 때
                        {
                            stateCondition = 2;//탐색 후
                        }
                        else//탐색을 하지 않았을 때
                        {
                            stateCondition = 0;//처음 입장(몬스터 등장x)
                        }
                    }
                }
                else//몬스터가 없는 방
                {
                    stateCondition = 0;//처음 입장(몬스터 등장x)
                }
            }
        }

        switch (stateCondition)//버튼의 활성화 여부 결정.
        {
            case 0://처음 입장(몬스터 등장x)
                buttons[0].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(false);
                buttons[4].gameObject.SetActive(true);
                //탐색 + 다음으로 이동
                break;
            case 1://처음 입장시(몬스터 등장o)
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(true);
                buttons[4].gameObject.SetActive(false);
                //전투 + 도망
                break;
            case 2://탐색 후
                if (isClear)
                {
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[2].gameObject.SetActive(true);
                    buttons[3].gameObject.SetActive(false);
                    buttons[4].gameObject.SetActive(true);
                    //몬스터고기 + 다음으로 이동
                }
                else
                {
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(true);
                    buttons[2].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                    buttons[4].gameObject.SetActive(true);
                    //탐색(회색) + 다음으로 이동
                }
                break;
            case 3://전투 후               
                if (isSearch)
                {
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[2].gameObject.SetActive(true);
                    buttons[3].gameObject.SetActive(false);
                    buttons[4].gameObject.SetActive(true);
                    //몬스터고기 + 다음으로 이동
                }
                else
                {
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(true);
                    buttons[2].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                    buttons[4].gameObject.SetActive(true);
                    //탐색 + 다음으로 이동
                }
                break;
            case 4://전투 도중
                buttons[0].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(false);
                buttons[4].gameObject.SetActive(false);
                break;
            case 5://전투 패배
                break;
            default:
                break;
        }//버튼의 활성화 여부 결정.
        if (Input.GetKeyDown(KeyCode.Space))//임시로 전투 승리 조건 달성.
        {
            CombatWin();
        }
    }

    bool MonsterPossibility()//몬스터와 조우
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        float a = 35 + GameManager.Instance.stageManager.RoomNum * 0.5f - 15;//스테이지가 올라갈수록 몬스터 등장 확률이 올라감.(기본 35퍼센트 - 축축동굴 15퍼센트 감소)
        if (Random.Range(0, 100) <= a ? true : false)
        {
            return true;
        }
        return false;
    }

    void MonsterEncounterRate()
    {
        if (isClear)
        {
            return;
        }//이미 모두 처치를 했다면 무시.

        if (isMonster)//몬스터가 있는 방
        {
            if (Random.Range(0, 100) <= monsterEncounterRate ? true : false)//monsterEncounterRate의 확률로 몬스터 등장.
            {
                SpawnMonster();
            }
            else
            {
                //몬스터가 나오지 않음.
            }
        }
        else
        {
            //몬스터가 없는 방.
        }
    }
    void SpawnMonster()
    {
        monsterSpanwed = true;
        Debug.Log("몬스터 등장");
    }
    public void StartCombat()//전투 시작.
    {
        GameManager.Instance.stageManager.duringCombat = true;
        Debug.Log("전투 시작");
        //전투 시작.
    }
    public void CombatWin()//전투 승리.
    {
        GameManager.Instance.stageManager.duringCombat = false;
        isClear = true;
    }
    public void EatMonsterMeat()
    {
        Debug.Log("몬스터 고기를 먹는다.");
    }
    public async void Search()
    {
        isSearch = true;
        await UniTask.Delay(500);
        //탐색중의 애니메이션 재생.

        //찾는 모션 >> 1.몬스터 조우, 2 아이템 발견 ui
        if (isMonster)//몬스터가 있는 방
        {
            if (Random.Range(0, 100) <= monsterEncounterRate ? true : false)//몬스터 조우 
            {
                buttons[1].interactable = true;//탐색 도중 조우시 탐색 버튼 재활성화.
                SpawnMonster();
            }
            else
            {
                Debug.Log("아이템 발견");
            }
        }
        else
        {
            Debug.Log("아이템 발견");
        }
    }
    public void MonsterRoomNextStage()
    {
        if (isMonster)//몬스터가 있는 방
        {
            if (isClear)//이미 몬스터를 처치함
            {
                GameManager.Instance.stageManager.NextStage();
            }
            else
            {
                if (Random.Range(0, 100) <= monsterEncounterRate ? true : false)//monsterEncounterRate의 확률로 몬스터 등장.
                {
                    buttons[4].interactable = true;
                    SpawnMonster();
                }
                else//몬스터가 등장 하지 않음.
                {
                    GameManager.Instance.stageManager.NextStage();
                }
            }
        }
        else//몬스터가 없는 방
        {
            GameManager.Instance.stageManager.NextStage();
        }
    }
}

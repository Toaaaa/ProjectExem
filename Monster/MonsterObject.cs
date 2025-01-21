using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObject : MonoBehaviour
{
    //체력바
    //애니메이션
    //버프 디버프 표시
    public MonsterData monsterData;
    public string monsterName;
    public int MonsterID;
    public int health;
    public float attackPower;
    public float defense;
    public bool isElite; //엘리트 몬스터인지

    public bool combatWaiting = true;//전투 시작 대기상태인지. (몬스터의 조우만 하고 실제로 전투가 시작되기 전 상태)



    private void OnEnable()
    {
        //몬스터가 생성될때의 필요한 요소 초기화. (애니메이션, 체력바, 디버프 요소등..)
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.enabled = true;//몬스터 hp0시 죽음 판정후 false 상태의 boxcollider = true;
    }
    private void OnDisable()
    {
        //몬스터가 죽었을때 or 도망시의 처리.
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.enabled = false;
        isElite = false;
        combatWaiting = true;//전투 대기 상태로 변경.
    }

    private void Update()
    {
        if (health <= 0)
        {
            MonsterDie();
        }
        if(!combatWaiting) //전투 대기 상태가 아닐때 모든 행동을 수행.
        {
            
        }
    }

    
    public void SetMonsterData(MonsterData data)//해당 몬스터의 prefab이 생성될때 호출되어 몬스터의 데이터를 설정한다.
    {
        int x = GameManager.Instance.stageManager.RoomNum/10; 

        monsterData = data;
        monsterName = data.monsterName;
        MonsterID = data.MonsterID;
        //health = data.health;
        //attackPower = data.attackPower;
        defense = (5*x + 5 + data.extraDefense); //5x+5+추가 방어력 == 총 방어력. (x는 10층 단위로 증가)
    }
    void MonsterDie()
    {
        Debug.Log("Monster Die");
        gameObject.SetActive(false);
    }
}

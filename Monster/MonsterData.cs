using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Monster System/Monster Data")]
public class MonsterData : ScriptableObject
{
    public string monsterName;
    public int MonsterID;
    public int healthValue;//몬스터의 체력 계수. 해당계수의 각 비율만큼 HP총합 보정치를 나눠서 배치.(기본이 1,몬스터의 등급에 따라 1~3정도로 설정)
    //프리셋의 expectedSkillValue총합이 설계된 dps총합과 같아지는 배율을 구해, 해당값을 몬스터의 attackPower에 넣어준다.
    public float expectedDPS; //공격력 1을 기준으로 예상되는 dps기반 스킬 계수. dps총합을 계산할때 활용.
    public int extraDefense;//기본 방어력 공식인 5x+5 에 추가되는 방어력. (몬스터의 종류 및 등급에 따라 -5 ~ +10정도로 설정)


}

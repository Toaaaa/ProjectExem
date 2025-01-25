using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillData : ScriptableObject
{
    public string skillName;
    public float damage;
    public float cost;//스킬 사용시 소모되는 코스트 (시간 기반 재화로 사용 해서 dps와 연동하여 계산에 용이하게 설계)

    public virtual void UseSkill()//스킬을 사용하는 함수로, 해당 MonsterSkillData를 상속받는 스크립트에서 구현.
    {

    }
}


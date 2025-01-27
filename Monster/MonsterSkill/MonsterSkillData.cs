using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillData : ScriptableObject
{
    public string skillName;
    public float damage;
    public float cost;//스킬 사용시 소모되는 코스트 (시간 기반 재화로 사용 해서 dps와 연동하여 계산에 용이하게 설계)

    public virtual void UseSkill(float atkPower)//스킬을 사용하는 함수로, 해당 MonsterSkillData를 상속받는 스크립트에서 구현.
    {
        //몬스터의 attackPower * damage만큼의 데미지를 출력하고, 이는 캐릭터의 defense수치에 따라 실제 데미지가 결정된다.
    }

    virtual public CharacterData TargetSkill(int i)//스킬을 사용할 대상을 정하는 함수.
    {
        if(i == 0)
        {
            return GameManager.Instance.characterManager.Joanna;//조안나를 타겟
        }
        else
        {
            return GameManager.Instance.characterManager.Rei;//레이를 타겟
        }
        //단순히 타겟을 return하는 함수일뿐, 실제로 타겟을 정하는건 UseSkill함수에서 상세 구현 필요.
    }
}


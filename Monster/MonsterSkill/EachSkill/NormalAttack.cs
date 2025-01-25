using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NormalAttack", menuName = "Monster Skill/Normal Attack")]
public class NormalAttack : MonsterSkillData
{
    public override void UseSkill()
    {
        //일반 공격 스킬로, 몬스터의 기본 공격력만큼의 데미지를 입힌다.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bite", menuName = "Monster Skill/Bite")]
public class Bite : MonsterSkillData
{
    public override void UseSkill()
    {
        //물어뜯기 스킬로, 몬스터의 기본 공격력의 1.5배만큼의 데미지를 입힌다.
    }
}

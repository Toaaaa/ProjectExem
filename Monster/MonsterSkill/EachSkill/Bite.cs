using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bite", menuName = "Monster Skill/Bite")]
public class Bite : MonsterSkillData //몸통 박치기로 교체함.
{
    public override void UseSkill(float atkPower,GameObject skillFx)
    {
        base.UseSkill(atkPower,skillFx);
        //물어뜯기 스킬로, 몬스터의 기본 공격력의 1.5배만큼의 데미지를 입힌다.
        Debug.Log("Bite Skill Used");
    }
}

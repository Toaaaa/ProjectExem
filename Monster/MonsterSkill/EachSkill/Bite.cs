using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bite", menuName = "Monster Skill/Bite")]
public class Bite : MonsterSkillData
{
    public override void UseSkill(float atkPower,GameObject skillFx)
    {
        base.UseSkill(atkPower,skillFx);
        //������ ��ų��, ������ �⺻ ���ݷ��� 1.5�踸ŭ�� �������� ������.
        Debug.Log("Bite Skill Used");
    }
}

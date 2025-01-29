using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NormalAttack", menuName = "Monster Skill/Normal Attack")]
public class NormalAttack : MonsterSkillData
{
    public override void UseSkill(float atkPower, GameObject skillFx)
    {
        base.UseSkill(atkPower, skillFx);
        //�Ϲ� ���� ��ų��, ������ �⺻ ���ݷ¸�ŭ�� �������� ������.
        Debug.Log("Normal Attack Skill Used");
    }
}

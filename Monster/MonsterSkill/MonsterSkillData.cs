using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillData : ScriptableObject
{
    public string skillName;
    public float damage;
    public float cost;//��ų ���� �Ҹ�Ǵ� �ڽ�Ʈ (�ð� ��� ��ȭ�� ��� �ؼ� dps�� �����Ͽ� ��꿡 �����ϰ� ����)

    public virtual void UseSkill()//��ų�� ����ϴ� �Լ���, �ش� MonsterSkillData�� ��ӹ޴� ��ũ��Ʈ���� ����.
    {

    }
}


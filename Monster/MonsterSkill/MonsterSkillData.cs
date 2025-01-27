using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillData : ScriptableObject
{
    public string skillName;
    public float damage;
    public float cost;//��ų ���� �Ҹ�Ǵ� �ڽ�Ʈ (�ð� ��� ��ȭ�� ��� �ؼ� dps�� �����Ͽ� ��꿡 �����ϰ� ����)

    public virtual void UseSkill(float atkPower)//��ų�� ����ϴ� �Լ���, �ش� MonsterSkillData�� ��ӹ޴� ��ũ��Ʈ���� ����.
    {
        //������ attackPower * damage��ŭ�� �������� ����ϰ�, �̴� ĳ������ defense��ġ�� ���� ���� �������� �����ȴ�.
    }

    virtual public CharacterData TargetSkill(int i)//��ų�� ����� ����� ���ϴ� �Լ�.
    {
        if(i == 0)
        {
            return GameManager.Instance.characterManager.Joanna;//���ȳ��� Ÿ��
        }
        else
        {
            return GameManager.Instance.characterManager.Rei;//���̸� Ÿ��
        }
        //�ܼ��� Ÿ���� return�ϴ� �Լ��ϻ�, ������ Ÿ���� ���ϴ°� UseSkill�Լ����� �� ���� �ʿ�.
    }
}


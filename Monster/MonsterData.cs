using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Monster System/Monster Data")]
public class MonsterData : ScriptableObject
{
    public RuntimeAnimatorController animCon;//������ ��Ʈ�ѷ�
    public string monsterName;
    public int MonsterID;
    public int healthValue;//������ ü�� ���. �ش����� �� ������ŭ HP���� ����ġ�� ������ ��ġ.(�⺻�� 1,������ ��޿� ���� 1~3������ ����)
    //�������� expectedSkillValue������ ����� dps���հ� �������� ������ ����, �ش簪�� ������ attackPower�� �־��ش�.
    public float expectedDPS; //���ݷ� 1�� �������� ����Ǵ� dps��� ��ų ���. dps������ ����Ҷ� Ȱ��.// ���� ����ؼ� �־������.
    public int extraDefense;//�⺻ ���� ������ 5x+5 �� �߰��Ǵ� ����. (������ ���� �� ��޿� ���� -5 ~ +10������ ����)

    public List<MonsterSkillList> monsterSkillList;

    [Serializable]
    public class MonsterSkillList
    {
        public MonsterSkillData monsterSkillData;
        [Range(0, 100)] public float skillChance;//��ų�� �ߵ��� Ȯ�� (0~100)
    }
}

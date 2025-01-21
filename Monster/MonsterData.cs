using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Monster System/Monster Data")]
public class MonsterData : ScriptableObject
{
    public string monsterName;
    public int MonsterID;
    public int healthValue;//������ ü�� ���. �ش����� �� ������ŭ HP���� ����ġ�� ������ ��ġ.(�⺻�� 1,������ ��޿� ���� 1~3������ ����)
    //�������� expectedSkillValue������ ����� dps���հ� �������� ������ ����, �ش簪�� ������ attackPower�� �־��ش�.
    public float expectedDPS; //���ݷ� 1�� �������� ����Ǵ� dps��� ��ų ���. dps������ ����Ҷ� Ȱ��.
    public int extraDefense;//�⺻ ���� ������ 5x+5 �� �߰��Ǵ� ����. (������ ���� �� ��޿� ���� -5 ~ +10������ ����)


}

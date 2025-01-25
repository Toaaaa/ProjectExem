using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterPreset", menuName = "Monster System/Monster Presets")]
public class MonsterPreset : ScriptableObject
{
    public List<Presets> presets;

    [Serializable]
    public class Presets
    {
        public List<MonsterData> monstersData; //�ش� �������� ���͵�
        [Range(0, 100)] public float spawnChance; // Ȯ�� (0~100)
        public bool isEliteCombat; //����Ʈ �������� ����(�� ü�� ���), ����Ʈ ������ ���������� �ƴ����� ������ �Ϲ� ���� �����ϴ� ����.
    }

    public void SetPresetMob(int i)
    {
        MonsterManager monsterManager = GameManager.Instance.stageManager.monsterManager;
        int x = GameManager.Instance.stageManager.RoomNum / 10;
        float totalDPS1 = ((float)30 / 74) * (40 + 10 * x) * (1f / 60 + 6 * x); //������ attackPower�� ����ϱ� ���� �Ű����� ��Ȱ.(�������� ������ �ش� �������� ������ �ϴ� �� ��������)
        float totalDPS2 = ((float)45 / 74) * (40 + 10 * x) * (1f / 60 + 6 * x);
        float healthValue;

        //���� 1�����϶� v2��ġ��, 2�����϶� v1,v2��ġ, 3�����϶� v1,v2,v3��ġ�� ���� �����Ѵ�.(v123�� monstermanager�� monsterpos123)
        if (presets[i].monstersData.Count == 1)//�ϳ��� �Ϲ������� ������.
        {
            monsterManager.monsterObject2.SetMonsterData(presets[i].monstersData[0]);
            SetExpectedDPS(i);// �ش��ϴ� Preset�� ��� ������ expectedDPS�� ����Ͽ� ��������.
            monsterManager.InitExDPS();//�� ������ expectedDPS�� ���� ���� ��ü�� expectedDPS�� ����.

            //���� ������Ʈ�� health ���
            monsterManager.monsterObject2.maxHealth = (int)(presets[i].monstersData[0].healthValue  * (80 + 40 * x));//�������� ���� �ٷ� ����� �����ش�.
            //���� ������Ʈ�� attackPower ���

            //���� ������Ʈ Ȱ��ȭ
            monsterManager.monsterObject2.gameObject.SetActive(true);
        }
        else if (presets[i].monstersData.Count == 2)
        {
            monsterManager.monsterObject1.SetMonsterData(presets[i].monstersData[0]);
            monsterManager.monsterObject2.SetMonsterData(presets[i].monstersData[1]);
            SetExpectedDPS(i);// �ش��ϴ� Preset�� ��� ������ expectedDPS�� ����Ͽ� ��������.
            monsterManager.InitExDPS();//�� ������ expectedDPS�� ���� ���� ��ü�� expectedDPS�� ����.
            //���� ������Ʈ�� health ���
            healthValue = (presets[i].monstersData[0].healthValue + presets[i].monstersData[1].healthValue);
            monsterManager.monsterObject1.maxHealth = (int)((presets[i].monstersData[0].healthValue / healthValue) * (80 + 40 * x));
            monsterManager.monsterObject2.maxHealth = (int)((presets[i].monstersData[1].healthValue / healthValue) * (80 + 40 * x));

            //������ dps������ 1~29���� 30�� �̻��� �ٸ�
            if (GameManager.Instance.stageManager.RoomNum < 30)
            {
                //((double)30 / 74) * (40 + 10 * x) / (60 + 6*x) �� ���ϴ� dps���̰�, ���͵��� expectedDPS�� ��հ����� ������ �ش� ���� monsterObject�� attackPower�� �־��ش�.
                totalDPS1 /= (presets[i].monstersData[0].expectedDPS + presets[i].monstersData[1].expectedDPS); //totalDPS1�� monsterObject�� attackPower�� �ȴ�. (������ ���� attackPower * ��ų�� damage �� �ش�)
                monsterManager.monsterObject1.attackPower = totalDPS1;
                monsterManager.monsterObject2.attackPower = totalDPS1;
            }
            else
            {
                //((double)45 / 74) * (40 + 10 * x) �� ���ϴ� dps���̰�, ���͵��� expectedDPS�� �� ������ ������ �ش� ���� monsterObject�� attackPower�� �־��ش�.
                totalDPS2 /= (presets[i].monstersData[0].expectedDPS + presets[i].monstersData[1].expectedDPS); //totalDPS2�� monsterObject�� attackPower�� �ȴ�.
                monsterManager.monsterObject1.attackPower = totalDPS2;
                monsterManager.monsterObject2.attackPower = totalDPS2;
            }

            if (presets[i].isEliteCombat)
            {
                monsterManager.monsterObject1.isElite = true;
                monsterManager.monsterObject2.isElite = true;
                monsterManager.monsterObject1.health = (int)(monsterManager.monsterObject1.health * 1.5);
                monsterManager.monsterObject2.health = (int)(monsterManager.monsterObject2.health * 1.5);
            }//����Ʈ ���� ü�� 1.5�� ����.
            //���� ������Ʈ Ȱ��ȭ
            monsterManager.monsterObject1.gameObject.SetActive(true);
            monsterManager.monsterObject2.gameObject.SetActive(true);
        }
        else if (presets[i].monstersData.Count == 3)
        {
            monsterManager.monsterObject1.SetMonsterData(presets[i].monstersData[0]);
            monsterManager.monsterObject2.SetMonsterData(presets[i].monstersData[1]);
            monsterManager.monsterObject3.SetMonsterData(presets[i].monstersData[2]);
            SetExpectedDPS(i);// �ش��ϴ� Preset�� ��� ������ expectedDPS�� ����Ͽ� ��������.
            monsterManager.InitExDPS();//�� ������ expectedDPS�� ���� ���� ��ü�� expectedDPS�� ����.
            //���� ������Ʈ�� health ���
            healthValue = (presets[i].monstersData[0].healthValue + presets[i].monstersData[1].healthValue + presets[i].monstersData[2].healthValue);
            monsterManager.monsterObject1.maxHealth = (int)((presets[i].monstersData[0].healthValue / healthValue) * (80 + 40 * x));
            monsterManager.monsterObject2.maxHealth = (int)((presets[i].monstersData[1].healthValue / healthValue) * (80 + 40 * x));
            monsterManager.monsterObject3.maxHealth = (int)((presets[i].monstersData[2].healthValue / healthValue) * (80 + 40 * x));

            //���� ������Ʈ�� attackPower ��� (������ dps������ 1~29���� 30�� �̻��� �ٸ�)
            if (GameManager.Instance.stageManager.RoomNum < 30)
            {
                totalDPS1 /= (presets[i].monstersData[0].expectedDPS + presets[i].monstersData[1].expectedDPS + presets[i].monstersData[2].expectedDPS); //totalDPS1�� monsterObject�� attackPower�� �ȴ�.
                monsterManager.monsterObject1.attackPower = totalDPS1;
                monsterManager.monsterObject2.attackPower = totalDPS1;
                monsterManager.monsterObject3.attackPower = totalDPS1;
            }
            else
            {
                totalDPS2 /= (presets[i].monstersData[0].expectedDPS + presets[i].monstersData[1].expectedDPS + presets[i].monstersData[2].expectedDPS); //totalDPS2�� monsterObject�� attackPower�� �ȴ�.
                monsterManager.monsterObject1.attackPower = totalDPS2;
                monsterManager.monsterObject2.attackPower = totalDPS2;
                monsterManager.monsterObject3.attackPower = totalDPS2;
            }

            if (presets[i].isEliteCombat)
            {
                monsterManager.monsterObject1.isElite = true;
                monsterManager.monsterObject2.isElite = true;
                monsterManager.monsterObject3.isElite = true;
                monsterManager.monsterObject1.health = (int)(monsterManager.monsterObject1.health * 1.5);
                monsterManager.monsterObject2.health = (int)(monsterManager.monsterObject2.health * 1.5);
                monsterManager.monsterObject3.health = (int)(monsterManager.monsterObject3.health * 1.5);
            }//����Ʈ ���� ü�� 1.5�� ����.
            //���� ������Ʈ Ȱ��ȭ
            monsterManager.monsterObject1.gameObject.SetActive(true);
            monsterManager.monsterObject2.gameObject.SetActive(true);
            monsterManager.monsterObject3.gameObject.SetActive(true);
        }
    }

    void SetExpectedDPS(int num)
    {
        for(int j = 0; presets[num].monstersData.Count > j; j++)
        {
            presets[num].monstersData[j].expectedDPS = 0;//�ʱ�ȭ

            for(int k =0; presets[num].monstersData[j].monsterSkillList.Count >k; k++)//�� ���Ͱ� ������ �ִ� ���� ��ų���� ���� dps�� ���.
            {
                presets[num].monstersData[j].expectedDPS += 
                    (presets[num].monstersData[j].monsterSkillList[k].monsterSkillData.damage 
                    / presets[num].monstersData[j].monsterSkillList[k].monsterSkillData.cost)   * (presets[num].monstersData[j].monsterSkillList[k].skillChance / 100); //��ų�� (������ / �ڽ�Ʈ) * ��ųȮ�� �� ���� dps ���.
            }

        }
    }
}

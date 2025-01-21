using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [Header("Preset 1~29")]
    public MonsterPreset preset1_29;
    [Header("Preset 30~49")]
    public MonsterPreset preset30_49;
    [Header("Preset 50~69")]
    public MonsterPreset preset50_69;
    [Header("Preset 70~94")]
    public MonsterPreset preset70_94;
    [Header("SpecialStage")]
    public MonsterPreset specialStage;
    [Header("BossStages")]
    public MonsterPreset bossStages;

    //���� ������Ʈ�� ��� ������Ʈ Ǯ������ �����Ѵ�.
    public MonsterObject monsterObject1;//���� ����
    public MonsterObject monsterObject2;//�߰� ����
    public MonsterObject monsterObject3;//�Ĺ� ����



    public void SpawnMonster(int stageNum)
    {
        MonsterPreset preset = null;
        if (stageNum < 30)
        {
            preset = preset1_29;
        }
        else if (stageNum < 50)
        {
            preset = preset30_49;
        }
        else if (stageNum < 70)
        {
            preset = preset50_69;
        }
        else if (stageNum < 95)
        {
            preset = preset70_94;
        }
        else if (stageNum == 95)
        {
            preset = specialStage;
        }
        else if (stageNum == 100)
        {
            preset = bossStages;
            //���� ���� ���� ����ÿ��� ������� ������ ���۵ǵ��� (���� �Ұ���,���� ��ư ��Ȱ��ȭ) �ϱ�.
        }

        if (preset == null)
        {
            Debug.LogError("No preset found for stage " + stageNum);
            return;
        }

        float random = Random.Range(0, 100);
        float sum = 0;
        int index = 0;
        foreach (var p in preset.presets)
        {
            sum += p.spawnChance;
            if (random < sum)
            {
                preset.SetPresetMob(index);//���� Ȯ���� ������ ���͸� �����Ѵ�.
                return;
            }
            index++;
        }
    }
    
    public void StartCombat()//CombatWaiting ������ ���� ������Ʈ�� ���� ���·� ����.
    {
        if(monsterObject1.gameObject.activeSelf)
        {
            monsterObject1.combatWaiting = false;
        }
        if (monsterObject2.gameObject.activeSelf)
        {
            monsterObject2.combatWaiting = false;
        }
        if (monsterObject3.gameObject.activeSelf)
        {
            monsterObject3.combatWaiting = false;
        }

    }

    public void FleeCombat()//������ ���� ���� ��� ����.
    {
        monsterObject1.gameObject.SetActive(false);
        monsterObject2.gameObject.SetActive(false);
        monsterObject3.gameObject.SetActive(false);
    }
}

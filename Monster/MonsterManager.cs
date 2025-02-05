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

    //몬스터 오브젝트의 경우 오브젝트 풀링으로 관리한다.
    public MonsterObject monsterObject1;//전방 몬스터
    public MonsterObject monsterObject2;//중간 몬스터
    public MonsterObject monsterObject3;//후방 몬스터

    public float initExDPS;//몬스터의 예상 dps의 합.
    float currentExDPS;
    bool isDuringCombat;//전투 중인지 여부.

    private void Update()
    {
        if (isDuringCombat)//전투 중일경우 사용.
        {




            CheckEndCombat();
        }
    }


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
            //추후 보스 전투 입장시에는 조우즉시 전투가 시작되도록 (도망 불가능,각종 버튼 비활성화) 하기.
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
                preset.SetPresetMob(index);//대항 확률의 프리셋 몬스터를 생성한다.
                return;
            }
            index++;
        }
    }
    
    public void StartCombat()//CombatWaiting 상태의 몬스터 오브젝트를 전투 상태로 변경.
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
        isDuringCombat = true;
    }
    void CheckEndCombat()
    {
        if (!monsterObject1.gameObject.activeSelf && !monsterObject2.gameObject.activeSelf && !monsterObject3.gameObject.activeSelf)
        {
            isDuringCombat = false;
            //전투 종료 처리. (보상, 다음 스테이지 버튼 등장 등..)
        }
    }
    public void InitExDPS()
    {
        float dpssum = 0;
        if(monsterObject1.monsterData != null)
        {
            dpssum += monsterObject1.monsterData.expectedDPS;
        }
        if (monsterObject2.monsterData != null)
        {
            dpssum += monsterObject2.monsterData.expectedDPS;
        }
        if (monsterObject3.monsterData != null)
        {
            dpssum += monsterObject3.monsterData.expectedDPS;
        }
        initExDPS = dpssum;
    }
    public void EnergyGenSet()
    {
        if (monsterObject1.isAlive)
        {
            currentExDPS += monsterObject1.monsterData.expectedDPS;
        }
        if (monsterObject2.isAlive)
        {
            currentExDPS += monsterObject2.monsterData.expectedDPS;
        }
        if (monsterObject3.isAlive)
        {
            currentExDPS += monsterObject3.monsterData.expectedDPS;
        }
        float energyG = currentExDPS / initExDPS;
        //몬스터의 예상 dps의 합을 기준으로 현재 dps의 비율을 구해 에너지 생성량을 조정한다. (이를 통해 몬스터는 전투 내내 일정한 dps를 유지할 수 있게 된다.)
        if(monsterObject1.isAlive)
        {
            monsterObject1.generateEnergeyPer = energyG;
        }
        if(monsterObject2.isAlive)
        {
            monsterObject2.generateEnergeyPer = energyG;
        }
        if(monsterObject3.isAlive)
        {
            monsterObject3.generateEnergeyPer = energyG;
        }

    }

    public void FleeCombat()//도망을 통해 전투 즉시 종료.
    {
        monsterObject1.gameObject.SetActive(false);
        monsterObject2.gameObject.SetActive(false);
        monsterObject3.gameObject.SetActive(false);
    }
}

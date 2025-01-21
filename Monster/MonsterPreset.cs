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
        public List<MonsterData> monstersData; //해당 프리셋의 몬스터들
        [Range(0, 100)] public float spawnChance; // 확률 (0~100)
        public bool isEliteCombat; //엘리트 전투인지 여부(몹 체력 상승), 엘리트 전투는 보스전투는 아니지만 강력한 일반 몹이 등장하는 전투.
    }

    public void SetPresetMob(int i)
    {
        MonsterManager monsterManager = GameManager.Instance.stageManager.monsterManager;
        int x = GameManager.Instance.stageManager.RoomNum / 10;
        float totalDPS1 = ((float)30 / 74) * (40 + 10 * x);
        float totalDPS2 = ((float)45 / 74) * (40 + 10 * x);
        float healthValue;

        //몹이 1마리일때 v2위치만, 2마리일때 v1,v2위치, 3마리일때 v1,v2,v3위치에 몹을 생성한다.(v123는 monstermanager의 monsterpos123)
        if (presets[i].monstersData.Count == 1)//하나면 일반적으로 보스몹.
        {
            monsterManager.monsterObject2.SetMonsterData(presets[i].monstersData[0]);

            //몬스터 오브젝트의 health 계산
            monsterManager.monsterObject2.health = (int)(presets[i].monstersData[0].healthValue  * (80 + 40 * x));//보스몹은 값에 바로 계수를 곱해준다.
            //몬스터 오브젝트의 attackPower 계산

            //몬스터 오브젝트 활성화
            monsterManager.monsterObject2.gameObject.SetActive(true);
        }
        else if (presets[i].monstersData.Count == 2)
        {
            monsterManager.monsterObject1.SetMonsterData(presets[i].monstersData[0]);
            monsterManager.monsterObject2.SetMonsterData(presets[i].monstersData[1]);
            //몬스터 오브젝트의 health 계산
            healthValue = (presets[i].monstersData[0].healthValue + presets[i].monstersData[1].healthValue);
            monsterManager.monsterObject1.health = (int)((presets[i].monstersData[0].healthValue / healthValue) * (80 + 40 * x));
            monsterManager.monsterObject2.health = (int)((presets[i].monstersData[1].healthValue / healthValue) * (80 + 40 * x));

            //몬스터의 dps공식은 1~29층과 30층 이상이 다름
            if (GameManager.Instance.stageManager.RoomNum < 30)
            {
                //((double)30 / 74) * (40 + 10 * x) 이 원하는 dps값이고, 몬스터들의 expectedDPS의 총 합으로 나눠서 해당 값을 monsterObject의 attackPower로 넣어준다.
                totalDPS1 /= (presets[i].monstersData[0].expectedDPS + presets[i].monstersData[1].expectedDPS); //totalDPS1이 monsterObject의 attackPower가 된다.
                monsterManager.monsterObject1.attackPower = totalDPS1;
                monsterManager.monsterObject2.attackPower = totalDPS1;
            }
            else
            {
                //((double)45 / 74) * (40 + 10 * x) 이 원하는 dps값이고, 몬스터들의 expectedDPS의 총 합으로 나눠서 해당 값을 monsterObject의 attackPower로 넣어준다.
                totalDPS2 /= (presets[i].monstersData[0].expectedDPS + presets[i].monstersData[1].expectedDPS); //totalDPS2이 monsterObject의 attackPower가 된다.
                monsterManager.monsterObject1.attackPower = totalDPS2;
                monsterManager.monsterObject2.attackPower = totalDPS2;
            }

            if (presets[i].isEliteCombat)
            {
                monsterManager.monsterObject1.isElite = true;
                monsterManager.monsterObject2.isElite = true;
                monsterManager.monsterObject1.health = (int)(monsterManager.monsterObject1.health * 1.5);
                monsterManager.monsterObject2.health = (int)(monsterManager.monsterObject2.health * 1.5);
            }//엘리트 전투 체력 1.5배 보정.
            //몬스터 오브젝트 활성화
            monsterManager.monsterObject1.gameObject.SetActive(true);
            monsterManager.monsterObject2.gameObject.SetActive(true);
        }
        else if (presets[i].monstersData.Count == 3)
        {
            monsterManager.monsterObject1.SetMonsterData(presets[i].monstersData[0]);
            monsterManager.monsterObject2.SetMonsterData(presets[i].monstersData[1]);
            monsterManager.monsterObject3.SetMonsterData(presets[i].monstersData[2]);
            //몬스터 오브젝트의 health 계산
            healthValue = (presets[i].monstersData[0].healthValue + presets[i].monstersData[1].healthValue + presets[i].monstersData[2].healthValue);
            monsterManager.monsterObject1.health = (int)((presets[i].monstersData[0].healthValue / healthValue) * (80 + 40 * x));
            monsterManager.monsterObject2.health = (int)((presets[i].monstersData[1].healthValue / healthValue) * (80 + 40 * x));
            monsterManager.monsterObject3.health = (int)((presets[i].monstersData[2].healthValue / healthValue) * (80 + 40 * x));

            //몬스터 오브젝트의 attackPower 계산 (몬스터의 dps공식은 1~29층과 30층 이상이 다름)
            if (GameManager.Instance.stageManager.RoomNum < 30)
            {
                totalDPS1 /= (presets[i].monstersData[0].expectedDPS + presets[i].monstersData[1].expectedDPS + presets[i].monstersData[2].expectedDPS); //totalDPS1이 monsterObject의 attackPower가 된다.
                monsterManager.monsterObject1.attackPower = totalDPS1;
                monsterManager.monsterObject2.attackPower = totalDPS1;
                monsterManager.monsterObject3.attackPower = totalDPS1;
            }
            else
            {
                totalDPS2 /= (presets[i].monstersData[0].expectedDPS + presets[i].monstersData[1].expectedDPS + presets[i].monstersData[2].expectedDPS); //totalDPS2이 monsterObject의 attackPower가 된다.
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
            }//엘리트 전투 체력 1.5배 보정.
            //몬스터 오브젝트 활성화
            monsterManager.monsterObject1.gameObject.SetActive(true);
            monsterManager.monsterObject2.gameObject.SetActive(true);
            monsterManager.monsterObject3.gameObject.SetActive(true);
        }
    }
}

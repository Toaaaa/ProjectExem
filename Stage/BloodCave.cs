using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BloodCave : Stage //stage 스크립트 상속
{
    public bool isMonster;//몬스터가 있는지 없는지.
    public bool isClear;//스테이지 클리어 여부.
    //public Item item;//해당 스테이지에서 탐색시 나오는 아이템.

    private void OnEnable()
    {
        isClear = false;
        isMonster = Random.Range(0, 100) >= 5 ? true : false;//몬스터가 95%확률로 나옴.

    }
}

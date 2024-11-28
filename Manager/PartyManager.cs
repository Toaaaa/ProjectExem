using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    //파티의 각종 자원 및 정보 관리//
    [SerializeField] private int bagpackSize = 20;//초기 가방 크기 = 20
    [SerializeField] private int gold = 0;//소지금
    
    //창고(보유 아이템) 데이터 추후 추가.

    public int BagpackSize { get { return bagpackSize; } }
    public int Gold { get { return gold; } }


    void Start()
    {
        GameManager.Instance.partyManager = this;
    }

    public void BagUpgrade()
    {
        bagpackSize += 4;
    }//가방 크기 +4
    public void AddGold(int amount)
    {
        gold += amount;
    }//소지금(int amount) 추가
}

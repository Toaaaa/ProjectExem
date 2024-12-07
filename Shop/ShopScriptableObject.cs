using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop", menuName = "Shop/New Shop")]
public class ShopScriptableObject : ScriptableObject
{
    public List<Item> items = new List<Item>();//itemdata는 아이템의 정보를, quantity는 상점 리셋전, 해당 아이템 최대 구매가능 개수 로서의 역활을 함.
    public List<Item> specialItems = new List<Item>();//itemdata는 아이템의 정보를, quantity는 상점 리셋전, 해당 아이템 최대 구매가능 개수 로서의 역활을 함.

    public Item RandomItem()
    {
        //임시로 제작한 랜덤 아이템 함수.
        Random.InitState(System.DateTime.Now.Millisecond);
        int i = Random.Range(0, 100);
        if(i < 10)
        {
            return specialItems[Random.Range(0, specialItems.Count)];//스페셜 아이템이 등장함. (10프로의 확률)
        }
        else
        {
            return items[0];//나중에 상점을 열어서 randomitem을 사용할 때 items[0]의 데이터를 받았을 경우, 새로운 슬롯을 추가하는 작업을 스킵하도록 함.
        }
    }
}

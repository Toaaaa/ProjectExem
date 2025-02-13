using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop", menuName = "Shop/New Shop")]
public class ShopScriptableObject : ScriptableObject
{
    public List<Item> items = new List<Item>();//itemdata는 아이템의 정보를, quantity는 상점이 리셋 될 때마다 items.itemData.itemtype에 따라 개수를 초기화 해줌.
    public List<Item> specialItems = new List<Item>();

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
    public void ResetQuantity()
    {
        for (int i = 0; i < items.Count; i++)
        {
            switch (items[i].itemData.Type)
            {
                case ItemData.ItemType.Consumable:
                    items[i].quantity = 6;//슬롯 2칸 분량의 아이템을 리셋.
                    break;
                case ItemData.ItemType.Equipment:
                    items[i].quantity = 1;
                    break;
                case ItemData.ItemType.Buff:
                    items[i].quantity = 2;
                    break;
            }
        }
    }
    public int getItemQuantity(ItemData itemData)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemData == itemData)
            {
                return items[i].quantity;
            }
        }
        return 0;
    }
    public void subItemQuantity(ItemData itemData, int quantity)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemData == itemData)
            {
                items[i].quantity -= quantity;
            }
        }
    }
}

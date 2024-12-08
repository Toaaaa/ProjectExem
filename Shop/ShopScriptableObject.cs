using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop", menuName = "Shop/New Shop")]
public class ShopScriptableObject : ScriptableObject
{
    public List<Item> items = new List<Item>();//itemdata�� �������� ������, quantity�� ������ ���� �� ������ items.itemData.itemtype�� ���� ������ �ʱ�ȭ ����.
    public List<Item> specialItems = new List<Item>();

    public Item RandomItem()
    {
        //�ӽ÷� ������ ���� ������ �Լ�.
        Random.InitState(System.DateTime.Now.Millisecond);
        int i = Random.Range(0, 100);
        if(i < 10)
        {
            return specialItems[Random.Range(0, specialItems.Count)];//����� �������� ������. (10������ Ȯ��)
        }
        else
        {
            return items[0];//���߿� ������ ��� randomitem�� ����� �� items[0]�� �����͸� �޾��� ���, ���ο� ������ �߰��ϴ� �۾��� ��ŵ�ϵ��� ��.
        }
    }
    public void ResetQuantity()
    {
        for (int i = 0; i < items.Count; i++)
        {
            switch (items[i].itemData.Type)
            {
                case ItemData.ItemType.Consumable:
                    items[i].quantity = 6;//���� 2ĭ �з��� �������� ����.
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

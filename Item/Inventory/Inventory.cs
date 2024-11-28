using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private int maxCapacity = 20;
    [SerializeField] private bool isStorage;//â���� ��� �ִ� �뷮�� ��� �þ.

    private void OnEnable()
    {
        if(isStorage)
        {
            GameManager.Instance.inventoryManager.storage = this;
            GameManager.Instance.inventoryManager.LoadStorage();
        }
        else
        {
            GameManager.Instance.inventoryManager.bagpack = this;
            GameManager.Instance.inventoryManager.LoadInventory();
        }
    }
    public bool AddItem(ItemData itemData, int quantity)
    {
        // ���� ������ �߰�
        foreach (var item in items)
        {
            if (item.itemData.ID == itemData.ID)
            {
                item.quantity += quantity;
                SortItemsByID(); // ���� ȣ��
                return true;
            }
        }

        // �� ������ �߰�
        if (items.Count >= maxCapacity) return false; // ������ �� ��
        items.Add(new Item(itemData, quantity));
        SortItemsByID(); // ���� ȣ��
        return true;
    }
    // ��� ���
    //ItemData itemData = new ItemData { id = 1};
    //inventory.AddItem(itemData, 5);
    //���� addItem�� ��.

    public bool RemoveItem(ItemData itemData, int quantity)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemData == itemData)
            {
                items[i].quantity -= quantity;
                if (items[i].quantity <= 0)
                {
                    items.RemoveAt(i); // ������ 0 ������ ��� ����
                }
                return true;
            }
        }

        return false;
    }

    public void SortItemsByID()
    {
        items.Sort((a, b) => a.itemData.ID.CompareTo(b.itemData.ID));
    }

    public List<Item> GetAllItems() => items;
}

[System.Serializable]
public class Item
{
    public ItemData itemData;
    public int quantity;

    public Item(ItemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
    }
}

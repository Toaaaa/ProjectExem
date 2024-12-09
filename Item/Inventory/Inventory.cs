using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    public int maxCapacity = 60;
    [SerializeField] private bool isStorage;//â���� ��� �ִ� �뷮�� ��� �þ.

    private void OnEnable()
    {
        maxCapacity = isStorage ? 60 : GameManager.Instance.inventoryManager.bagpackSize;

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
            if (items[i].itemData.ID == itemData.ID)
            {
                items[i].quantity -= quantity;
                /*if (items[i].quantity <= 0)
                {
                    items.RemoveAt(i); // ������ 0 ������ ��� ����
                }*/
                return true;
            }
        }

        return false;
    }

    public void SortItemsByID()
    {
        // ���� ItemType�� ��������, �״��� ID�� �������� ����
        items.Sort((a, b) =>
        {
            int typeComparison = a.itemData.Type.CompareTo(b.itemData.Type); // ItemType ��
            if (typeComparison != 0)
            {
                return typeComparison; // ItemType�� �ٸ��� �� ����� ����
            }
            return a.itemData.ID.CompareTo(b.itemData.ID); // ItemType�� ������ ID�� ����
        });
    }
    public void ApplyToScriptable(Item item, InventoryScriptableObject sobj)
    {
        //��ũ���ͺ� ��������� �����ϴ� �Լ�.
        foreach (var i in sobj.items)
        {
            if (i.itemData.ID == item.itemData.ID)
            {
                int index = item.itemData.ID;
                try
                {
                    if (items[index].quantity == 0)//InventoryŬ�󽺿� �ӽ÷� ����� �����Ͱ� 0�̸�
                    {
                        sobj.items[index].quantity = 0;//��ũ���ͺ��� 0�� ����.
                        return;
                    }
                    else//0�� �ƴ� ���
                    {
                        i.quantity = items[index].quantity;//items�� �ε����� �������� id�� ���� //�ش��ϴ� InventoryŬ�󽺿� �ӽ÷� ����� �����͸� scriptable�� ����.
                        return;
                    }
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                    return;
                }
            }
        }

        //�������� ���� ���� : Ŭ�� >> InventoryŬ�󽺸� ������ �ӽ� ������Ʈ�� ������ ���� >> ��ũ���ͺ� ������ ���� >> ��ũ���ͺ��� �����͸� �����ϴ� UI�� �����͸� ǥ��.
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

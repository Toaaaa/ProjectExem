using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    public int maxCapacity = 60;
    [SerializeField] private bool isStorage;//창고일 경우 최대 용량이 계속 늘어남.

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
        // 기존 아이템 추가
        foreach (var item in items)
        {
            if (item.itemData.ID == itemData.ID)
            {
                item.quantity += quantity;
                SortItemsByID(); // 정렬 호출
                return true;
            }
        }

        // 새 아이템 추가
        if (items.Count >= maxCapacity) return false; // 가방이 꽉 참
        items.Add(new Item(itemData, quantity));
        SortItemsByID(); // 정렬 호출
        return true;
    }
    // 사용 방법
    //ItemData itemData = new ItemData { id = 1};
    //inventory.AddItem(itemData, 5);
    //으로 addItem을 함.

    public bool RemoveItem(ItemData itemData, int quantity)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemData.ID == itemData.ID)
            {
                items[i].quantity -= quantity;
                /*if (items[i].quantity <= 0)
                {
                    items.RemoveAt(i); // 수량이 0 이하일 경우 제거
                }*/
                return true;
            }
        }

        return false;
    }

    public void SortItemsByID()
    {
        // 먼저 ItemType을 기준으로, 그다음 ID를 기준으로 정렬
        items.Sort((a, b) =>
        {
            int typeComparison = a.itemData.Type.CompareTo(b.itemData.Type); // ItemType 비교
            if (typeComparison != 0)
            {
                return typeComparison; // ItemType이 다르면 그 결과로 정렬
            }
            return a.itemData.ID.CompareTo(b.itemData.ID); // ItemType이 같으면 ID로 정렬
        });
    }
    public void ApplyToScriptable(Item item, InventoryScriptableObject sobj)
    {
        //스크립터블에 변경사항을 적용하는 함수.
        foreach (var i in sobj.items)
        {
            if (i.itemData.ID == item.itemData.ID)
            {
                int index = item.itemData.ID;
                try
                {
                    if (items[index].quantity == 0)//Inventory클라스에 임시로 저장된 데이터가 0이면
                    {
                        sobj.items[index].quantity = 0;//스크립터블에도 0을 적용.
                        return;
                    }
                    else//0이 아닌 경우
                    {
                        i.quantity = items[index].quantity;//items의 인덱스는 아이템의 id와 같다 //해당하는 Inventory클라스에 임시로 저장된 데이터를 scriptable에 적용.
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

        //아이템의 적용 순서 : 클릭 >> Inventory클라스를 가지는 임시 오브젝트에 데이터 적용 >> 스크립터블에 데이터 적용 >> 스크립터블의 데이터를 참고하는 UI가 데이터를 표시.
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

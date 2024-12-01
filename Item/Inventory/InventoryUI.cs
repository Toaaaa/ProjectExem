using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab; // 슬롯 프리팹
    public Transform slotsParent; // Grid Layout이 적용된 부모
    public InventoryScriptableObject inventoryData; // 데이터
    public ItemInfoPanel itemInfoPanel; // 아이템 정보 패널

    private InventorySlot[] slots; // 슬롯 배열

    void Start()
    {
        GenerateSlots();
        UpdateUI();
    }

    public void GenerateSlots()
    {
        // 슬롯 초기화
        slots = new InventorySlot[inventoryData.items.Count];
        for (int i = 0; i < inventoryData.items.Count; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsParent);
            slots[i] = slotObj.GetComponent<RealSlot>().slotPrefab;//실제로 아이템의 정보를 가질 슬롯(slotPrefab).
            slots[i].inventoryUI = this;
        }
    }

    public void UpdateUI()
    {
        // 데이터와 UI 연결
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventoryData.items.Count)
            {
                slots[i].AddItem(inventoryData.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}

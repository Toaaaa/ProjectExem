using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab; // ���� ������
    public Transform slotsParent; // Grid Layout�� ����� �θ�
    public InventoryScriptableObject inventoryData; // ������

    private InventorySlot[] slots; // ���� �迭

    void Start()
    {
        GenerateSlots();
        UpdateUI();
    }

    public void GenerateSlots()
    {
        // ���� �ʱ�ȭ
        slots = new InventorySlot[inventoryData.items.Count];
        for (int i = 0; i < inventoryData.items.Count; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsParent);
            slots[i] = slotObj.GetComponent<RealSlot>().slotPrefab;
        }
    }

    public void UpdateUI()
    {
        // �����Ϳ� UI ����
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon; // ������ ������
    public Button button; // ���� ��ư (���� ����)
    private Item item; // ���Կ� �Ҵ�� ������
    public bool isItemOn; // �������� �ִ��� ����

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemData.Icon; // �������� ������ ǥ��
        icon.enabled = true; // ������ Ȱ��ȭ
        isItemOn = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null; // ������ ����
        icon.enabled = false; // ������ ��Ȱ��ȭ
        isItemOn = false;
    }

    public void OnSlotClicked()//���� Ŭ���� �ƴ� ���콺 ������ ����.
    {
        if (item != null)
        {
            Debug.Log($"Clicked on item: {LocalizationManager.Instance.GetLocalizedString(item.itemData.ItemNameKey)}");
            // �߰� �׼� ó��
        }
    }
}

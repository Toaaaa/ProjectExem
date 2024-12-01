using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryUI inventoryUI;
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
    public void OnPointerEnter(PointerEventData eventData)//���콺�� ���Կ� ���� ������ ����â�� ����.
    {
        if (item != null)
        {
            inventoryUI.itemInfoPanel.gameObject.SetActive(true);
            inventoryUI.itemInfoPanel.icon.sprite = item.itemData.Icon;
            inventoryUI.itemInfoPanel.itemName.text = LocalizationManager.Instance.GetLocalizedString(item.itemData.ItemNameKey);
            inventoryUI.itemInfoPanel.itemDescription.text = LocalizationManager.Instance.GetLocalizedString(item.itemData.DescriptionKey);
            // �߰� �׼� ó��
        }
    }
    public void OnPointerExit(PointerEventData eventData)//���콺�� ���Կ��� ������ ������ ����â�� �ݴ´�.
    {
        if (item != null)
        {
            inventoryUI.itemInfoPanel.gameObject.SetActive(false);
            inventoryUI.itemInfoPanel.icon.sprite = null;
            inventoryUI.itemInfoPanel.itemName.text = "";
            inventoryUI.itemInfoPanel.itemDescription.text = "";
            // �߰� �׼� ó��
        }
    }
}

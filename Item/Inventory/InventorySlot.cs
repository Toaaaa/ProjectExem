using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryUI inventoryUI;
    public Image icon; // 아이템 아이콘
    public Button button; // 슬롯 버튼 (선택 사항)
    private Item item; // 슬롯에 할당된 아이템
    public bool isItemOn; // 아이템이 있는지 여부

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemData.Icon; // 아이템의 아이콘 표시
        icon.enabled = true; // 아이콘 활성화
        isItemOn = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null; // 아이콘 제거
        icon.enabled = false; // 아이콘 비활성화
        isItemOn = false;
    }

    public void OnSlotClicked()//추후 클릭이 아닌 마우스 오버로 변경.
    {
        if (item != null)
        {
            Debug.Log($"Clicked on item: {LocalizationManager.Instance.GetLocalizedString(item.itemData.ItemNameKey)}");
            // 추가 액션 처리
        }
    }
    public void OnPointerEnter(PointerEventData eventData)//마우스가 슬롯에 들어가면 아이템 정보창을 띄운다.
    {
        if (item != null)
        {
            inventoryUI.itemInfoPanel.gameObject.SetActive(true);
            inventoryUI.itemInfoPanel.icon.sprite = item.itemData.Icon;
            inventoryUI.itemInfoPanel.itemName.text = LocalizationManager.Instance.GetLocalizedString(item.itemData.ItemNameKey);
            inventoryUI.itemInfoPanel.itemDescription.text = LocalizationManager.Instance.GetLocalizedString(item.itemData.DescriptionKey);
            // 추가 액션 처리
        }
    }
    public void OnPointerExit(PointerEventData eventData)//마우스가 슬롯에서 나가면 아이템 정보창을 닫는다.
    {
        if (item != null)
        {
            inventoryUI.itemInfoPanel.gameObject.SetActive(false);
            inventoryUI.itemInfoPanel.icon.sprite = null;
            inventoryUI.itemInfoPanel.itemName.text = "";
            inventoryUI.itemInfoPanel.itemDescription.text = "";
            // 추가 액션 처리
        }
    }
}

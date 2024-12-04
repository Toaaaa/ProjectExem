using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryUI inventoryUI;
    public InventoryManager inventoryManager;

    public Image icon; // 아이템 아이콘
    public Button button; // 슬롯 버튼 (선택 사항)
    public TextMeshProUGUI amountText; // 아이템 개수 텍스트
    private Item item; // 슬롯에 할당된 아이템

    public bool isItemOn; // 아이템이 있는지 여부
    public bool isStorage; //false면 인벤토리, true면 창고
    public bool slotBlocked; // 아직 개방되지 않은 슬롯

    [SerializeField]
    private Sprite blockedIcon;

    private void Start()
    {
        inventoryManager = GameManager.Instance.inventoryManager;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemData.Icon; // 아이템의 아이콘 표시
        icon.enabled = true; // 아이콘 활성화
        isItemOn = true;
        amountText.text = item.quantity.ToString(); // 아이템 개수 표시
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null; // 아이콘 제거
        icon.enabled = false; // 아이콘 비활성화
        isItemOn = false;
    }

    public void ZeroAmout()
    {
        amountText.text = ""; // 아이템 개수 제거
    }
    private void Update()
    {
        IconDisplay();
    }
    public void OnSlotClicked()
    {

        if (item != null && inventoryManager != null)
        {
            //클릭한 아이템을 isStorage에 따라서 인벤토리에서 창고로, 창고에서 인벤토리로 이동시킨다.
            if (isStorage)//창고에 있는 아이템을 클릭한 경우
            {
                if (inventoryManager.bagpackSize < inventoryManager.bagSizeMax)
                {
                    inventoryManager.bagpack.AddItem(item.itemData, 1);
                    inventoryManager.storage.RemoveItem(item.itemData, 1);
                    inventoryManager.bagpack.ApplyToScriptable(item, inventoryManager.inventoryData);//인벤토리의 변경사항을 스크립터블에 적용.
                    inventoryManager.storage.ApplyToScriptable(item,inventoryManager.inventoryStorageData);//창고의 변경사항을 스크립터블에 적용.
                }
            }
            else
            {
                //if (GameManager.Instance.inventoryManager.storageSize < GameManager.Instance.inventoryManager.storageSizeMax) //창고는 최대 용량 없음.
                inventoryManager.storage.AddItem(item.itemData, 1);
                inventoryManager.bagpack.RemoveItem(item.itemData, 1);
                inventoryManager.storage.ApplyToScriptable(item,inventoryManager.inventoryStorageData);//창고의 변경사항을 스크립터블에 적용.
                inventoryManager.bagpack.ApplyToScriptable(item,inventoryManager.inventoryData);//인벤토리의 변경사항을 스크립터블에 적용.
            }
        }
        inventoryManager.UpdateUIBoth();//변경사항 업데이트.
    }
    public void OnPointerEnter(PointerEventData eventData)//마우스가 슬롯에 들어가면 아이템 정보창을 띄운다.
    {
        if (item != null)
        {
            inventoryUI.itemInfoPanel.gameObject.SetActive(true);
            if(isStorage)
                inventoryUI.PanelPos(true);//창고일 경우 true를 넣어 창고의 상단에 패널 표시                
            else
                inventoryUI.PanelPos(false);//인벤토리일 경우 false를 넣어 인벤토리의 상단에 패널 표시
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
    private void IconDisplay()
    {
        if(slotBlocked)
        {
            icon.sprite = blockedIcon;
            button.interactable = false;
        }
        else
        {/*
            if (item != null)
                icon.enabled = true;
            else
                icon.enabled = false;*/
        }
    }
}

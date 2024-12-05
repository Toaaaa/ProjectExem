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
    public bool isFoodORArmor; //식량이나 방어구가 들어있는 슬롯

    [SerializeField]
    private Sprite blockedIcon;
    [SerializeField]
    private Sprite FoodOrArmored;

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
    public void OnSlotClicked()//클릭시 아이템의 이동(가방<<O>>창고)
    {

        if (item != null && inventoryManager != null)
        {
            //클릭한 아이템을 isStorage에 따라서 인벤토리에서 창고로, 창고에서 인벤토리로 이동시킨다.
            if (isStorage)//창고에 있는 아이템을 클릭한 경우
            {
                // 가방 용량 확인
                if (!CanAddItemToBagpack(item.itemData, 1))
                {
                    Debug.Log("가방 용량이 가득 찼습니다! 더 이상 아이템을 넣을 수 없습니다.");
                    //가방이 가득 찼음. >> 추후 UX적인 각종 처리.
                    return;
                }

                // 아이템 이동
                inventoryManager.bagpack.AddItem(item.itemData, 1);
                inventoryManager.storage.RemoveItem(item.itemData, 1);
                inventoryManager.bagpack.ApplyToScriptable(item, inventoryManager.inventoryData);
                inventoryManager.storage.ApplyToScriptable(item, inventoryManager.inventoryStorageData);
            }
            else
            {
                //if (GameManager.Instance.inventoryManager.storageSize < GameManager.Instance.inventoryManager.storageSizeMax) 
                //창고는 최대 용량 없음.
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
        {
            if (isFoodORArmor)
            {
                icon.sprite = FoodOrArmored;
                icon.enabled = true;
                button.interactable = false;
            }
            else
            {
                if (item != null)
                {
                    icon.enabled = true;
                    button.interactable = true;
                }
                else
                {
                    icon.enabled = false;
                    button.interactable = false;
                }
            }
        }
    }

    private bool CanAddItemToBagpack(ItemData itemData, int quantityToAdd) // 가방에 아이템을 추가할 수 있는지 빈 공간 확인
    {
        int remainingQuantity = quantityToAdd;
        bool isStackable = false; //가방에 있는 아이템이 스택이 가능한지

        // 현재 점유 슬롯 개수 계산
        int currentSlots = 0;
        foreach (var existingItem in inventoryManager.bagpack.GetAllItems())
        {
            int stackCount = Mathf.CeilToInt((float)existingItem.quantity / existingItem.itemData.MaxStack);
            currentSlots += stackCount;
            if(itemData.ID == existingItem.itemData.ID)
            {
                if ((existingItem.quantity % existingItem.itemData.MaxStack) != 0)
                    isStackable = true;//가방에 있는 아이템이 maxStack을 확인시 여유가 있을떄 true.
            }
        }
        // 식량, 방어구 수량 추가
        int currentFood = inventoryManager.inventoryData.getFoodAmount();
        int currentArmor = inventoryManager.inventoryData.getArmorAmount();
        int totalOccupiedSlots = currentSlots + currentFood + currentArmor;
        // 슬롯 초과 여부 확인
        if (totalOccupiedSlots >= inventoryManager.bagpackSize)
        {
            if(itemData.Stackable && !isStackable)
            {
                return false; // 슬롯이 부족함
            }
            else if (isStackable)
            {
                //빈칸은 없지만 스택이 가능한 경우 false를 반환하지 않고 넘어감.
            }
            else if(inventoryManager.bagUI.GetSlot(currentSlots).FoodOrArmored)//새로 넣으려는 슬롯에 식량이나 방어구가 들어있는 경우
            {
                return false; 
            }
        }
        // 1단계: 기존 스택에서 추가 가능 여부 확인
        foreach (var existingItem in inventoryManager.bagpack.GetAllItems())
        {
            if (existingItem.itemData.ID == itemData.ID && itemData.Stackable)
            {
                int availableSpace = existingItem.itemData.MaxStack - (existingItem.quantity % existingItem.itemData.MaxStack);
                if (availableSpace > 0)
                {
                    if (availableSpace >= remainingQuantity)
                    {
                        return true; // 기존 스택에서 충분히 추가 가능
                    }
                    remainingQuantity -= availableSpace; // 일부만 채워지고 남은 양 갱신
                }
            }
        }

        // 2단계: 남은 수량 처리 (빈 슬롯 필요)
        int emptySlots = inventoryManager.bagpackSize - totalOccupiedSlots;
        int slotsNeeded = Mathf.CeilToInt((float)remainingQuantity / itemData.MaxStack);

        return emptySlots >= slotsNeeded; // 빈 슬롯으로 남은 수량 처리 가능 여부 반환
    }
}

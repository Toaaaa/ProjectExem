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

    public Image icon; // ������ ������
    public Button button; // ���� ��ư (���� ����)
    public TextMeshProUGUI amountText; // ������ ���� �ؽ�Ʈ
    private Item item; // ���Կ� �Ҵ�� ������

    public bool isItemOn; // �������� �ִ��� ����
    public bool isStorage; //false�� �κ��丮, true�� â��
    public bool slotBlocked; // ���� ������� ���� ����
    public bool isFoodORArmor; //�ķ��̳� ���� ����ִ� ����

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
        icon.sprite = item.itemData.Icon; // �������� ������ ǥ��
        icon.enabled = true; // ������ Ȱ��ȭ
        isItemOn = true;
        amountText.text = item.quantity.ToString(); // ������ ���� ǥ��
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null; // ������ ����
        icon.enabled = false; // ������ ��Ȱ��ȭ
        isItemOn = false;
    }

    public void ZeroAmout()
    {
        amountText.text = ""; // ������ ���� ����
    }
    private void Update()
    {
        IconDisplay();
    }
    public void OnSlotClicked()//Ŭ���� �������� �̵�(����<<O>>â��)
    {

        if (item != null && inventoryManager != null)
        {
            //Ŭ���� �������� isStorage�� ���� �κ��丮���� â���, â���� �κ��丮�� �̵���Ų��.
            if (isStorage)//â�� �ִ� �������� Ŭ���� ���
            {
                // ���� �뷮 Ȯ��
                if (!CanAddItemToBagpack(item.itemData, 1))
                {
                    Debug.Log("���� �뷮�� ���� á���ϴ�! �� �̻� �������� ���� �� �����ϴ�.");
                    //������ ���� á��. >> ���� UX���� ���� ó��.
                    return;
                }

                // ������ �̵�
                inventoryManager.bagpack.AddItem(item.itemData, 1);
                inventoryManager.storage.RemoveItem(item.itemData, 1);
                inventoryManager.bagpack.ApplyToScriptable(item, inventoryManager.inventoryData);
                inventoryManager.storage.ApplyToScriptable(item, inventoryManager.inventoryStorageData);
            }
            else
            {
                //if (GameManager.Instance.inventoryManager.storageSize < GameManager.Instance.inventoryManager.storageSizeMax) 
                //â��� �ִ� �뷮 ����.
                inventoryManager.storage.AddItem(item.itemData, 1);
                inventoryManager.bagpack.RemoveItem(item.itemData, 1);
                inventoryManager.storage.ApplyToScriptable(item,inventoryManager.inventoryStorageData);//â���� ��������� ��ũ���ͺ� ����.
                inventoryManager.bagpack.ApplyToScriptable(item,inventoryManager.inventoryData);//�κ��丮�� ��������� ��ũ���ͺ� ����.
            }
        }
        inventoryManager.UpdateUIBoth();//������� ������Ʈ.
    }
    public void OnPointerEnter(PointerEventData eventData)//���콺�� ���Կ� ���� ������ ����â�� ����.
    {
        if (item != null)
        {
            inventoryUI.itemInfoPanel.gameObject.SetActive(true);
            if(isStorage)
                inventoryUI.PanelPos(true);//â���� ��� true�� �־� â���� ��ܿ� �г� ǥ��                
            else
                inventoryUI.PanelPos(false);//�κ��丮�� ��� false�� �־� �κ��丮�� ��ܿ� �г� ǥ��
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

    private bool CanAddItemToBagpack(ItemData itemData, int quantityToAdd) // ���濡 �������� �߰��� �� �ִ��� �� ���� Ȯ��
    {
        int remainingQuantity = quantityToAdd;
        bool isStackable = false; //���濡 �ִ� �������� ������ ��������

        // ���� ���� ���� ���� ���
        int currentSlots = 0;
        foreach (var existingItem in inventoryManager.bagpack.GetAllItems())
        {
            int stackCount = Mathf.CeilToInt((float)existingItem.quantity / existingItem.itemData.MaxStack);
            currentSlots += stackCount;
            if(itemData.ID == existingItem.itemData.ID)
            {
                if ((existingItem.quantity % existingItem.itemData.MaxStack) != 0)
                    isStackable = true;//���濡 �ִ� �������� maxStack�� Ȯ�ν� ������ ������ true.
            }
        }
        // �ķ�, �� ���� �߰�
        int currentFood = inventoryManager.inventoryData.getFoodAmount();
        int currentArmor = inventoryManager.inventoryData.getArmorAmount();
        int totalOccupiedSlots = currentSlots + currentFood + currentArmor;
        // ���� �ʰ� ���� Ȯ��
        if (totalOccupiedSlots >= inventoryManager.bagpackSize)
        {
            if(itemData.Stackable && !isStackable)
            {
                return false; // ������ ������
            }
            else if (isStackable)
            {
                //��ĭ�� ������ ������ ������ ��� false�� ��ȯ���� �ʰ� �Ѿ.
            }
            else if(inventoryManager.bagUI.GetSlot(currentSlots).FoodOrArmored)//���� �������� ���Կ� �ķ��̳� ���� ����ִ� ���
            {
                return false; 
            }
        }
        // 1�ܰ�: ���� ���ÿ��� �߰� ���� ���� Ȯ��
        foreach (var existingItem in inventoryManager.bagpack.GetAllItems())
        {
            if (existingItem.itemData.ID == itemData.ID && itemData.Stackable)
            {
                int availableSpace = existingItem.itemData.MaxStack - (existingItem.quantity % existingItem.itemData.MaxStack);
                if (availableSpace > 0)
                {
                    if (availableSpace >= remainingQuantity)
                    {
                        return true; // ���� ���ÿ��� ����� �߰� ����
                    }
                    remainingQuantity -= availableSpace; // �Ϻθ� ä������ ���� �� ����
                }
            }
        }

        // 2�ܰ�: ���� ���� ó�� (�� ���� �ʿ�)
        int emptySlots = inventoryManager.bagpackSize - totalOccupiedSlots;
        int slotsNeeded = Mathf.CeilToInt((float)remainingQuantity / itemData.MaxStack);

        return emptySlots >= slotsNeeded; // �� �������� ���� ���� ó�� ���� ���� ��ȯ
    }
}

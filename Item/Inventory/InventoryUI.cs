using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor;

public class InventoryUI : MonoBehaviour
{
    private bool isInitialized = false; //ó�� �ʱ�ȭ ����.

    public GameObject slotPrefab; // ���� ������
    public Transform slotsParent; // Grid Layout�� ����� �ڱ��ڽ�
    public InventoryScriptableObject inventoryData; // ������
    public ItemInfoPanel itemInfoPanel; // ������ ���� �г�
    private RectTransform panelRect; // �г��� RectTransform
    [SerializeField]
    private Vector2 panelPosBag; // �г��� ��������
    [SerializeField]
    private Vector2 panelPosStorage; // �г��� â������


    private InventorySlot[] slots; // ���� �迭

    [SerializeField]
    private bool isStorage; //false�� �κ��丮, true�� â��
    [SerializeField]
    private bool isShopStorage; //���� UI���� ���̴� â�� ��������
    [SerializeField]
    private ScrollRect scrollRect; //��ũ�Ѻ�

    //////////�κ��丮 ��ü ����///////////
    public int currentSize; //���� �κ��丮 ũ�� (���빰�� �ִ� ������ ����)
    public int maxSize; //�κ��丮 ũ�� (�ִ� ���� ����)
    public TextMeshProUGUI sizeText; //�κ��丮 ũ�� ǥ�� �ؽ�Ʈ
    //////////�ķ� �� �� ��ư///////////
    [SerializeField] private TextMeshProUGUI foodAmount;
    [SerializeField] private TextMeshProUGUI armorAmount;

    private void Start()
    {
        panelRect = itemInfoPanel.GetComponent<RectTransform>();
        
        //////////
        
        if (isStorage&&!isShopStorage)
        {
            if (scrollRect != null)
                scrollRect.enabled = true; // ��ũ�� Ȱ��ȭ
            GameManager.Instance.inventoryManager.storageUI = this;
            GenerateSlotsStorage();
            maxSize = GameManager.Instance.inventoryManager.storageSize;
        }
        else if(!isStorage && !isShopStorage)
        {
            if(scrollRect != null)
                scrollRect.enabled = false; // ��ũ�� ��Ȱ��ȭ
            GameManager.Instance.inventoryManager.bagUI = this;
            GenerateSlots();
            maxSize = GameManager.Instance.inventoryManager.bagpackSize;
        }
        else//���� â��
        {
            if (scrollRect != null)
                scrollRect.enabled = true; // ��ũ�� Ȱ��ȭ
            GameManager.Instance.inventoryManager.shopStorageUI = this;
            GenerateSlotsStorage();
            maxSize = GameManager.Instance.inventoryManager.storageSize;
        }

        isInitialized = true;
        UpdateUI();//getfilledslotindex �������� ����.
    }

    private void OnEnable()
    {
        if (isInitialized)
        {
            UpdateUI();
            ScrollToTop();

        }
    }

    private void GenerateSlots()
    {
        // ���� �ʱ�ȭ
        int maxSize = GameManager.Instance.inventoryManager.bagSizeMax;
        int currentSize = GameManager.Instance.inventoryManager.bagpackSize;
        slots = new InventorySlot[maxSize];
        for (int i = 0; i < maxSize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsParent);
            slots[i] = slotObj.GetComponent<RealSlot>().slotPrefab;//������ �������� ������ ���� ����(slotPrefab).
            slots[i].inventoryUI = this;
            slots[i].isStorage = isStorage;
            if(i >= currentSize)
            {
                slots[i].slotBlocked = true;
            }//���� ��� ������ ���� �̻��� ���� ������ ���Ƶд�.
        }
    }
    private void GenerateSlotsStorage()
    {
        int maxSize = GameManager.Instance.inventoryManager.storageSize;

        // Grid Layout ���� ��������
        RectTransform slotsParentRect = slotsParent.GetComponent<RectTransform>();
        GridLayoutGroup gridLayoutGroup = slotsParent.GetComponent<GridLayoutGroup>();

        // ���� �� �� ���Կ� ���� �� ���
        int columns = gridLayoutGroup.constraintCount; // ������ �� ��
        int totalRows = Mathf.CeilToInt((float)maxSize / columns); // �� �� ���

        // Content ũ�� ���� ���� (�� * �� ũ�� + ����)
        float newHeight = totalRows * gridLayoutGroup.cellSize.y + (totalRows - 1) * gridLayoutGroup.spacing.y;
        slotsParentRect.sizeDelta = new Vector2(slotsParentRect.sizeDelta.x, newHeight);

        // ���� �ʱ�ȭ
        slots = new InventorySlot[maxSize];
        for (int i = 0; i < maxSize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsParent);
            slots[i] = slotObj.GetComponent<RealSlot>().slotPrefab;//������ �������� ������ ���� ����(slotPrefab).
            slots[i].inventoryUI = this;
            slots[i].isStorage = isStorage;
            slots[i].isShopStorage = isShopStorage;
        }

        ScrollToTop(); // ��ũ�� �� ���� �̵�
    }

    public void UpdateUI()//���÷��� ���� ���� ������Ʈ
    {
        // ��ȿ�� �����۸� ����
        List<Item> validItems = new List<Item>();
        foreach (var item in inventoryData.items)
        {
            if (item.quantity > 0)
            {
                validItems.Add(item);
            }
        }

        int slotIndex = 0;

        // ��ȿ�� �������� ���Կ� ���������� ��ġ
        foreach (var item in validItems)
        {
            int remainingQuantity = item.quantity;

            while (remainingQuantity > 0 && slotIndex < slots.Length)
            {
                int stackAmount = Mathf.Min(item.itemData.MaxStack, remainingQuantity);

                slots[slotIndex].slotBlocked = false;
                slots[slotIndex].AddItem(new Item(item.itemData, stackAmount));

                remainingQuantity -= stackAmount;
                slotIndex++;
            }

            // ������ �� á�µ��� �������� ���� ���� ó������ ���� (�뷮 �ʰ� ����)
            if (remainingQuantity > 0)
            {
                Debug.LogWarning($"������ {item.itemData.ID}�� ���� ���� �뷮�� �ʰ��߽��ϴ�!");
            }
        }

        // ���� ���� �ʱ�ȭ �Ǵ� ��Ȱ��ȭ
        for (int i = slotIndex; i < slots.Length; i++)
        {
            if (i < GameManager.Instance.inventoryManager.bagpackSize || isStorage) // ��ȿ�� ���� ���� ��
            {
                slots[i].slotBlocked = false;
                slots[i].ClearSlot();
                slots[i].ZeroAmout();      
            }
            else // ������ ���� ��Ȱ��ȭ
            {
                if (!isStorage) // �κ��丮�� ��� ���� ���� ǥ��
                {
                    slots[i].slotBlocked = true;
                    slots[i].ZeroAmout();
                }
            }
            slots[i].isFoodORArmor = false;
        }
        //�κ��丮 ���� �ķ� �Ǵ� ���� ���Կ� ǥ��
        int foodAndArmor = inventoryData.getFoodAmount() + inventoryData.getArmorAmount();
        for (int i = slots.Length - 1; i >= slotIndex; i--)
        {
            if (!isStorage && !slots[i].slotBlocked)
            {
                if(foodAndArmor > 0)
                {
                    slots[i].isFoodORArmor = true;
                    foodAndArmor--;
                }
            }
        }

            GetFilledSlotIndex();//currentSize (���� �������� ����ִ� ���� ����) ������Ʈ
       
        if (!isStorage)//������ ��츸, �ķ�,��,����뷮 ǥ��.
        {
            foodAmount.text = $"x{inventoryData.getFoodAmount().ToString()}";
            armorAmount.text = $"x{inventoryData.getArmorAmount().ToString()}";
            sizeText.text = $"{currentSize}/{maxSize}";
        }

#if UNITY_EDITOR
EditorUtility.SetDirty(inventoryData);//�κ��丮 scriptable object�� ������� ����(������ ���� ��������� ����)
#endif
    }
    private void GetFilledSlotIndex()//�������� ����ִ� ���� ����
    {
        int index = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isItemOn)
            {
                index++;
            }
        }
        if (!isStorage)//������ ���
            index += inventoryData.getFoodAmount() + inventoryData.getArmorAmount();
        currentSize = index;
    }
    private void ScrollToTop()
    {
        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases(); // ĵ���� ����
            scrollRect.verticalNormalizedPosition = 1f; // �� ���� ����
        }
    }
    public void PanelPos(bool isstorage)
    {
        if(isstorage)
        {
            panelRect.anchoredPosition = panelPosStorage;
        }
        else
        {
            panelRect.anchoredPosition = panelPosBag;
        }
    }

    public void FoodAddButton()
    {
        if(currentSize < maxSize)
        {
            inventoryData.FoodButton(1);
            UpdateUI();
        }
    }
    public void ArmorAddButton()
    {
        if (currentSize < maxSize)
        {
            inventoryData.ArmorButton(1);
            UpdateUI();
        }
    }
    public void FoodSubButton()
    {
        if (inventoryData.getFoodAmount() > 0)
        {
            inventoryData.FoodButton(-1);
            UpdateUI();
        }
    }
    public void ArmorSubButton()
    {
        if (inventoryData.getArmorAmount() > 0)
        {
            inventoryData.ArmorButton(-1);
            UpdateUI();
        }
    }
    public InventorySlot GetSlot(int index)
    {
        return slots[index];
    }
}

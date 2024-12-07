using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private bool isInitialized = false; //ó�� �ʱ�ȭ ����.
    public ShopUIManager shopUIManager;

    public GameObject slotPrefab; // ���� ������
    public Transform slotsParent; // Grid Layout�� ����� �ڱ��ڽ�
    public ShopScriptableObject shopData; // ������
    public ItemInfoPanel itemInfoPanel; // ������ ���� �г�
    private RectTransform panelRect; // �г��� RectTransform
    [SerializeField]
    private Vector2 panelPosStorage; // �г��� â������


    private InventorySlot[] slots; // ���� �迭
    private bool isSpecialItem;//����� �������� �����ߴ��� ���� << ���߿� �����ֺ��� ȿ���� �� ����.

    [SerializeField]
    private ScrollRect scrollRect; //��ũ�Ѻ�



    private void Start()
    {
        panelRect = itemInfoPanel.GetComponent<RectTransform>();
        isSpecialItem = false;
            if (scrollRect != null)
                scrollRect.enabled = true; // ��ũ�� Ȱ��ȭ
            GenerateSlotsShop();

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


    private void GenerateSlotsShop()
    {
        int maxSize = shopData.items.Count;
        if(shopData.RandomItem() != shopData.items[0])
        {
            maxSize++; //10%�� Ȯ���� ����� �������� ����������, ���� �ϳ��� �߰�.
            isSpecialItem = true;
        }
        else
        {
            isSpecialItem = false;
        }
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
            slots[i].inventoryUI = shopUIManager.storageUI;
        }

        ScrollToTop(); // ��ũ�� �� ���� �̵�
    }

    public void UpdateUI()//���÷��� ���� ���� ������Ʈ
    {
        // ��ȿ�� �����۸� ����
        List<Item> validItems = new List<Item>();
        foreach (var item in shopData.items)
        {
            if (item.quantity > 0)
            {
                validItems.Add(item);
            }
        }
        if (isSpecialItem)
        {
            Debug.Log("����� �������� �����߽��ϴ�.");//���� ���� �ֺ��� ȿ���� ���⵵�� �ڵ� �߰�.
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
            if (i < GameManager.Instance.inventoryManager.bagpackSize) // ��ȿ�� ���� ���� ��
            {
                slots[i].slotBlocked = false;
                slots[i].ClearSlot();
                slots[i].ZeroAmout();
            }
            else // ������ ���� ��Ȱ��ȭ
            {
                slots[i].slotBlocked = true;
                slots[i].ZeroAmout();                
            }
            slots[i].isFoodORArmor = false;
        }

        GetFilledSlotIndex();//currentSize (���� �������� ����ִ� ���� ����) ������Ʈ

#if UNITY_EDITOR
        EditorUtility.SetDirty(shopData);//�κ��丮 scriptable object�� ������� ����(������ ���� ��������� ����)
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
        panelRect.anchoredPosition = panelPosStorage;
    }
    public InventorySlot GetSlot(int index)
    {
        return slots[index];
    }
}

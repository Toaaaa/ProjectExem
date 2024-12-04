using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    private ScrollRect scrollRect; //��ũ�Ѻ�

    //////////�κ��丮 ��ü ����///////////
    public int currentSize; //���� �κ��丮 ũ�� (���빰�� �ִ� ������ ����)
    public int maxSize; //�ִ� �κ��丮 ũ�� (�ִ� ���� ����)

    private void Start()
    {
        panelRect = itemInfoPanel.GetComponent<RectTransform>();
        
        //////////
        
        if (isStorage)
        {
            if (scrollRect != null)
                scrollRect.enabled = true; // ��ũ�� Ȱ��ȭ
            GameManager.Instance.inventoryManager.storageUI = this;
            GenerateSlotsStorage();
        }
        else
        {
            if(scrollRect != null)
                scrollRect.enabled = false; // ��ũ�� ��Ȱ��ȭ
            GameManager.Instance.inventoryManager.bagUI = this;
            GenerateSlots();
        }
        maxSize = GameManager.Instance.inventoryManager.bagSizeMax;
        currentSize = GetEmptySlotIndex();

        isInitialized = true;
        UpdateUI();
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

        // ��ȿ�� �������� ���Կ� ���������� ��ġ
        int itemIndex = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (itemIndex < validItems.Count) // ��ȿ�� �������� ���� �ִ� ���
            {
                slots[i].slotBlocked = false;
                slots[i].AddItem(validItems[itemIndex]);
                itemIndex++;
            }
            else if (i < GameManager.Instance.inventoryManager.bagpackSize || isStorage) // ��ȿ�� ���� ���� ��
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
        }
    }
    private int GetEmptySlotIndex()//currentSize�� ���ϴ� �Լ�
    {
        int index = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].isItemOn)
            {
                index++;
            }
        }
        return maxSize - index;
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
}

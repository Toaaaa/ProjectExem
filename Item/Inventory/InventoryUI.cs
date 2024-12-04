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

    private InventorySlot[] slots; // ���� �迭

    [SerializeField]
    private bool isStorage; //false�� �κ��丮, true�� â��
    [SerializeField]
    private ScrollRect scrollRect; //��ũ�Ѻ�

    private void Start()
    {
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
        // �����Ϳ� UI ����
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventoryData.items.Count && inventoryData.items[i].quantity !=0) //�������� �����ϸ� 0���� �ƴҶ�.
            {
                slots[i].slotBlocked = false;
                slots[i].AddItem(inventoryData.items[i]);
            }
            else if(i < GameManager.Instance.inventoryManager.bagpackSize || isStorage)
            {
                slots[i].slotBlocked = false;
                slots[i].ClearSlot();
                slots[i].ZeroAmout();
            }
            else
            {
                if(!isStorage)//�κ��� ��� �������� ǥ��
                {
                    slots[i].slotBlocked = true;
                    slots[i].ZeroAmout();
                }
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
}

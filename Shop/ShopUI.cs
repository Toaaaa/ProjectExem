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
    public ShopBuyPopup shopBuyPopup; // ���� �˾�
    private RectTransform panelRect; // �г��� RectTransform
    [SerializeField]
    private Vector2 panelPosStorage; // �г��� â������


    private InventorySlot[] slots; // ���� �迭
    private bool isSpecialItem;//����� �������� �����ߴ��� ���� << ���߿� �����ֺ��� ȿ���� �� ����.
    private Item specialItem;//����� ������

    [SerializeField]
    private ScrollRect scrollRect; //��ũ�Ѻ�



    private void Start()
    {
        panelRect = itemInfoPanel.GetComponent<RectTransform>();
        GameManager.Instance.inventoryManager.shopUI = this;
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


    private void GenerateSlotsShop()//�̰� ���� ����Ű�鼭 ������ ���� �����ϱ�, �����ؼ� ���� �ڵ� ����.
    {
        int maxSize = shopData.items.Count;
        Item specialIt = shopData.RandomItem();
        if(specialIt != shopData.items[0])
        {
            specialItem = specialIt;
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
            slots[i].inventoryUI = shopUIManager.storageUI;//���� ������ inventoryUI���� ����ϴ� ������ �����г��� ��ġ���̶� �����ҵ�.
            slots[i].isShopStorage = true;
        }

        ScrollToTop(); // ��ũ�� �� ���� �̵�
    }

    public void UpdateUI()//���÷��� ���� ���� ������Ʈ
    {
        // ��ȿ�� �����۸� ����
        List<Item> validItems = new List<Item>();
        foreach (var item in shopData.items)
        {
            if (item.quantity >= 0) //������ ��� 0���� �Ǿ ������ ����� ����.
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
            if (slotIndex >= slots.Length)
            {
                Debug.LogWarning("������ �����մϴ�. ���� �����͸� Ȯ���ϼ���.");
                break;
            }

            slots[slotIndex].slotBlocked = false;
            slots[slotIndex].AddItem(new Item(item.itemData, item.quantity));
            slotIndex++;
        }

        // ���� ���� �ʱ�ȭ �Ǵ� ��Ȱ��ȭ (���� 0�� ���� ����)
        for (int i = slotIndex; i < slots.Length; i++)
        {
            if(isSpecialItem && i == slots.Length - 1)
            {
                slots[i].AddItem(specialItem);
            }
                // ���������� ������ �ʱ�ȭ���� ���� (0�� ����)
                if (shopData.getItemQuantity(slots[i].GetItem().itemData) == 0)
                {
                    slots[i].ZeroAmout(); // �ð������� ������ 0 ǥ��
                }
        }

#if UNITY_EDITOR
        EditorUtility.SetDirty(shopData);//�κ��丮 scriptable object�� ������� ����(������ ���� ��������� ����)
#endif
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

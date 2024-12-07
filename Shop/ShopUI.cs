using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private bool isInitialized = false; //처음 초기화 여부.
    public ShopUIManager shopUIManager;

    public GameObject slotPrefab; // 슬롯 프리팹
    public Transform slotsParent; // Grid Layout이 적용된 자기자신
    public ShopScriptableObject shopData; // 데이터
    public ItemInfoPanel itemInfoPanel; // 아이템 정보 패널
    private RectTransform panelRect; // 패널의 RectTransform
    [SerializeField]
    private Vector2 panelPosStorage; // 패널이 창고위에


    private InventorySlot[] slots; // 슬롯 배열
    private bool isSpecialItem;//스페셜 아이템이 등장했는지 여부 << 나중에 슬롯주변에 효과를 줄 예정.

    [SerializeField]
    private ScrollRect scrollRect; //스크롤뷰



    private void Start()
    {
        panelRect = itemInfoPanel.GetComponent<RectTransform>();
        isSpecialItem = false;
            if (scrollRect != null)
                scrollRect.enabled = true; // 스크롤 활성화
            GenerateSlotsShop();

        isInitialized = true;
        UpdateUI();//getfilledslotindex 마지막에 실행.
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
            maxSize++; //10%의 확률로 스페셜 아이템이 등장함으로, 슬롯 하나더 추가.
            isSpecialItem = true;
        }
        else
        {
            isSpecialItem = false;
        }
        // Grid Layout 정보 가져오기
        RectTransform slotsParentRect = slotsParent.GetComponent<RectTransform>();
        GridLayoutGroup gridLayoutGroup = slotsParent.GetComponent<GridLayoutGroup>();

        // 열의 수 및 슬롯에 따른 행 계산
        int columns = gridLayoutGroup.constraintCount; // 고정된 열 수
        int totalRows = Mathf.CeilToInt((float)maxSize / columns); // 행 수 계산

        // Content 크기 동적 조정 (행 * 셀 크기 + 간격)
        float newHeight = totalRows * gridLayoutGroup.cellSize.y + (totalRows - 1) * gridLayoutGroup.spacing.y;
        slotsParentRect.sizeDelta = new Vector2(slotsParentRect.sizeDelta.x, newHeight);

        // 슬롯 초기화
        slots = new InventorySlot[maxSize];
        for (int i = 0; i < maxSize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsParent);
            slots[i] = slotObj.GetComponent<RealSlot>().slotPrefab;//실제로 아이템의 정보를 가질 슬롯(slotPrefab).
            slots[i].inventoryUI = shopUIManager.storageUI;
        }

        ScrollToTop(); // 스크롤 맨 위로 이동
    }

    public void UpdateUI()//디스플레이 상의 정보 업데이트
    {
        // 유효한 아이템만 수집
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
            Debug.Log("스페셜 아이템이 등장했습니다.");//추후 슬롯 주변에 효과가 생기도록 코드 추가.
        }
        int slotIndex = 0;

        // 유효한 아이템을 슬롯에 순차적으로 배치
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

            // 슬롯이 다 찼는데도 아이템이 남는 경우는 처리하지 않음 (용량 초과 상태)
            if (remainingQuantity > 0)
            {
                Debug.LogWarning($"아이템 {item.itemData.ID}의 양이 슬롯 용량을 초과했습니다!");
            }
        }

        // 남은 슬롯 초기화 또는 비활성화
        for (int i = slotIndex; i < slots.Length; i++)
        {
            if (i < GameManager.Instance.inventoryManager.bagpackSize) // 유효한 슬롯 범위 내
            {
                slots[i].slotBlocked = false;
                slots[i].ClearSlot();
                slots[i].ZeroAmout();
            }
            else // 나머지 슬롯 비활성화
            {
                slots[i].slotBlocked = true;
                slots[i].ZeroAmout();                
            }
            slots[i].isFoodORArmor = false;
        }

        GetFilledSlotIndex();//currentSize (현재 아이템이 들어있는 슬롯 개수) 업데이트

#if UNITY_EDITOR
        EditorUtility.SetDirty(shopData);//인벤토리 scriptable object에 변경사항 저장(에디터 상의 변경사항을 유지)
#endif
    }
    private void GetFilledSlotIndex()//아이템이 들어있는 슬롯 개수
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
            Canvas.ForceUpdateCanvases(); // 캔버스 갱신
            scrollRect.verticalNormalizedPosition = 1f; // 맨 위로 설정
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

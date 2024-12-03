using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    private bool isInitialized = false; //처음 초기화 여부.

    public GameObject slotPrefab; // 슬롯 프리팹
    public Transform slotsParent; // Grid Layout이 적용된 자기자신
    public InventoryScriptableObject inventoryData; // 데이터
    public ItemInfoPanel itemInfoPanel; // 아이템 정보 패널

    private InventorySlot[] slots; // 슬롯 배열

    [SerializeField]
    private bool isStorage; //false면 인벤토리, true면 창고
    [SerializeField]
    private ScrollRect scrollRect; //스크롤뷰

    private void Start()
    {
        if (isStorage)
        {
            if (scrollRect != null)
                scrollRect.enabled = true; // 스크롤 활성화
            GenerateSlotsStorage();
        }
        else
        {
            if(scrollRect != null)
                scrollRect.enabled = false; // 스크롤 비활성화
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
        // 슬롯 초기화
        int maxSize = GameManager.Instance.inventoryManager.bagSizeMax;
        int currentSize = GameManager.Instance.inventoryManager.bagpackSize;
        slots = new InventorySlot[maxSize];
        for (int i = 0; i < maxSize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsParent);
            slots[i] = slotObj.GetComponent<RealSlot>().slotPrefab;//실제로 아이템의 정보를 가질 슬롯(slotPrefab).
            slots[i].inventoryUI = this;
            if(i >= currentSize)
            {
                slots[i].slotBlocked = true;
            }//현재 사용 가능한 슬롯 이상의 여분 슬롯은 막아둔다.
        }
    }
    private void GenerateSlotsStorage()
    {
        int maxSize = GameManager.Instance.inventoryManager.storageSize;

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
            slots[i].inventoryUI = this;
        }

        ScrollToTop(); // 스크롤 맨 위로 이동
    }

    private void UpdateUI()
    {
        // 데이터와 UI 연결
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventoryData.items.Count)
            {
                slots[i].slotBlocked = false;
                slots[i].AddItem(inventoryData.items[i]);
            }
            else if(i < GameManager.Instance.inventoryManager.bagpackSize || isStorage)
            {
                slots[i].slotBlocked = false;
                slots[i].ClearSlot();
            }
            else
            {
                if(!isStorage)//인벤의 경우 막힌슬롯 표시
                    slots[i].slotBlocked = true;
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
}

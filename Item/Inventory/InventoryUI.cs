using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor;

public class InventoryUI : MonoBehaviour
{
    private bool isInitialized = false; //처음 초기화 여부.

    public GameObject slotPrefab; // 슬롯 프리팹
    public Transform slotsParent; // Grid Layout이 적용된 자기자신
    public InventoryScriptableObject inventoryData; // 데이터
    public ItemInfoPanel itemInfoPanel; // 아이템 정보 패널
    private RectTransform panelRect; // 패널의 RectTransform
    [SerializeField]
    private Vector2 panelPosBag; // 패널이 가방위에
    [SerializeField]
    private Vector2 panelPosStorage; // 패널이 창고위에


    private InventorySlot[] slots; // 슬롯 배열

    [SerializeField]
    private bool isStorage; //false면 인벤토리, true면 창고
    [SerializeField]
    private bool isShopStorage; //상점 UI에서 보이는 창고 슬롯인지
    [SerializeField]
    private ScrollRect scrollRect; //스크롤뷰

    //////////인벤토리 자체 정보///////////
    public int currentSize; //현재 인벤토리 크기 (내용물이 있는 슬롯의 개수)
    public int maxSize; //인벤토리 크기 (최대 슬롯 개수)
    public TextMeshProUGUI sizeText; //인벤토리 크기 표시 텍스트
    //////////식량 및 방어구 버튼///////////
    [SerializeField] private TextMeshProUGUI foodAmount;
    [SerializeField] private TextMeshProUGUI armorAmount;

    private void Start()
    {
        panelRect = itemInfoPanel.GetComponent<RectTransform>();
        
        //////////
        
        if (isStorage&&!isShopStorage)
        {
            if (scrollRect != null)
                scrollRect.enabled = true; // 스크롤 활성화
            GameManager.Instance.inventoryManager.storageUI = this;
            GenerateSlotsStorage();
            maxSize = GameManager.Instance.inventoryManager.storageSize;
        }
        else if(!isStorage && !isShopStorage)
        {
            if(scrollRect != null)
                scrollRect.enabled = false; // 스크롤 비활성화
            GameManager.Instance.inventoryManager.bagUI = this;
            GenerateSlots();
            maxSize = GameManager.Instance.inventoryManager.bagpackSize;
        }
        else//상점 창고
        {
            if (scrollRect != null)
                scrollRect.enabled = true; // 스크롤 활성화
            GameManager.Instance.inventoryManager.shopStorageUI = this;
            GenerateSlotsStorage();
            maxSize = GameManager.Instance.inventoryManager.storageSize;
        }

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
            slots[i].isStorage = isStorage;
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
            slots[i].isStorage = isStorage;
            slots[i].isShopStorage = isShopStorage;
        }

        ScrollToTop(); // 스크롤 맨 위로 이동
    }

    public void UpdateUI()//디스플레이 상의 정보 업데이트
    {
        // 유효한 아이템만 수집
        List<Item> validItems = new List<Item>();
        foreach (var item in inventoryData.items)
        {
            if (item.quantity > 0)
            {
                validItems.Add(item);
            }
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
            if (i < GameManager.Instance.inventoryManager.bagpackSize || isStorage) // 유효한 슬롯 범위 내
            {
                slots[i].slotBlocked = false;
                slots[i].ClearSlot();
                slots[i].ZeroAmout();      
            }
            else // 나머지 슬롯 비활성화
            {
                if (!isStorage) // 인벤토리의 경우 막힌 슬롯 표시
                {
                    slots[i].slotBlocked = true;
                    slots[i].ZeroAmout();
                }
            }
            slots[i].isFoodORArmor = false;
        }
        //인벤토리 상의 식량 또는 방어구를 슬롯에 표시
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

            GetFilledSlotIndex();//currentSize (현재 아이템이 들어있는 슬롯 개수) 업데이트
       
        if (!isStorage)//가방의 경우만, 식량,방어구,가방용량 표시.
        {
            foodAmount.text = $"x{inventoryData.getFoodAmount().ToString()}";
            armorAmount.text = $"x{inventoryData.getArmorAmount().ToString()}";
            sizeText.text = $"{currentSize}/{maxSize}";
        }

#if UNITY_EDITOR
EditorUtility.SetDirty(inventoryData);//인벤토리 scriptable object에 변경사항 저장(에디터 상의 변경사항을 유지)
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
        if (!isStorage)//가방의 경우
            index += inventoryData.getFoodAmount() + inventoryData.getArmorAmount();
        currentSize = index;
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

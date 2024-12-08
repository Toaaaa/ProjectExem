using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class ShopBuyPopup : MonoBehaviour
{
    InventoryManager inventoryManager;
    [SerializeField]
    private TextMeshProUGUI textMessage;
    [SerializeField]
    private Button button1;//네 버튼
    [SerializeField]
    private Button button2;//아니오 버튼
    [SerializeField]
    private Button button3;//중앙의 닫기 버튼

    public Item item;
    public ItemData itemdata;
    public int itemQuantity;

    private void Start()
    {
        inventoryManager = GameManager.Instance.inventoryManager;
    }

    public void SetItemData(Item it, int Quantity)
    {
        item = it;
        itemdata = it.itemData;
        itemQuantity = Quantity;
    }

    private async void OnEnable()
    {
        if(inventoryManager == null)
            inventoryManager = GameManager.Instance.inventoryManager;

        if(itemdata == null)
        {
            await UniTask.WaitUntil(() => itemdata != null);

            if (itemQuantity != 0)//상점에서 해당 아이템의 수량이 0이 아닐때
            {
                if(inventoryManager.inventoryStorageData.getGold() >= itemdata.ItemPrice)//아이템을 구매할 정도의 골드가 있을때 (모든 골드는 창고에 보관된다)
                {
                    textMessage.text = LocalizationManager.Instance.GetLocalizedString("ShopText1");//key값을 통해 해당 언어의 문자열을 가져옴.
                    button1.gameObject.SetActive(true);
                    button1.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationManager.Instance.GetLocalizedString("ShopText2");
                    button2.gameObject.SetActive(true);
                    button2.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationManager.Instance.GetLocalizedString("ShopText3");
                    button3.gameObject.SetActive(false);
                }
                else
                {
                    textMessage.text = LocalizationManager.Instance.GetLocalizedString("ShopText4");
                    button1.gameObject.SetActive(false);
                    button2.gameObject.SetActive(false);
                    button3.gameObject.SetActive(true);
                    button3.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationManager.Instance.GetLocalizedString("ShopText5");
                }
            }
            else
            {
                //아이템이 0개임.. 아무런 처리 하지 않음.
            }
        }
    }
    private void OnDisable()
    {
        itemdata = null;
    }

    public void Bt1()//아이템 구매 버튼.
    {
        //사전 조건으로 이미 아이템이 0 개가 아니고, 골드가 충분한 상태를 확인함.
        inventoryManager.inventoryStorageData.setGold(inventoryManager.inventoryStorageData.getGold() - itemdata.ItemPrice);//골드 차감.
        inventoryManager.storage.AddItem(itemdata, 1);//temporary 창고에 아이템 1개 추가.
        inventoryManager.storage.ApplyToScriptable(item, inventoryManager.inventoryStorageData);//temporary 창고에 추가된 아이템을 스크립터블에 적용.
        inventoryManager.shopData.subItemQuantity(itemdata, 1);//상점의 아이템 수량 1개 차감.
        inventoryManager.shopStorageUI.UpdateUI();//상점창고 업데이트
        inventoryManager.shopUI.UpdateUI();//상점 업데이트
        GameManager.Instance.shopUIManager.UpdateGold();//골드 업데이트.
        gameObject.SetActive(false);
    }
    public void Bt2()//아니요
    {
        gameObject.SetActive(false);
    }
    public void Bt3()//닫기
    {
        gameObject.SetActive(false);
    }
    public void Close()//닫기
    {
        gameObject.SetActive(false);
    }
}

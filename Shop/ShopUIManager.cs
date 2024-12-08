using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    
    public ShopScriptableObject shopData;//상점 데이터
    //public
    public InventoryUI storageUI;//상점 UI옆에 표시될 창고 UI
    public TextMeshProUGUI goldText;//골드 표시 텍스트


    private void Start()
    {
        GameManager.Instance.shopUIManager = this;
        UpdateGold();
    }

    private void OnEnable()
    {
        //LoadShop();
        UpdateGold();
    }


    public void LoadShop()
    {
        //int j = ItemDatabase.Instance.itemDataCount();
        int j = shopData.items.Count;
        for (int i = 0; i < j; i++)
        {
            //shop.AddItem(shopData.items[i].itemData, shopData.items[i].quantity);
        }
    }

    public void UpdateGold()
    {
        goldText.text = "<sprite=0> : " + storageUI.inventoryData.getGold();
    }
    private void GoldSubPrice(int price)//.만약 상점의 아이템을 클릭시 차감되는 만큼의 골드를 표시.
    {
        goldText.text = "<sprite=0> : " + storageUI.inventoryData.getGold() + " - " + price;
        UpdateGold();
    }
}

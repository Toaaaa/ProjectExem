using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    
    public InventoryScriptableObject shopData;//���� ������
    //public
    public InventoryUI storageUI;//���� UI���� ǥ�õ� â�� UI
    public TextMeshProUGUI goldText;//��� ǥ�� �ؽ�Ʈ


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

    private void UpdateGold()
    {
        goldText.text = "<sprite=0> : " + storageUI.inventoryData.getGold();
    }
    private void GoldSubPrice(int price)//.���� ������ �������� Ŭ���� �����Ǵ� ��ŭ�� ��带 ǥ��.
    {
        goldText.text = "<sprite=0> : " + storageUI.inventoryData.getGold() + " - " + price;
        UpdateGold();
    }
}

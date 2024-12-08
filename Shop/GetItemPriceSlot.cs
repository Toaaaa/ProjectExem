using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GetItemPriceSlot : MonoBehaviour
{
    [SerializeField]
    InventorySlot inventorySlot;
    [SerializeField]
    TextMeshProUGUI priceText;

    private async void Start()
    {
        if(inventorySlot.GetItem() == null)
        {
            await UniTask.WaitUntil(() => inventorySlot.GetItem() != null);
            priceText.text = inventorySlot.GetItem().itemData.ItemPrice.ToString();
        }
        else
        {
            priceText.text = inventorySlot.GetItem().itemData.ItemPrice.ToString();
        }
    }
    private void OnEnable()
    {
        if (inventorySlot.GetItem() != null)
        {
            priceText.text = inventorySlot.GetItem().itemData.ItemPrice.ToString();
        }

    }

}

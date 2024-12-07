using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GetItemPriceSlot : MonoBehaviour
{
    [SerializeField]
    InventorySlot inventorySlot;
    TextMeshProUGUI priceText;

    private async void Start()
    {
        priceText = GetComponent<TextMeshProUGUI>();
        if(inventorySlot.GetItem() == null)
        {
            await UniTask.WaitUntil(() => inventorySlot.GetItem() != null);
            priceText.text = inventorySlot.GetItem().itemData.ItemPrice.ToString();
        }
    }

}

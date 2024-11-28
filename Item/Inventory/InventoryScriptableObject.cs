using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryData", menuName = "Inventory/InventoryData", order = 1)]
public class InventoryScriptableObject : ScriptableObject
{
    public List<Item> items = new List<Item>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryData", menuName = "Inventory/InventoryData", order = 1)]
public class InventoryScriptableObject : ScriptableObject
{
    public List<Item> items = new List<Item>();

    [SerializeField] private int foodAmount;
    [SerializeField] private int armorAmount;


    public void FoodButton(int i)
    {
        foodAmount += i;
    }
    public void ArmorButton(int i)
    {
        armorAmount += i;
    }

    public int getFoodAmount()
    {
        return foodAmount;
    }
    public int getArmorAmount()
    {
        return armorAmount;
    }
}

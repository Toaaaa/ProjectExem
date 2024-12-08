using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryData", menuName = "Inventory/InventoryData", order = 1)]
public class InventoryScriptableObject : ScriptableObject
{
    public List<Item> items = new List<Item>();

    [SerializeField] private int foodAmount;
    [SerializeField] private int armorAmount;
    [SerializeField] private int gold;


    public void FoodButton(int i)
    {
        foodAmount += i;
    }
    public void ArmorButton(int i)
    {
        armorAmount += i;
    }
    public void GoldChange(int i)
    {
        gold += i;
    }

    public int getFoodAmount()
    {
        return foodAmount;
    }
    public int getArmorAmount()
    {
        return armorAmount;
    }
    public int getGold()
    {
        return gold;
    }
    public int setGold(int i)
    {
        return gold = i;
    }
}

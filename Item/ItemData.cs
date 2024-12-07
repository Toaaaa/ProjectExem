using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string itemNameKey; // Key for localization
    [SerializeField] private string descriptionKey; // Key for localization
    [SerializeField] private ItemType type;
    [SerializeField] private Sprite icon;
    [SerializeField] private bool stackable;
    [SerializeField] private int maxStack;
    [SerializeField] private int itemPrice;
    [SerializeField] private ItemRarity rarity;



    public enum ItemType { Consumable, Equipment, Buff }
    public enum ItemRarity { Common, Rare, Legendary }

    public int ID => id;
    public string ItemNameKey => itemNameKey;
    public string DescriptionKey => descriptionKey;
    public ItemType Type => type;
    public Sprite Icon => icon;
    public bool Stackable => stackable;
    public int MaxStack => maxStack;
    public int ItemPrice => itemPrice;
    public ItemRarity Rarity => rarity;

}

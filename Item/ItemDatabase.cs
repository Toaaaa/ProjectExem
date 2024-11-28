using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    [SerializeField]
    private List<ItemData> items;

    private Dictionary<int, ItemData> itemDictionary;

    private void Awake()
    {
        Instance = this;
        itemDictionary = new Dictionary<int, ItemData>();

        foreach (var item in items)
        {
            itemDictionary.Add(item.ID, item);
        }
    }

    public ItemData GetItem(int id)
    {
        return itemDictionary.TryGetValue(id, out var item) ? item : null;
    }
    public int GetItemIndex(int id)
    {
        return items.FindIndex(x => x.ID == id);
    }
    public int itemDataCount()
    {
        return items.Count;
    }
}

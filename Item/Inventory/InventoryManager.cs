using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryScriptableObject inventoryData;//���� ������
    public InventoryScriptableObject inventoryStorageData;//â�� ������

    public Inventory bagpack;
    public Inventory storage;

    public InventoryUI bagUI;
    public InventoryUI storageUI;

    public int bagpackSize = 20;//���� ������ ��ü ���� ����
    public int bagSizeMax = 32;//���׷��̵�� �þ �� �ִ� �ִ� ũ��
    public int storageSize = 60;


    private void Start()
    {
        GameManager.Instance.inventoryManager = this;
    }
    private void OnEnable()
    {
        
    }
    /*
    public void SaveInventory()
    {
        string json = JsonUtility.ToJson(inventoryData);
        Debug.Log($"Serialized inventory data: {json}");
        System.IO.File.WriteAllText(Application.persistentDataPath + "/inventory.json", json);
        string json2 = JsonUtility.ToJson(inventoryStorageData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/inventoryStorage.json", json2);
    }

    public void LoadInventory()
    {
        string filePath = Application.persistentDataPath + "/inventory.json";
        string filePath2 = Application.persistentDataPath + "/inventoryStorage.json";
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, inventoryData);
            string json2 = System.IO.File.ReadAllText(filePath2);
            JsonUtility.FromJsonOverwrite(json2, inventoryStorageData);
        }
    }
    */

    public void LoadInventory()
    {
        //int j = ItemDatabase.Instance.itemDataCount();
        int j = inventoryData.items.Count;
        for (int i = 0; i < j; i++)
        {
            bagpack.AddItem(inventoryData.items[i].itemData, inventoryData.items[i].quantity);
        }
    }
    public void LoadStorage()
    {
        //int j = ItemDatabase.Instance.itemDataCount();
        int j = inventoryStorageData.items.Count;
        for (int i = 0; i < j; i++)
        {
            storage.AddItem(inventoryStorageData.items[i].itemData, inventoryStorageData.items[i].quantity);
        }
    }
    public void UpdateUIBoth()
    {
        bagUI.UpdateUI();
        storageUI.UpdateUI();
    }
}

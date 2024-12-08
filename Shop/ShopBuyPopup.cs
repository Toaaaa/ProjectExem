using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class ShopBuyPopup : MonoBehaviour
{
    InventoryManager inventoryManager;
    [SerializeField]
    private TextMeshProUGUI textMessage;
    [SerializeField]
    private Button button1;//�� ��ư
    [SerializeField]
    private Button button2;//�ƴϿ� ��ư
    [SerializeField]
    private Button button3;//�߾��� �ݱ� ��ư

    public Item item;
    public ItemData itemdata;
    public int itemQuantity;

    private void Start()
    {
        inventoryManager = GameManager.Instance.inventoryManager;
    }

    public void SetItemData(Item it, int Quantity)
    {
        item = it;
        itemdata = it.itemData;
        itemQuantity = Quantity;
    }

    private async void OnEnable()
    {
        if(inventoryManager == null)
            inventoryManager = GameManager.Instance.inventoryManager;

        if(itemdata == null)
        {
            await UniTask.WaitUntil(() => itemdata != null);

            if (itemQuantity != 0)//�������� �ش� �������� ������ 0�� �ƴҶ�
            {
                if(inventoryManager.inventoryStorageData.getGold() >= itemdata.ItemPrice)//�������� ������ ������ ��尡 ������ (��� ���� â�� �����ȴ�)
                {
                    textMessage.text = LocalizationManager.Instance.GetLocalizedString("ShopText1");//key���� ���� �ش� ����� ���ڿ��� ������.
                    button1.gameObject.SetActive(true);
                    button1.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationManager.Instance.GetLocalizedString("ShopText2");
                    button2.gameObject.SetActive(true);
                    button2.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationManager.Instance.GetLocalizedString("ShopText3");
                    button3.gameObject.SetActive(false);
                }
                else
                {
                    textMessage.text = LocalizationManager.Instance.GetLocalizedString("ShopText4");
                    button1.gameObject.SetActive(false);
                    button2.gameObject.SetActive(false);
                    button3.gameObject.SetActive(true);
                    button3.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationManager.Instance.GetLocalizedString("ShopText5");
                }
            }
            else
            {
                //�������� 0����.. �ƹ��� ó�� ���� ����.
            }
        }
    }
    private void OnDisable()
    {
        itemdata = null;
    }

    public void Bt1()//������ ���� ��ư.
    {
        //���� �������� �̹� �������� 0 ���� �ƴϰ�, ��尡 ����� ���¸� Ȯ����.
        inventoryManager.inventoryStorageData.setGold(inventoryManager.inventoryStorageData.getGold() - itemdata.ItemPrice);//��� ����.
        inventoryManager.storage.AddItem(itemdata, 1);//temporary â�� ������ 1�� �߰�.
        inventoryManager.storage.ApplyToScriptable(item, inventoryManager.inventoryStorageData);//temporary â�� �߰��� �������� ��ũ���ͺ� ����.
        inventoryManager.shopData.subItemQuantity(itemdata, 1);//������ ������ ���� 1�� ����.
        inventoryManager.shopStorageUI.UpdateUI();//����â�� ������Ʈ
        inventoryManager.shopUI.UpdateUI();//���� ������Ʈ
        GameManager.Instance.shopUIManager.UpdateGold();//��� ������Ʈ.
        gameObject.SetActive(false);
    }
    public void Bt2()//�ƴϿ�
    {
        gameObject.SetActive(false);
    }
    public void Bt3()//�ݱ�
    {
        gameObject.SetActive(false);
    }
    public void Close()//�ݱ�
    {
        gameObject.SetActive(false);
    }
}

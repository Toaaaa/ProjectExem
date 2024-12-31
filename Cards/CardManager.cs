using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour //���� ����, �Ʒü� ui���� ����� ī�� �����͸� �����ϴ� �Ŵ���.
{
    //��ų ī�� + ���� ���ο��� ȹ���ϴ� ��� ī��(���� ���) + ���濡 ������ ������ ��� ī��
    public CardDatabase cardDatabase;//ī�� �����ͺ��̽�, ���ӳ� �����ϴ� ��� ī�嵥���� ����.

    public List<CardData> cardDataList;//��ų ī�� ������
    public List<CardData> dungeonCardList;//�������� ȹ���� ī�� ������(cardData�� utility�� �ش���)
    public List<CardData> itemCardList;//���濡 ������ ������ ī�� ������

    public List<CardData> availableCardList;// ��� ������ ī�� ����Ʈ(���õ� �������� ���� ���� ī��� ����)
    public CardHoldData cardHoldData;//�п� �������� ī�� �����͸� �����ϴ� scriptable object. ���� ù ����� �ʱ�ȭ.

    public bool isCardUpdate;//ó���� ī�尡 ������Ʈ�� �� ���� ������ �۾��� �����ϵ��� �ϴ� ����.

    private void Start()
    {
        //���濡 ������ �������� ī��� ��ȯ�� itemCardList�� �߰��ϴ� ��ũ��Ʈ ���� �߰�.
        GameManager.Instance.cardManager = this;
        dungeonCardList = new List<CardData>();
        itemCardList = new List<CardData>();

        UpdateAvailableCards();
    }

    // ��� ������ ī�� ������Ʈ
    public void UpdateAvailableCards()
    {
        cardDataList = cardDatabase.cardDataListData;
        availableCardList = cardDataList.FindAll(card => !card.islocked);
        availableCardList.AddRange(dungeonCardList.FindAll(card => !card.islocked));
        availableCardList.AddRange(itemCardList.FindAll(card => !card.islocked));
        //������ �������� ī��� ��ȯ�ϴ� ��ũ��Ʈ ���� �߰�.

        isCardUpdate = true;
    }
    public void InitializeDeck()//���� �����(���� 1������) CardHoldData�� �ʱ�ȭ�ϴ� �޼���.
    {
        dungeonCardList = new List<CardData>();
        itemCardList = new List<CardData>();
        ItemCardUpdate();        
        DeckShuffle();
    }
    public void DeckShuffle()//ī�� �и� ���� �̾��ִ� �ż���.
    {
        //���� Ȯ���� ���� availableī���� ����Ʈ ������, ���� ī��(cardHoldData)�� �������ش�
    }
    void ItemCardUpdate()//���濡 ������ ������ ī�带 ������Ʈ.
    {
        GameManager.Instance.inventoryManager.inventoryData.items.ForEach(item =>
        {
            for(int i = 0; i < item.quantity; i++)
            {
                itemCardList.Add(cardDatabase.GetItemCard(item.itemData.ID));
            }
        });
    }

    public void ItemCardUsed(CardData card)//������ ī�� ���� ȣ��Ǵ� �޼���.
    {
        //������ ī�� ����,
        //1.�������� sfx �۵� 2.�������� ��� ���� 3.itemCardList���� �ش� ī�带 �������ش�.

        //sfx �۵�

        //�������� ��� ����
        switch (card.cardID)
        {
            case 0://
                
                break;
        }

        //ī�� ����
        for (int i = 0; i < itemCardList.Count; i++) 
        { 
            if (itemCardList[i].cardID == card.cardID) 
            { 
                itemCardList.RemoveAt(i); 
                break; 
            } 
        }
    }




    // Ư�� ī�� ��� ����
    public void UnlockCard(CardData cardData)
    {
        if (cardData.islocked)
        {
            cardData.islocked = false;
            availableCardList.Add(cardData);
        }
    }

    // Ư�� ī�� ���
    public void LockCard(CardData cardData)
    {
        if (!cardData.islocked)
        {
            cardData.islocked = true;
            availableCardList.Remove(cardData);
        }
    }
}

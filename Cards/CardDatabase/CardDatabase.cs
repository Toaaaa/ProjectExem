using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "ScriptableObjects/CardDatabase", order = 1)]
public class CardDatabase : ScriptableObject
{
    public List<CardData> cardDataListData;//��ų ī�� ������
    public List<CardData> dungeonCardListData;//�������� ȹ�� ������ ī�� ������(cardData�� utility�� �ش���)'
    public List<CardData> itemCardListData;//������ ī�� ������

    public CardData GetItemCard(int id)
    {
        if(id < 0 || id >= itemCardListData.Count)
        {
            Debug.LogError("Item ID is out of range");
            return null;
        }
        return itemCardListData[id];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "ScriptableObjects/CardDatabase", order = 1)]
public class CardDatabase : ScriptableObject
{
    public List<CardData> cardDataListData;//스킬 카드 데이터
    public List<CardData> dungeonCardListData;//던전에서 획득 가능한 카드 데이터(cardData에 utility에 해당함)'
    public List<CardData> itemCardListData;//아이템 카드 데이터

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

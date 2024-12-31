using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public CardHoldData cardHoldData;//보유중인 카드 데이터를 저장하는 scriptable object. 던전 첫 입장시 초기화.
    public List<CardData> cardsHolding;
    public List<Cards> cards;//보유중인 카드의 prefab데이터.

    private void Start()
    {
        InitializeCards();//전투 맵 시작시 덱의 카드를 세팅해준다.
    }

    // 카드 초기화 메서드
    public void InitializeCards()
    {
        cardsHolding = cardHoldData.cardsHolding;
        cards = new List<Cards>();
        /*
        foreach (CardData cardData in cardsHolding)
        {
            AddCard(cardData); //cardsHolding 리스트에 보유중인 cardData들을 추가.
        }*/
    }


    // 패에 새로운 카드를 추가
    public void AddCard(CardData cardData)
    {
        cardsHolding.Add(cardData);
        GameObject cardObject = Instantiate(cardData.cardPrefab, transform);
        Cards cardComponent = cardObject.GetComponent<Cards>();
        cards.Add(cardComponent);
    }

    // 패에서 카드를 제거
    public void RemoveCard(CardData cardData)
    {
        Cards cardToRemove = cards.Find(c => c.cardData == cardData);
        if (cardToRemove != null)
        {
            cards.Remove(cardToRemove);
            cardsHolding.Remove(cardData);
            Destroy(cardToRemove.gameObject);
        }
    }
}

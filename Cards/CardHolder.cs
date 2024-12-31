using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public CardHoldData cardHoldData;//�������� ī�� �����͸� �����ϴ� scriptable object. ���� ù ����� �ʱ�ȭ.
    public List<CardData> cardsHolding;
    public List<Cards> cards;//�������� ī���� prefab������.

    private void Start()
    {
        InitializeCards();//���� �� ���۽� ���� ī�带 �������ش�.
    }

    // ī�� �ʱ�ȭ �޼���
    public void InitializeCards()
    {
        cardsHolding = cardHoldData.cardsHolding;
        cards = new List<Cards>();
        /*
        foreach (CardData cardData in cardsHolding)
        {
            AddCard(cardData); //cardsHolding ����Ʈ�� �������� cardData���� �߰�.
        }*/
    }


    // �п� ���ο� ī�带 �߰�
    public void AddCard(CardData cardData)
    {
        cardsHolding.Add(cardData);
        GameObject cardObject = Instantiate(cardData.cardPrefab, transform);
        Cards cardComponent = cardObject.GetComponent<Cards>();
        cards.Add(cardComponent);
    }

    // �п��� ī�带 ����
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardHoldData", menuName = "ScriptableObjects/CardHoldData", order = 1)]
public class CardHoldData : ScriptableObject //cardHolder의 데이터를 저장하는 scriptable object. 던전입장시 매번 초기화.
{
    public List<CardData> cardsHolding;//보유중인 카드를 저장하는 리스트.
}

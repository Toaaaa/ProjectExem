using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoldData : ScriptableObject //cardHolder의 데이터를 저장하는 scriptable object.
{
    public List<CardData> cardsHolding;//보유중인 카드를 저장하는 리스트.
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardHoldData", menuName = "ScriptableObjects/CardHoldData", order = 1)]
public class CardHoldData : ScriptableObject //cardHolder�� �����͸� �����ϴ� scriptable object. ��������� �Ź� �ʱ�ȭ.
{
    public List<CardData> cardsHolding;//�������� ī�带 �����ϴ� ����Ʈ.
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cards : MonoBehaviour //ī�� �����͸� �����ͼ� �����ϴ� prefab ������ == ���ӿ��� ����� ī�� ������Ʈ.
{
    public CardData cardData;

    public Sprite cardImage;
    public TextMeshProUGUI cardName;

    private void Start()
    {
        InitializeCard();
    }

    // ī���� UI �� ��� �ʱ�ȭ
    public void InitializeCard()
    {
        if (cardData != null)
        {
            // ī�� UI ���� ����
            cardName.text = cardData.cardName;
            cardImage = cardData.cardImage;
        }
    }

    // ī�� ȿ�� �ߵ�
    public void UseCard()
    {
        
    }
}

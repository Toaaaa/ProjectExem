using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cards : MonoBehaviour //카드 데이터를 가져와서 구현하는 prefab 데이터 == 게임에서 사용할 카드 오브젝트.
{
    public CardData cardData;

    public Sprite cardImage;
    public TextMeshProUGUI cardName;

    private void Start()
    {
        InitializeCard();
    }

    // 카드의 UI 및 기능 초기화
    public void InitializeCard()
    {
        if (cardData != null)
        {
            // 카드 UI 설정 예시
            cardName.text = cardData.cardName;
            cardImage = cardData.cardImage;
        }
    }

    // 카드 효과 발동
    public void UseCard()
    {
        
    }
}

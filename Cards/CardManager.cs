using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour //던전 씬과, 훈련소 ui에서 사용할 카드 데이터를 관리하는 매니저.
{
    //스킬 카드 + 던전 내부에서 획득하는 사용 카드(여러 기능) + 가방에 보유한 아이템 사용 카드
    public List<CardData> cardDataList;//스킬 카드 데이터
    public List<CardData> dungeonCardList;//던전에서 획득하는 카드 데이터(cardData에 utility에 해당함)
    public List<CardData> itemCardList;//가방에 보유한 아이템 카드 데이터

    public List<CardData> availableCardList;// 사용 가능한 카드 리스트(숙련도 부족으로 얻지 못한 카드는 제외)

    public bool isCardUpdate;//처음에 카드가 업데이트가 된 이후 나머지 작업을 진행하도록 하는 변수.

    private void Start()
    {
        //가방에 보유한 아이템을 카드로 변환후 itemCardList에 추가하는 스크립트 추후 추가.
        UpdateAvailableCards();
    }

    // 사용 가능한 카드 업데이트
    public void UpdateAvailableCards()
    {
        availableCardList = cardDataList.FindAll(card => !card.islocked);
        availableCardList.AddRange(dungeonCardList.FindAll(card => !card.islocked));
        //가방의 아이템을 카드로 변환하는 스크립트 추후 추가.

        isCardUpdate = true;
    }

    // 특정 카드 잠금 해제
    public void UnlockCard(CardData cardData)
    {
        if (cardData.islocked)
        {
            cardData.islocked = false;
            availableCardList.Add(cardData);
        }
    }

    // 특정 카드 잠금
    public void LockCard(CardData cardData)
    {
        if (!cardData.islocked)
        {
            cardData.islocked = true;
            availableCardList.Remove(cardData);
        }
    }
}

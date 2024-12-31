using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour //던전 씬과, 훈련소 ui에서 사용할 카드 데이터를 관리하는 매니저.
{
    //스킬 카드 + 던전 내부에서 획득하는 사용 카드(여러 기능) + 가방에 보유한 아이템 사용 카드
    public CardDatabase cardDatabase;//카드 데이터베이스, 게임내 존재하는 모든 카드데이터 저장.

    public List<CardData> cardDataList;//스킬 카드 데이터
    public List<CardData> dungeonCardList;//던전에서 획득한 카드 데이터(cardData에 utility에 해당함)
    public List<CardData> itemCardList;//가방에 보유한 아이템 카드 데이터

    public List<CardData> availableCardList;// 사용 가능한 카드 리스트(숙련도 부족으로 얻지 못한 카드는 제외)
    public CardHoldData cardHoldData;//패에 소지중인 카드 데이터를 저장하는 scriptable object. 던전 첫 입장시 초기화.

    public bool isCardUpdate;//처음에 카드가 업데이트가 된 이후 나머지 작업을 진행하도록 하는 변수.

    private void Start()
    {
        //가방에 보유한 아이템을 카드로 변환후 itemCardList에 추가하는 스크립트 추후 추가.
        GameManager.Instance.cardManager = this;
        dungeonCardList = new List<CardData>();
        itemCardList = new List<CardData>();

        UpdateAvailableCards();
    }

    // 사용 가능한 카드 업데이트
    public void UpdateAvailableCards()
    {
        cardDataList = cardDatabase.cardDataListData;
        availableCardList = cardDataList.FindAll(card => !card.islocked);
        availableCardList.AddRange(dungeonCardList.FindAll(card => !card.islocked));
        availableCardList.AddRange(itemCardList.FindAll(card => !card.islocked));
        //가방의 아이템을 카드로 변환하는 스크립트 추후 추가.

        isCardUpdate = true;
    }
    public void InitializeDeck()//던전 입장시(던전 1층에서) CardHoldData를 초기화하는 메서드.
    {
        dungeonCardList = new List<CardData>();
        itemCardList = new List<CardData>();
        ItemCardUpdate();        
        DeckShuffle();
    }
    public void DeckShuffle()//카드 패를 새로 뽑아주는 매서드.
    {
        //일정 확률에 따라 available카드의 리스트 내에서, 패의 카드(cardHoldData)를 세팅해준다
    }
    void ItemCardUpdate()//가방에 보유한 아이템 카드를 업데이트.
    {
        GameManager.Instance.inventoryManager.inventoryData.items.ForEach(item =>
        {
            for(int i = 0; i < item.quantity; i++)
            {
                itemCardList.Add(cardDatabase.GetItemCard(item.itemData.ID));
            }
        });
    }

    public void ItemCardUsed(CardData card)//아이템 카드 사용시 호출되는 메서드.
    {
        //아이템 카드 사용시,
        //1.아이템의 sfx 작동 2.아이템의 기능 적용 3.itemCardList에서 해당 카드를 제거해준다.

        //sfx 작동

        //아이템의 기능 적용
        switch (card.cardID)
        {
            case 0://
                
                break;
        }

        //카드 제거
        for (int i = 0; i < itemCardList.Count; i++) 
        { 
            if (itemCardList[i].cardID == card.cardID) 
            { 
                itemCardList.RemoveAt(i); 
                break; 
            } 
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card/Create New Card")]
public class CardData : ScriptableObject
{
    public Cards cardPrefab;//카드의 프리팹.

    public enum CardType { Attack, Utility, Items };// 공격, 유틸리티(던전 내부에서 획득), 아이템으로 나뉨. (이걸로 색상을 나눌지, 카드 사용 주체로 색상을 나눌지 미정)
    public enum CardUseSubject { Item, Warrior, Mage };// 아이템, 전사, 마법사로 나뉨. (이걸로 색상을 나눌지, 카드 타입으로 색상을 나눌지 미정)
    public CardType cardType;
    public CardUseSubject cardUseSubject;
    public int cardID;           // 고유 ID 
    public string cardName;  // 카드 이름 (key값으로 존재)
    public string cardDescription;   // 카드 설명 (key값으로 존재하며)
    public Sprite cardImage;        // 스킬의 이미지
    public float param1;              // 파라미터1(스킬 계수)
    public int energyCost;          // 스킬 코스트
    //////카드의 분류//////
    public bool isPysical;          // 물리,마법
    public bool isPenetrate;        // 관통,일반
    public bool isBlunt;            // 둔기,베기 (전사의 경우만 해당) >> CardUseSubject가 Warrior일 경우에만 사용
    public int chainCount;          // 해당 카드에 저장된 체인 (마법사의 경우만 해당) >> CardUseSubject가 Mage일 경우에만 사용
    public enum CardTargetType { SingleEnemy, AllEnemies, Player, AllPlayer }
    public List<CardEffect> effects; // 카드 효과 리스트
    public List<CardTags> tags;       // 카드 태그 리스트
    //////카드 언락//////
    public bool islocked;          // 잠김 여부

    public void GiveData()//prefab을 처음 instantiate할 때, cardData를
    {
        cardPrefab.cardData = this;
    }
}

[System.Serializable]
public class CardEffect//카드 사용시 해당 클래스의 데이터를 참고하여 CardManager의 개별 메서드에서 효과를 적용시킴.
{
    public enum EffectType { Damage, Block, Heal, Buff, Stun, Invincible, Burn, Electric, Slowed, DamageWithHeal}//invincible은 시전시 일정시간동안 무적이 됨.
    public EffectType effectType;
    public int value; //효과의 수치 (param1 * character의 공격력) //카드가 prefab으로 생성될 때, 이 값을 초기화 해줌.
    public float duration;

    public void UpdateValue(float param1, int atk, int extraAtk) //extraAtk는 아티팩트나 추가 아이템이 있을경우 계수와 관계없이 추가되는 공격력.
    {
        value = (int)(param1 * atk) + extraAtk;
    }
}
[System.Serializable]
public class CardTags
{
    public enum TagType {Penetrate, Magic, Multi } // 관통공격, 마법공격, 다중공격, 유틸리티(힐,버프,디버프,치명타 등..)
    public TagType tagType;
    //public string tagDescription;// 태그 설명(key값으로 존재)
}
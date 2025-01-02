using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card/Create New Card")]
public class CardData : ScriptableObject
{
    public Cards cardPrefab;//ī���� ������.

    public enum CardType { Attack, Utility, Items };// ����, ��ƿ��Ƽ(���� ���ο��� ȹ��), ���������� ����. (�̰ɷ� ������ ������, ī�� ��� ��ü�� ������ ������ ����)
    public enum CardUseSubject { Item, Warrior, Mage };// ������, ����, ������� ����. (�̰ɷ� ������ ������, ī�� Ÿ������ ������ ������ ����)
    public CardType cardType;
    public CardUseSubject cardUseSubject;
    public int cardID;           // ���� ID 
    public string cardName;  // ī�� �̸� (key������ ����)
    public string cardDescription;   // ī�� ���� (key������ �����ϸ�)
    public Sprite cardImage;        // ��ų�� �̹���
    public float param1;              // �Ķ����1(��ų ���)
    public int energyCost;          // ��ų �ڽ�Ʈ
    //////ī���� �з�//////
    public bool isPysical;          // ����,����
    public bool isPenetrate;        // ����,�Ϲ�
    public bool isBlunt;            // �б�,���� (������ ��츸 �ش�) >> CardUseSubject�� Warrior�� ��쿡�� ���
    public int chainCount;          // �ش� ī�忡 ����� ü�� (�������� ��츸 �ش�) >> CardUseSubject�� Mage�� ��쿡�� ���
    public enum CardTargetType { SingleEnemy, AllEnemies, Player, AllPlayer }
    public List<CardEffect> effects; // ī�� ȿ�� ����Ʈ
    public List<CardTags> tags;       // ī�� �±� ����Ʈ
    //////ī�� ���//////
    public bool islocked;          // ��� ����

    public void GiveData()//prefab�� ó�� instantiate�� ��, cardData��
    {
        cardPrefab.cardData = this;
    }
}

[System.Serializable]
public class CardEffect//ī�� ���� �ش� Ŭ������ �����͸� �����Ͽ� CardManager�� ���� �޼��忡�� ȿ���� �����Ŵ.
{
    public enum EffectType { Damage, Block, Heal, Buff, Stun, Invincible, Burn, Electric, Slowed, DamageWithHeal}//invincible�� ������ �����ð����� ������ ��.
    public EffectType effectType;
    public int value; //ȿ���� ��ġ (param1 * character�� ���ݷ�) //ī�尡 prefab���� ������ ��, �� ���� �ʱ�ȭ ����.
    public float duration;

    public void UpdateValue(float param1, int atk, int extraAtk) //extraAtk�� ��Ƽ��Ʈ�� �߰� �������� ������� ����� ������� �߰��Ǵ� ���ݷ�.
    {
        value = (int)(param1 * atk) + extraAtk;
    }
}
[System.Serializable]
public class CardTags
{
    public enum TagType {Penetrate, Magic, Multi } // �������, ��������, ���߰���, ��ƿ��Ƽ(��,����,�����,ġ��Ÿ ��..)
    public TagType tagType;
    //public string tagDescription;// �±� ����(key������ ����)
}
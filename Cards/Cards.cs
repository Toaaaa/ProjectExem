using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CardEffect;

public class Cards : MonoBehaviour //ī�� �����͸� �����ͼ� �����ϴ� prefab ������ == ���ӿ��� ����� ī�� ������Ʈ.
{
    public CardData cardData;

    public Sprite cardImage;
    public TextMeshProUGUI cardName;

    public TextMeshProUGUI cardCost;
    public TextMeshProUGUI cardDescription;

    private void Start()
    {
        InitializeCard();
    }

    // ī���� UI �� ��� �ʱ�ȭ
    public void InitializeCard()
    {
        if (cardData != null)
        {
            //���� �̹����� �̸��� ������ ��
            cardName.text = LocalizationManager.Instance.GetLocalizedString(cardData.cardName);
            cardImage = cardData.cardImage;

            //ī���� �ڽ�Ʈ�� ���� + �±׸� �����´�
            SetCardCostAndDescription();
        }
        else
        {
            StartCoroutine(RetryInitializeCard());
        }
    }

    // ī���� �ڽ�Ʈ�� ���� ����
    public void SetCardCostAndDescription()
    {
        cardCost.text = cardData.energyCost.ToString();//���Ŀ��� ����� �߰������� �ϴ��� �ٷ� �ڽ�Ʈ ����
        cardDescription.text = DescTextWithParam();
    }
    public string DescTextWithParam()
    {
        if(cardData.effects.Count == 1)
        {
            int param;
            if (cardData.effects[0].effectType == EffectType.Damage || 
                cardData.effects[0].effectType == EffectType.Heal || 
                cardData.effects[0].effectType == EffectType.DamageWithHeal)//������,��,������+�� ��ų�� ���, param�� value���� �����´�.
            {
                param = cardData.effects[0].value;
            }
            else
            {
                param = (int)cardData.effects[0].duration;
            }
            //�Ķ���� 1���� ���ö������ �ؽ�Ʈ ����.
            return LocalizationManager.Instance.GetLocalizedFormattedString(cardData.cardDescription,param);
        }
        else//ī���� ȿ���� 2���� ���.
        {
            int param1;
            int param2;

            if (cardData.effects[0].effectType == EffectType.Damage ||
                cardData.effects[0].effectType == EffectType.Heal ||
                cardData.effects[0].effectType == EffectType.DamageWithHeal)//������,��,������+�� ��ų�� ���, param�� value���� �����´�.
            {
                param1 = cardData.effects[0].value;
            }
            else
            {
                param1 = (int)cardData.effects[0].duration;
            }
            if (cardData.effects[1].effectType == EffectType.Damage ||
                cardData.effects[1].effectType == EffectType.Heal ||
                cardData.effects[1].effectType == EffectType.DamageWithHeal)//������,��,������+�� ��ų�� ���, param�� value���� �����´�.
            {
                param2 = cardData.effects[1].value;
            }
            else
            {
                param2 = (int)cardData.effects[1].duration;
            }
            //�Ķ���� 2���� ���ö������ �ؽ�Ʈ ����.
            return LocalizationManager.Instance.GetLocalizedFormattedString(cardData.cardDescription, param1, param2);
        }
    }
    // ī�� ȿ�� �ߵ�
    public void UseCard()
    {

    }

    private IEnumerator RetryInitializeCard()
    {
        yield return null;
        InitializeCard();
    }
}

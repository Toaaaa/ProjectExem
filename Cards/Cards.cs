using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CardEffect;

public class Cards : MonoBehaviour //카드 데이터를 가져와서 구현하는 prefab 데이터 == 게임에서 사용할 카드 오브젝트.
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

    // 카드의 UI 및 기능 초기화
    public void InitializeCard()
    {
        if (cardData != null)
        {
            //먼저 이미지와 이름을 가져온 뒤
            cardName.text = LocalizationManager.Instance.GetLocalizedString(cardData.cardName);
            cardImage = cardData.cardImage;

            //카드의 코스트와 설명 + 태그를 가져온다
            SetCardCostAndDescription();
        }
        else
        {
            StartCoroutine(RetryInitializeCard());
        }
    }

    // 카드의 코스트와 설명 설정
    public void SetCardCostAndDescription()
    {
        cardCost.text = cardData.energyCost.ToString();//추후에는 기믹을 추가하지만 일단은 바로 코스트 대입
        cardDescription.text = DescTextWithParam();
    }
    public string DescTextWithParam()
    {
        if(cardData.effects.Count == 1)
        {
            int param;
            if (cardData.effects[0].effectType == EffectType.Damage || 
                cardData.effects[0].effectType == EffectType.Heal || 
                cardData.effects[0].effectType == EffectType.DamageWithHeal)//데미지,힐,데미지+힐 스킬일 경우, param은 value값을 가져온다.
            {
                param = cardData.effects[0].value;
            }
            else
            {
                param = (int)cardData.effects[0].duration;
            }
            //파라미터 1개의 로컬라이즈된 텍스트 리턴.
            return LocalizationManager.Instance.GetLocalizedFormattedString(cardData.cardDescription,param);
        }
        else//카드의 효과가 2개일 경우.
        {
            int param1;
            int param2;

            if (cardData.effects[0].effectType == EffectType.Damage ||
                cardData.effects[0].effectType == EffectType.Heal ||
                cardData.effects[0].effectType == EffectType.DamageWithHeal)//데미지,힐,데미지+힐 스킬일 경우, param은 value값을 가져온다.
            {
                param1 = cardData.effects[0].value;
            }
            else
            {
                param1 = (int)cardData.effects[0].duration;
            }
            if (cardData.effects[1].effectType == EffectType.Damage ||
                cardData.effects[1].effectType == EffectType.Heal ||
                cardData.effects[1].effectType == EffectType.DamageWithHeal)//데미지,힐,데미지+힐 스킬일 경우, param은 value값을 가져온다.
            {
                param2 = cardData.effects[1].value;
            }
            else
            {
                param2 = (int)cardData.effects[1].duration;
            }
            //파라미터 2개의 로컬라이즈된 텍스트 리턴.
            return LocalizationManager.Instance.GetLocalizedFormattedString(cardData.cardDescription, param1, param2);
        }
    }
    // 카드 효과 발동
    public void UseCard()
    {

    }

    private IEnumerator RetryInitializeCard()
    {
        yield return null;
        InitializeCard();
    }
}

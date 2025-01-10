using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterData : MonoBehaviour //조안나와 레이에게 상속시켜줄 캐릭터 데이터.
{
    [SerializeField]
    CharacterScriptable characterData;

    private void Start()
    {
        currentHealth = characterData.currentHealth;
        maxHealth = characterData.maxHealth;
        attackPower = characterData.attackPower;
        defense = characterData.defense;
        speed = characterData.speed;
        stamina = characterData.stamina;
        maxStamina = characterData.maxStamina;
        currenChain = characterData.currenChain;
        isDead = characterData.isDead;
        isStunned = characterData.isStunned;
        isSilenced = characterData.isSilenced;
        isPoisoned = characterData.isPoisoned;
        isBurning = characterData.isBurning;
        isFrozen = characterData.isFrozen;
        isShielded = characterData.isShielded;
        shieldTime = characterData.shieldTime;
    }

    public int currentHealth;
    public int maxHealth;
    public int attackPower;
    public int defense;
    public int speed;
    public int stamina;
    public int maxStamina;
    public int currenChain;//마법사가 사용(현재 스킬 체인 수)
    public int skillUnlocked;//기본 : 0, 1~3번까지 스킬이 언락가능.
    public bool isDead;
    public bool isStunned;
    public bool isSilenced;
    public bool isPoisoned;
    public bool isBurning;
    public bool isFrozen;
    public bool isShielded;
    public int shieldTime;//쉴드 남은시간


    //////////////////////////////////////
    //플레이어의 스테이터스를 업데이트하는 데이터.

    public Image playerSprite;
    public Slider hpBar;
    public Slider staminaBar;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI staminaText; //조안나는 stamina, 레이는 mana를 사용 하지만, 해당 스크립트 에서는 편의상 stamina로 통일.

    private bool OnShake;
    //////////////////////////////////////
    //전투시스템 에서 사용하는 데이터.
    public GameObject[] targetMonster;//공격할 몬스터를 저장하는 배열.
    public ExtraSkill[] extraSkills;//플레이어가 사용할 수 있는 추가 스킬들을 저장하는 배열.//주로 버프 형식의 이펙트
    public bool isMagicShield;//마법방어막이 활성화 되어있는지 확인하는 변수.


    private void Awake()
    {
        isMagicShield = false;
    }
    private void Update()
    {

        if (hpBar.value != currentHealth / maxHealth)
        {
            PlayerHpUpdate();
        }
        if (staminaBar.value != stamina / maxStamina)
        {
            PlayerStaminaUpdate();
        }
        hpText.text = (int)currentHealth + "/" + maxHealth;
        staminaText.text = (int)stamina + "/" + maxStamina;


    }

    private void PlayerHpUpdate()
    {
        if (hpBar.value > currentHealth / maxHealth)//체력이 감소했을때
        {
            StartCoroutine(HpReduce());
        }
        else if (hpBar.value < currentHealth / maxHealth)//체력이 회복했을때
        {
            StartCoroutine(HpIncrease());
        }
    }
    private void PlayerStaminaUpdate()
    {
        if (staminaBar.value > stamina / maxStamina)//스태미나가 감소했을때
        {
            StartCoroutine(StaminaReduce());
        }
        else if (staminaBar.value < stamina / maxStamina)//스태미나가 회복했을때
        {
            StartCoroutine(StaminaIncrease());
        }
    }
    IEnumerator HpReduce()
    {
        //체력바 줄이기.
        if (OnShake == false)
            StartCoroutine(HpBarShake());
        while (hpBar.value > currentHealth / maxHealth)
        {
            hpBar.value -= 0.001f;
            if (hpBar.value < currentHealth / maxHealth)
            {
                hpBar.value = currentHealth / maxHealth;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator HpIncrease()
    {
        //체력바 늘리기.
        while (hpBar.value < currentHealth / maxHealth)
        {
            hpBar.value += 0.001f;
            if (hpBar.value > currentHealth / maxHealth)
            {
                hpBar.value = currentHealth / maxHealth;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator StaminaReduce()
    {
        //스태미나바 줄이기.
        while (staminaBar.value > stamina / maxStamina)
        {
            staminaBar.value -= 0.001f;
            if (staminaBar.value < stamina / maxStamina)
            {
                staminaBar.value = stamina / maxStamina;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator StaminaIncrease()
    {
        //스태미나바 늘리기.
        while (staminaBar.value < stamina / maxStamina)
        {
            staminaBar.value += 0.001f;
            if (staminaBar.value > stamina / maxStamina)
            {
                staminaBar.value = stamina / maxStamina;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    IEnumerator HpBarShake()//피해를 입었을때 체력바 흔들리는 효과
    {
        Debug.Log("데미지 입음");
        OnShake = true;
        hpBar.gameObject.transform.DOPunchPosition(new Vector3(5.5f, 0, 0), 1f, 10, 1);
        yield return new WaitForSeconds(1f);
        OnShake = false;
    }

}

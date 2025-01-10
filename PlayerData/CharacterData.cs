using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterData : MonoBehaviour //���ȳ��� ���̿��� ��ӽ����� ĳ���� ������.
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
    public int currenChain;//�����簡 ���(���� ��ų ü�� ��)
    public int skillUnlocked;//�⺻ : 0, 1~3������ ��ų�� �������.
    public bool isDead;
    public bool isStunned;
    public bool isSilenced;
    public bool isPoisoned;
    public bool isBurning;
    public bool isFrozen;
    public bool isShielded;
    public int shieldTime;//���� �����ð�


    //////////////////////////////////////
    //�÷��̾��� �������ͽ��� ������Ʈ�ϴ� ������.

    public Image playerSprite;
    public Slider hpBar;
    public Slider staminaBar;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI staminaText; //���ȳ��� stamina, ���̴� mana�� ��� ������, �ش� ��ũ��Ʈ ������ ���ǻ� stamina�� ����.

    private bool OnShake;
    //////////////////////////////////////
    //�����ý��� ���� ����ϴ� ������.
    public GameObject[] targetMonster;//������ ���͸� �����ϴ� �迭.
    public ExtraSkill[] extraSkills;//�÷��̾ ����� �� �ִ� �߰� ��ų���� �����ϴ� �迭.//�ַ� ���� ������ ����Ʈ
    public bool isMagicShield;//�������� Ȱ��ȭ �Ǿ��ִ��� Ȯ���ϴ� ����.


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
        if (hpBar.value > currentHealth / maxHealth)//ü���� ����������
        {
            StartCoroutine(HpReduce());
        }
        else if (hpBar.value < currentHealth / maxHealth)//ü���� ȸ��������
        {
            StartCoroutine(HpIncrease());
        }
    }
    private void PlayerStaminaUpdate()
    {
        if (staminaBar.value > stamina / maxStamina)//���¹̳��� ����������
        {
            StartCoroutine(StaminaReduce());
        }
        else if (staminaBar.value < stamina / maxStamina)//���¹̳��� ȸ��������
        {
            StartCoroutine(StaminaIncrease());
        }
    }
    IEnumerator HpReduce()
    {
        //ü�¹� ���̱�.
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
        //ü�¹� �ø���.
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
        //���¹̳��� ���̱�.
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
        //���¹̳��� �ø���.
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


    IEnumerator HpBarShake()//���ظ� �Ծ����� ü�¹� ��鸮�� ȿ��
    {
        Debug.Log("������ ����");
        OnShake = true;
        hpBar.gameObject.transform.DOPunchPosition(new Vector3(5.5f, 0, 0), 1f, 10, 1);
        yield return new WaitForSeconds(1f);
        OnShake = false;
    }

}

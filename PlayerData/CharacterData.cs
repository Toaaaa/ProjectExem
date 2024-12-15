using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}

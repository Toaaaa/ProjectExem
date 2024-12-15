using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}

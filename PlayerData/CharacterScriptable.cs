using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Character Data")]
public class CharacterScriptable : ScriptableObject//조안나와 레이의 스테이터스 등을 저장할 스크립터블 오브젝트.
{
    [Header("Character Info")]
    public int currentHealth;
    public int maxHealth;
    public int attackPower;
    public int defense;
    public int speed;
    public int stamina;
    public int maxStamina;
    public int currenChain;//마법사가 사용(현재 스킬 체인 수)
    public bool isDead;
    public bool isStunned;
    public bool isSilenced;
    public bool isPoisoned;
    public bool isBurning;
    public bool isFrozen;
    public bool isShielded;
    public int shieldTime;//쉴드 남은시간
    /////////////////////
    public int upgradeCount;//훈련소에서 업그레이드한 횟수.
    public int originMaxHealth;
    public int originAttackPower;
    public int originDefense;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Character Data")]
public class CharacterScriptable : ScriptableObject//���ȳ��� ������ �������ͽ� ���� ������ ��ũ���ͺ� ������Ʈ.
{
    [Header("Character Info")]
    public int currentHealth;
    public int maxHealth;
    public int attackPower;
    public int defense;
    public int speed;
    public int stamina;
    public int maxStamina;
    public int currenChain;//�����簡 ���(���� ��ų ü�� ��)
    public bool isDead;
    public bool isStunned;
    public bool isSilenced;
    public bool isPoisoned;
    public bool isBurning;
    public bool isFrozen;
    public bool isShielded;
    public int shieldTime;//���� �����ð�
    /////////////////////
    public int upgradeCount;//�Ʒüҿ��� ���׷��̵��� Ƚ��.
    public int originMaxHealth;
    public int originAttackPower;
    public int originDefense;

}

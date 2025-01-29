using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MonsterSkillData : ScriptableObject
{
    public RuntimeAnimatorController animCon;//�ش� ��ų�� �ִϸ��̼� ��Ʈ�ѷ�.
    public float skillStartDelay;//�ش� ��ų�� ����ϴ� ������ ��ǰ� ��ųFX�� �������� ����Ǵ� (UseSkill�� ����Ǵ�) �ð� ������ ������. (�⺻�� 0��)

    public string skillName;
    public float damage;
    public float cost;//��ų ���� �Ҹ�Ǵ� �ڽ�Ʈ (�ð� ��� ��ȭ�� ��� �ؼ� dps�� �����Ͽ� ��꿡 �����ϰ� ����)
    public int skillTarget;//��ų�� Ÿ���� ���ϴ� ����. 0:����, 1:�Ĺ�, 2:��ü

    public virtual async void UseSkill(float atkPower,GameObject skillFx)//��ų�� ����ϴ� �Լ���, �ش� MonsterSkillData�� ��ӹ޴� ��ũ��Ʈ���� ����.
    {
        SkillFxPos(skillFx, skillTarget);
        await UniTask.Delay((int)(skillStartDelay*1000));//��ų�� FX�� ���� �������� ����Ǵ� �ð� ������ ������.
        SkillDamage(atkPower,skillTarget);
    }

    public void SkillFxPos(GameObject fx, int i)//��ų�� fx�� ǥ��.
    {
        CharacterData joan = GameManager.Instance.characterManager.Joanna;
        CharacterData rei = GameManager.Instance.characterManager.Rei;

        fx.gameObject.SetActive(false);
        fx.GetComponent<Animator>().runtimeAnimatorController = animCon;
        switch (i)
        {
            case 0://����
                if (!joan.isDead)//���ȳ��� ����������, Ÿ��: ���ȳ�
                {
                    fx.transform.position = GameManager.Instance.characterManager.Joanna.transform.position;
                }
                else if (!rei.isDead)//���̰� ����������, Ÿ��: ����
                {
                    fx.transform.position = GameManager.Instance.characterManager.Rei.transform.position;
                }
                else//��� ĳ���Ͱ� �׾������
                {

                }
                break;
            case 1://�Ĺ�
                if(!rei.isDead)//���̰� ����������, Ÿ��: ����
                {
                    fx.transform.position = GameManager.Instance.characterManager.Rei.transform.position;
                }
                else if (!joan.isDead)//���ȳ��� ����������, Ÿ��: ���ȳ�
                {
                    fx.transform.position = GameManager.Instance.characterManager.Joanna.transform.position;
                }
                else//��� ĳ���Ͱ� �׾������
                {

                }
                break;
            case 2://����
                fx.transform.position = new Vector3((joan.transform.position.x + rei.transform.position.x)/2, joan.transform.position.y, 0);//���ȳ��� ������ �߰�����.
                break;
            default:
                break;
        }//fx�� ��ġ�� ����.
        fx.gameObject.SetActive(true);
    }

    public void SkillDamage(float atkP,int i)//������ ���� �� ���.
    {
        CharacterData joan = GameManager.Instance.characterManager.Joanna;
        CharacterData rei = GameManager.Instance.characterManager.Rei;
        int joanInitHp = (int)joan.currentHealth;
        int reiInitHp = (int)rei.currentHealth;
        float dmg = atkP * damage;
        //������ attackPower * damage��ŭ�� �������� ����ϰ�, �̴� ĳ������ defense��ġ�� ���� ���� �������� �����ȴ�.
        switch (i)
        {
            case 0://����
                //������ ����
                if(!joan.isDead)//���ȳ��� ����������, Ÿ��: ���ȳ�
                {
                    if(joan.isMagicShield)//���ȳ��� �������尡 Ȱ��ȭ �Ǿ��������, ������ 0
                    {
                        joan.extraSkills[0].GetComponent<Animator>().SetTrigger("BlockedSkill");
                    }
                    else
                    {
                        joan.currentHealth -= dmg * (1 - (joan.defense / (25 + joan.defense)));//����� 25�� ���� ���.
                        joan.HpBarShake();
                    }
                }
                else if (!rei.isDead)//���̰� ����������, Ÿ��: ����
                {
                    if(rei.isMagicShield)//������ �������尡 Ȱ��ȭ �Ǿ��������, ������ 0
                    {
                        rei.extraSkills[0].GetComponent<Animator>().SetTrigger("BlockedSkill");
                    }
                    else
                    {
                        rei.currentHealth -= dmg * (1 - (rei.defense / (25 + rei.defense)));//����� 25�� ���� ���.
                        rei.HpBarShake();
                    }
                }
                else//��� ĳ���Ͱ� �׾������
                {

                }
                //������ ��� ex(joanHp - joan.currentHealth)�� ���� �־ ���.
                break;
            case 1://�Ĺ�
                //������ ����
                if (!rei.isDead)
                {
                    if(rei.isMagicShield)
                    {
                        rei.extraSkills[0].GetComponent<Animator>().SetTrigger("BlockedSkill");
                    }
                    else
                    {
                        rei.currentHealth -= dmg * (1 - (rei.defense / (25 + rei.defense)));//����� 25�� ���� ���.
                        rei.HpBarShake();
                    }
                }
                else if(!joan.isDead)
                {
                    if(joan.isMagicShield)
                    {
                        joan.extraSkills[0].GetComponent<Animator>().SetTrigger("BlockedSkill");
                    }
                    else
                    {
                        joan.currentHealth -= dmg * (1 - (joan.defense / (25 + joan.defense)));//����� 25�� ���� ���.
                        joan.HpBarShake();
                    }
                }
                else//��� ĳ���Ͱ� �׾������
                {

                }
                //������ ���
                break;
            case 2://����
                //������ ����
                if(rei.isMagicShield)
                {
                    rei.extraSkills[0].GetComponent<Animator>().SetTrigger("BlockedSkill");
                    joan.extraSkills[0].GetComponent<Animator>().SetTrigger("BlockedSkill");
                }
                else
                {
                    if(!joan.isDead)
                    {
                        joan.currentHealth -= dmg * (1 - (joan.defense / (25 + joan.defense)));
                        joan.HpBarShake();
                    }
                    if(!rei.isDead)
                    {
                        rei.currentHealth -= dmg * (1 - (rei.defense / (25 + rei.defense))); 
                        rei.HpBarShake();
                    }
                }
                //������ ���
                break;
            default:
                break;
        }
    }
}


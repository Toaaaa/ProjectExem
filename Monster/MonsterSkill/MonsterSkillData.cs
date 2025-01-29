using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MonsterSkillData : ScriptableObject
{
    public RuntimeAnimatorController animCon;//해당 스킬의 애니메이션 컨트롤러.
    public float skillStartDelay;//해당 스킬을 사용하는 몬스터의 모션과 스킬FX의 데미지가 적용되는 (UseSkill이 실행되는) 시간 사이의 딜레이. (기본은 0초)

    public string skillName;
    public float damage;
    public float cost;//스킬 사용시 소모되는 코스트 (시간 기반 재화로 사용 해서 dps와 연동하여 계산에 용이하게 설계)
    public int skillTarget;//스킬의 타겟을 정하는 변수. 0:전방, 1:후방, 2:전체

    public virtual async void UseSkill(float atkPower,GameObject skillFx)//스킬을 사용하는 함수로, 해당 MonsterSkillData를 상속받는 스크립트에서 구현.
    {
        SkillFxPos(skillFx, skillTarget);
        await UniTask.Delay((int)(skillStartDelay*1000));//스킬의 FX와 실제 데미지가 적용되는 시간 사이의 딜레이.
        SkillDamage(atkPower,skillTarget);
    }

    public void SkillFxPos(GameObject fx, int i)//스킬의 fx를 표시.
    {
        CharacterData joan = GameManager.Instance.characterManager.Joanna;
        CharacterData rei = GameManager.Instance.characterManager.Rei;

        fx.gameObject.SetActive(false);
        fx.GetComponent<Animator>().runtimeAnimatorController = animCon;
        switch (i)
        {
            case 0://전방
                if (!joan.isDead)//조안나가 살아있을경우, 타겟: 조안나
                {
                    fx.transform.position = GameManager.Instance.characterManager.Joanna.transform.position;
                }
                else if (!rei.isDead)//레이가 살아있을경우, 타겟: 레이
                {
                    fx.transform.position = GameManager.Instance.characterManager.Rei.transform.position;
                }
                else//모든 캐릭터가 죽었을경우
                {

                }
                break;
            case 1://후방
                if(!rei.isDead)//레이가 살아있을경우, 타겟: 레이
                {
                    fx.transform.position = GameManager.Instance.characterManager.Rei.transform.position;
                }
                else if (!joan.isDead)//조안나가 살아있을경우, 타겟: 조안나
                {
                    fx.transform.position = GameManager.Instance.characterManager.Joanna.transform.position;
                }
                else//모든 캐릭터가 죽었을경우
                {

                }
                break;
            case 2://범위
                fx.transform.position = new Vector3((joan.transform.position.x + rei.transform.position.x)/2, joan.transform.position.y, 0);//조안나와 레이의 중간지점.
                break;
            default:
                break;
        }//fx의 위치를 설정.
        fx.gameObject.SetActive(true);
    }

    public void SkillDamage(float atkP,int i)//데미지 적용 및 출력.
    {
        CharacterData joan = GameManager.Instance.characterManager.Joanna;
        CharacterData rei = GameManager.Instance.characterManager.Rei;
        int joanInitHp = (int)joan.currentHealth;
        int reiInitHp = (int)rei.currentHealth;
        float dmg = atkP * damage;
        //몬스터의 attackPower * damage만큼의 데미지를 출력하고, 이는 캐릭터의 defense수치에 따라 실제 데미지가 결정된다.
        switch (i)
        {
            case 0://전방
                //데미지 적용
                if(!joan.isDead)//조안나가 살아있을경우, 타겟: 조안나
                {
                    if(joan.isMagicShield)//조안나의 매직쉴드가 활성화 되어있을경우, 데미지 0
                    {
                        joan.extraSkills[0].GetComponent<Animator>().SetTrigger("BlockedSkill");
                    }
                    else
                    {
                        joan.currentHealth -= dmg * (1 - (joan.defense / (25 + joan.defense)));//방어상수 25로 방어력 계산.
                        joan.HpBarShake();
                    }
                }
                else if (!rei.isDead)//레이가 살아있을경우, 타겟: 레이
                {
                    if(rei.isMagicShield)//레이의 매직쉴드가 활성화 되어있을경우, 데미지 0
                    {
                        rei.extraSkills[0].GetComponent<Animator>().SetTrigger("BlockedSkill");
                    }
                    else
                    {
                        rei.currentHealth -= dmg * (1 - (rei.defense / (25 + rei.defense)));//방어상수 25로 방어력 계산.
                        rei.HpBarShake();
                    }
                }
                else//모든 캐릭터가 죽었을경우
                {

                }
                //데미지 출력 ex(joanHp - joan.currentHealth)을 위에 넣어서 출력.
                break;
            case 1://후방
                //데미지 적용
                if (!rei.isDead)
                {
                    if(rei.isMagicShield)
                    {
                        rei.extraSkills[0].GetComponent<Animator>().SetTrigger("BlockedSkill");
                    }
                    else
                    {
                        rei.currentHealth -= dmg * (1 - (rei.defense / (25 + rei.defense)));//방어상수 25로 방어력 계산.
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
                        joan.currentHealth -= dmg * (1 - (joan.defense / (25 + joan.defense)));//방어상수 25로 방어력 계산.
                        joan.HpBarShake();
                    }
                }
                else//모든 캐릭터가 죽었을경우
                {

                }
                //데미지 출력
                break;
            case 2://범위
                //데미지 적용
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
                //데미지 출력
                break;
            default:
                break;
        }
    }
}


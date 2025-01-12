using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class SkillShoting : StateMachineBehaviour //레이의 스킬 오브젝트의 애니메이터에서 사용.
{
    public bool isSummonSkill;//발사형식이 아닌 위치 소환 형식인 경우
    public bool isRangeSkill;//범위 스킬인 경우
    private float skillX;//스킬의 x값.

    //아래의 데이터는 투사체의 SkillHitMonster에게 전달후 Collision시 사용됨.
    [Header("Skill Data_직접 넣어주기")]
    public CardData skillData;//스킬의 데이터.
    public bool fastMultiAttack;//빠른 연속 공격인 경우.(천천히 툭툭툭이 아니라 빠르게 파바박! 들어가는 스킬)
    public int skillAttackCount;//스킬의 공격 횟수.
    public float SkillDamageTime;//피격후 스킬의 데미지가 들어가는 시간
    public float SkillDamage;//스킬의 데미지 계수. 피격후 몬스터의 방어력+플레이어 공격력 에 따라 실제 적용될 데미지에 차이가 있음.

    private void Awake()
    {
        fastMultiAttack = skillData.fastMultiAttack;
        skillAttackCount = skillData.skillAttackCount;
        SkillDamageTime = skillData.SkillDamageTime;
        SkillDamage = skillData.SkillDamage;
    }

    public override async void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<SkillHitMonster>().isUsing = true;//스킬 사용중으로 변경.
        skillX = 0;//x값 초기화.

        animator.GetComponent<SkillHitMonster>().moveTween = null;//기존의 tween 초기화.

        if(!isSummonSkill)//발사 형식인 경우, 제일 앞에 있는 몬스터 우선 타격.
        {
            //스킬을 x축 일직선으로 발사하는 형식.
            GameObject skill = animator.gameObject;
            //Scene에서의 몬스터의 위치는 보통 3~4정도.(8,2)로 설정해 투사체 충돌까지 약 1초가량.
            SkillHitMonster skillHitMonster = animator.GetComponent<SkillHitMonster>();
            skillHitMonster.moveTween = skill.transform.DOMoveX(8, 2);
            //스킬의 공격 횟수, 데미지 계수, 데미지 시간을 설정.

            skillHitMonster.fastMultiAttack = fastMultiAttack;
            skillHitMonster.skillAttackCount = skillAttackCount;
            skillHitMonster.SkillDamageTime = SkillDamageTime;
            skillHitMonster.SkillDamage = SkillDamage;
        }
        else//위치 소환 형식인 경우, 단일 판정 or 범위 판정.
        {
            if(isRangeSkill)//범위 스킬인 경우
            {
                //범위 스킬의 경우, 존재하는 모든 몬스터의 x값의 평균 위치에 스킬을 소환. 
                //해당 state 진입전 targetMonster를 설정 후 진입하도록 해야함.
                GameObject skill = animator.gameObject;
                //타겟들의 중간위치를 찾아 해당 위치에 스킬 소환.
                if(animator.GetComponent<SkillHitMonster>().characterData.targetMonster != null)
                {
                    for(int i = 0; i < animator.GetComponent<SkillHitMonster>().characterData.targetMonster.Length; i++)
                    {
                        skillX += animator.GetComponent<SkillHitMonster>().characterData.targetMonster[i].transform.position.x;
                    }
                    skillX /= animator.GetComponent<SkillHitMonster>().characterData.targetMonster.Length;
                    Vector3 skillPos = animator.transform.parent.InverseTransformPoint(new Vector3(skillX,0,0));
                    skillPos.y = 0;
                    animator.transform.localPosition = skillPos;
                    animator.ResetTrigger("HitMonster");//몬스터의 중간에 소환되면, 피격 판정이 없을 수 있어 수동으로 초기화.
                    //피격 판정이 없을 수도 있으니 이곳에서 데미지 연산 실행
                    //데미지 연산 스크립트.
                }
                else
                {
                    Debug.LogWarning("Target Monster is not assigned.");
                }
            }
            else//단일 판정인 경우(targetMonster중 index 0번째 몬스터에게만 피격 판정 >> 더 앞에있는 몬스터로 올수 있게 몬스터 매니저에서 관리해주기.)
            {
                GameObject skill = animator.gameObject;
                SkillHitMonster skillHitMonster = animator.GetComponent<SkillHitMonster>();
                //타겟중 가장 앞에있는 타겟의 위치값을 가져와서
                skillX = animator.GetComponent<SkillHitMonster>().characterData.targetMonster[0].transform.position.x;
                Vector3 skillPos = animator.transform.parent.InverseTransformPoint(new Vector3(skillX,0,0));
                skillPos.y = 0;
                //해당 위치에 스킬을 소환.
                animator.transform.localPosition = skillPos;
                animator.ResetTrigger("HitMonster");//몬스터의 중간에 소환되면, 피격 판정이 없을 수 있어 수동으로 초기화.
                //스킬의 공격 횟수, 데미지 계수, 데미지 시간을 설정.
                skillHitMonster.fastMultiAttack = fastMultiAttack;
                skillHitMonster.skillAttackCount = skillAttackCount;
                skillHitMonster.SkillDamageTime = SkillDamageTime;
                skillHitMonster.SkillDamage = SkillDamage;
            }
        }
    }
}

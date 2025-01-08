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

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        skillX = 0;//x값 초기화.

        animator.GetComponent<SkillHitMonster>().moveTween = null;//기존의 tween 초기화.

        if(!isSummonSkill)//발사 형식인 경우, 제일 앞에 있는 몬스터 우선 타격.
        {
            //스킬을 x축 일직선으로 발사하는 형식.
            GameObject skill = animator.gameObject;
            //Scene에서의 몬스터의 위치는 보통 3~4정도.(8,2)로 설정해 투사체 충돌까지 약 1초가량.
            animator.GetComponent<SkillHitMonster>().moveTween = skill.transform.DOMoveX(8, 2);           
        }
        else//위치 소환 형식인 경우, 단일 판정 or 범위 판정.
        {
            if(isRangeSkill)//범위 스킬인 경우
            {
                //범위 스킬의 경우, 존재하는 모든 몬스터의 x값의 평균 위치에 스킬을 소환. 
                //해당 state 진입전 targetMonster를 설정 후 진입하도록 해야함.
                GameObject skill = animator.gameObject;
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
                }
                else
                {
                    Debug.LogWarning("Target Monster is not assigned.");
                }
            }
            else//단일 판정인 경우
            {
                //단일 판정의 경우, 빠르게 투명한 스킬을 발사 + 피격시점부터 스킬 재생 하도록 애니메이션 조정.
                GameObject skill = animator.gameObject;
                animator.GetComponent<SkillHitMonster>().moveTween = skill.transform.DOMoveX(8, 0.1f);
            }
        }
    }
}

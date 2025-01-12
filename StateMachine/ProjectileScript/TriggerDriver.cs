using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDriver : StateMachineBehaviour //해당 state가 시작될때 스킬오브젝트의 애니메이션을 트리거 해줌.
{
    public string triggerName;
    public bool isReiSkill;//Rei's Personal Skill에 해당하는 스킬인 경우 사용.

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isReiSkill)
        {
            animator.GetComponent<Rei>().reiPersonal.SetTrigger(triggerName);
        }
        else
        {
            //사용중이지 않은 스킬 오브젝트를 찾아서 스킬을 사용. (오브젝트 풀링)
            for (int i = 0; i < animator.GetComponent<Rei>().skillObject.Length; i++)
            {
                if (!animator.GetComponent<Rei>().skillObject[i].GetComponent<SkillHitMonster>().isUsing)
                {
                    //해당 스킬 오브젝트가 사용중이 아닐때 사용.
                    animator.GetComponent<Rei>().UseSkill(i, triggerName);
                    break;
                }
            }
        }
    }
}

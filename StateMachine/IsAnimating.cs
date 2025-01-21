using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAnimating : StateMachineBehaviour
{
    //캐릭터가 애니메이션 도중에는 스킬 카드를 사용할 수 없도록 bool값으로 관리.
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAnimating", true);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAnimating", false);
    }
}

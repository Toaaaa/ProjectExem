using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class ReturnSkillPos : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<SkillHitMonster>().moveTween?.Kill();//Tween이 존재하면 취소.
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.localPosition = new Vector3(0.5f, 0, 0);
        animator.GetComponent<SkillHitMonster>().isUsing = false;//사용 종료.
    }
}

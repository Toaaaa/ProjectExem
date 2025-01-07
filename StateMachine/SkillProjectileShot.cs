using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectileShot : StateMachineBehaviour
{
    public Animator externalAnimator;//스킬 투사체의 애니메이터
    public string triggerName;//스킬 투사체의 트리거 이름

    public void SetExternalAnimator(Animator animator)
    {
        externalAnimator = animator;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (externalAnimator != null)
        {
            externalAnimator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogWarning("External Animator is not assigned.");
        }
    }
}

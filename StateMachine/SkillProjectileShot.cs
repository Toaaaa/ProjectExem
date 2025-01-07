using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectileShot : StateMachineBehaviour
{
    public Animator externalAnimator;//��ų ����ü�� �ִϸ�����
    public string triggerName;//��ų ����ü�� Ʈ���� �̸�

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

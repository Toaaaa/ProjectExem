using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAnimating : StateMachineBehaviour
{
    //ĳ���Ͱ� �ִϸ��̼� ���߿��� ��ų ī�带 ����� �� ������ bool������ ����.
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAnimating", true);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAnimating", false);
    }
}

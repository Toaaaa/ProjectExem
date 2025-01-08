using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterDriver : StateMachineBehaviour//State�� ������ �Ķ������ ���� �����ϴ� Ŭ��.
{
    public string parameterName;
    public bool value;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(parameterName, value);
    }
}

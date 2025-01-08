using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterDriver : StateMachineBehaviour//State가 끝날때 파라미터의 값을 변경하는 클라스.
{
    public string parameterName;
    public bool value;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(parameterName, value);
    }
}

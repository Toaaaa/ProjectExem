using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDriver : StateMachineBehaviour
{
    public string triggerName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //사용중이지 않은 스킬 오브젝트를 찾아서 스킬을 사용. (오브젝트 풀링)
        for (int i = 0; i < animator.GetComponent<Rei>().skillObject.Length; i++)
        {
            if (!animator.GetComponent<Rei>().skillObject[i].GetComponent<Animator>().GetBool("IsCasting"))
            {
                animator.GetComponent<Rei>().UseSkill(i, triggerName);
                break;
            }
        }
    }
}

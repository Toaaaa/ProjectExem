using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDriver : StateMachineBehaviour
{
    public string triggerName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //��������� ���� ��ų ������Ʈ�� ã�Ƽ� ��ų�� ���. (������Ʈ Ǯ��)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDriver : StateMachineBehaviour //�ش� state�� ���۵ɶ� ��ų������Ʈ�� �ִϸ��̼��� Ʈ���� ����.
{
    public string triggerName;
    public bool isReiSkill;//Rei's Personal Skill�� �ش��ϴ� ��ų�� ��� ���.

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isReiSkill)
        {
            animator.GetComponent<Rei>().reiPersonal.SetTrigger(triggerName);
        }
        else
        {
            //��������� ���� ��ų ������Ʈ�� ã�Ƽ� ��ų�� ���. (������Ʈ Ǯ��)
            for (int i = 0; i < animator.GetComponent<Rei>().skillObject.Length; i++)
            {
                if (!animator.GetComponent<Rei>().skillObject[i].GetComponent<SkillHitMonster>().isUsing)
                {
                    //�ش� ��ų ������Ʈ�� ������� �ƴҶ� ���.
                    animator.GetComponent<Rei>().UseSkill(i, triggerName);
                    break;
                }
            }
        }
    }
}

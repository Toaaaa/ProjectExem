using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class SkillShoting : StateMachineBehaviour //������ ��ų ������Ʈ�� �ִϸ����Ϳ��� ���.
{
    public bool isSummonSkill;//�߻������� �ƴ� ��ġ ��ȯ ������ ���
    public bool isRangeSkill;//���� ��ų�� ���
    private float skillX;//��ų�� x��.

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        skillX = 0;//x�� �ʱ�ȭ.

        animator.GetComponent<SkillHitMonster>().moveTween = null;//������ tween �ʱ�ȭ.

        if(!isSummonSkill)//�߻� ������ ���, ���� �տ� �ִ� ���� �켱 Ÿ��.
        {
            //��ų�� x�� ���������� �߻��ϴ� ����.
            GameObject skill = animator.gameObject;
            //Scene������ ������ ��ġ�� ���� 3~4����.(8,2)�� ������ ����ü �浹���� �� 1�ʰ���.
            animator.GetComponent<SkillHitMonster>().moveTween = skill.transform.DOMoveX(8, 2);           
        }
        else//��ġ ��ȯ ������ ���, ���� ���� or ���� ����.
        {
            if(isRangeSkill)//���� ��ų�� ���
            {
                //���� ��ų�� ���, �����ϴ� ��� ������ x���� ��� ��ġ�� ��ų�� ��ȯ. 
                //�ش� state ������ targetMonster�� ���� �� �����ϵ��� �ؾ���.
                GameObject skill = animator.gameObject;
                if(animator.GetComponent<SkillHitMonster>().characterData.targetMonster != null)
                {
                    for(int i = 0; i < animator.GetComponent<SkillHitMonster>().characterData.targetMonster.Length; i++)
                    {
                        skillX += animator.GetComponent<SkillHitMonster>().characterData.targetMonster[i].transform.position.x;
                    }
                    skillX /= animator.GetComponent<SkillHitMonster>().characterData.targetMonster.Length;
                    Vector3 skillPos = animator.transform.parent.InverseTransformPoint(new Vector3(skillX,0,0));
                    skillPos.y = 0;
                    animator.transform.localPosition = skillPos;
                    animator.ResetTrigger("HitMonster");//������ �߰��� ��ȯ�Ǹ�, �ǰ� ������ ���� �� �־� �������� �ʱ�ȭ.
                }
                else
                {
                    Debug.LogWarning("Target Monster is not assigned.");
                }
            }
            else//���� ������ ���
            {
                //���� ������ ���, ������ ������ ��ų�� �߻� + �ǰݽ������� ��ų ��� �ϵ��� �ִϸ��̼� ����.
                GameObject skill = animator.gameObject;
                animator.GetComponent<SkillHitMonster>().moveTween = skill.transform.DOMoveX(8, 0.1f);
            }
        }
    }
}

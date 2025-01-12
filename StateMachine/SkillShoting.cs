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

    //�Ʒ��� �����ʹ� ����ü�� SkillHitMonster���� ������ Collision�� ����.
    [Header("Skill Data_���� �־��ֱ�")]
    public CardData skillData;//��ų�� ������.
    public bool fastMultiAttack;//���� ���� ������ ���.(õõ�� �������� �ƴ϶� ������ �Ĺٹ�! ���� ��ų)
    public int skillAttackCount;//��ų�� ���� Ƚ��.
    public float SkillDamageTime;//�ǰ��� ��ų�� �������� ���� �ð�
    public float SkillDamage;//��ų�� ������ ���. �ǰ��� ������ ����+�÷��̾� ���ݷ� �� ���� ���� ����� �������� ���̰� ����.

    private void Awake()
    {
        fastMultiAttack = skillData.fastMultiAttack;
        skillAttackCount = skillData.skillAttackCount;
        SkillDamageTime = skillData.SkillDamageTime;
        SkillDamage = skillData.SkillDamage;
    }

    public override async void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<SkillHitMonster>().isUsing = true;//��ų ��������� ����.
        skillX = 0;//x�� �ʱ�ȭ.

        animator.GetComponent<SkillHitMonster>().moveTween = null;//������ tween �ʱ�ȭ.

        if(!isSummonSkill)//�߻� ������ ���, ���� �տ� �ִ� ���� �켱 Ÿ��.
        {
            //��ų�� x�� ���������� �߻��ϴ� ����.
            GameObject skill = animator.gameObject;
            //Scene������ ������ ��ġ�� ���� 3~4����.(8,2)�� ������ ����ü �浹���� �� 1�ʰ���.
            SkillHitMonster skillHitMonster = animator.GetComponent<SkillHitMonster>();
            skillHitMonster.moveTween = skill.transform.DOMoveX(8, 2);
            //��ų�� ���� Ƚ��, ������ ���, ������ �ð��� ����.

            skillHitMonster.fastMultiAttack = fastMultiAttack;
            skillHitMonster.skillAttackCount = skillAttackCount;
            skillHitMonster.SkillDamageTime = SkillDamageTime;
            skillHitMonster.SkillDamage = SkillDamage;
        }
        else//��ġ ��ȯ ������ ���, ���� ���� or ���� ����.
        {
            if(isRangeSkill)//���� ��ų�� ���
            {
                //���� ��ų�� ���, �����ϴ� ��� ������ x���� ��� ��ġ�� ��ų�� ��ȯ. 
                //�ش� state ������ targetMonster�� ���� �� �����ϵ��� �ؾ���.
                GameObject skill = animator.gameObject;
                //Ÿ�ٵ��� �߰���ġ�� ã�� �ش� ��ġ�� ��ų ��ȯ.
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
                    //�ǰ� ������ ���� ���� ������ �̰����� ������ ���� ����
                    //������ ���� ��ũ��Ʈ.
                }
                else
                {
                    Debug.LogWarning("Target Monster is not assigned.");
                }
            }
            else//���� ������ ���(targetMonster�� index 0��° ���Ϳ��Ը� �ǰ� ���� >> �� �տ��ִ� ���ͷ� �ü� �ְ� ���� �Ŵ������� �������ֱ�.)
            {
                GameObject skill = animator.gameObject;
                SkillHitMonster skillHitMonster = animator.GetComponent<SkillHitMonster>();
                //Ÿ���� ���� �տ��ִ� Ÿ���� ��ġ���� �����ͼ�
                skillX = animator.GetComponent<SkillHitMonster>().characterData.targetMonster[0].transform.position.x;
                Vector3 skillPos = animator.transform.parent.InverseTransformPoint(new Vector3(skillX,0,0));
                skillPos.y = 0;
                //�ش� ��ġ�� ��ų�� ��ȯ.
                animator.transform.localPosition = skillPos;
                animator.ResetTrigger("HitMonster");//������ �߰��� ��ȯ�Ǹ�, �ǰ� ������ ���� �� �־� �������� �ʱ�ȭ.
                //��ų�� ���� Ƚ��, ������ ���, ������ �ð��� ����.
                skillHitMonster.fastMultiAttack = fastMultiAttack;
                skillHitMonster.skillAttackCount = skillAttackCount;
                skillHitMonster.SkillDamageTime = SkillDamageTime;
                skillHitMonster.SkillDamage = SkillDamage;
            }
        }
    }
}

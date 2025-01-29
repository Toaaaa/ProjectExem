using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillUseOnAnim : StateMachineBehaviour//������ ��ų �ִϸ��̼� ���ۿ��� �������� ���� Ÿ�ֿ̹� ȣ��Ǵ� �Լ�.
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//�������� ���� Ÿ�ֿ̹� ȣ��Ǵ� �Լ�.
    {
        MonsterObject monsterObject = animator.GetComponent<MonsterObject>();
        monsterObject.skillOnWaiting?.Invoke();//��ų�� fx, ������, ������ ��� ����.
        monsterObject.skillOnWaiting = null;
        monsterObject.SkillQueNext();//��ų�� ť�� �Ѱ��� ������ ����.
    }
}

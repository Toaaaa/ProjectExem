using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;



public class MagicShieldRei : StateMachineBehaviour
{
    public int skillPhase;//0:��ų ����, 1: ��ų(��) ������, 2: �� �ǰ�, 3:��ų ����
    private bool isResetting;
    public Joanna joanna;
    public Rei rei;

    private void Awake()
    {
        joanna = GameObject.FindWithTag("Joanna").GetComponent<Joanna>();
    }
    //������ ��ü ���������, ���ȳ����Ե� ����Ǵ� �������带 ���� ������ stateMachine���� ����.
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rei = animator.GetComponent<Rei>();

        if(skillPhase == 0)//������ ��������
        {
            joanna.extraSkills[0].GetComponent<Animator>().SetTrigger("MagicShield");
        }
        if (skillPhase == 1)//�踮�� �����Ϸ�
        {
            animator.SetBool("IsBlocking", true);
            joanna.extraSkills[0].GetComponent<Animator>().SetBool("IsBlocking",true);
            joanna.isMagicShield = true;
            rei.isMagicShield = true;
            //5�� �� IsBlocking�� false�� �ٲ���.
            Count3Second(animator);//3���� ���н�ų ����(phase 3 ���� ����)
        }
        if(skillPhase == 2)//����� ��ų�� �ǰݵ�
        {
            //�ǰ� ������ ������ ����

        }
       if(skillPhase == 3)//������ ����
        {
            joanna.extraSkills[0].GetComponent<Animator>().SetBool("IsBlocking", false);
            joanna.isMagicShield = false;
            rei.isMagicShield = false;
            animator.ResetTrigger("BlockedSkill");
            joanna.extraSkills[0].GetComponent<Animator>().ResetTrigger("BlockedSkill");
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    private void Count3Second(Animator anim)//5���� ���н�ų ����
    {
        UniTask.WaitForSeconds(3f).ContinueWith(() =>
        {
            anim.GetComponent<Animator>().SetBool("IsBlocking", false);
        }).Forget();
    }
    private IEnumerator ResetTrigger(Animator anim,string triggerName)//Ʈ���Ÿ� ������ �� ���� ������ ��� 5���� ����.
    {
        isResetting = true;
        yield return new WaitForSeconds(3f);
        anim.GetComponent<Animator>().ResetTrigger(triggerName);
        isResetting = false;
    }
}

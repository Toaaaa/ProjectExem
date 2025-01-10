using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;



public class MagicShieldRei : StateMachineBehaviour
{
    public int skillPhase;//0:스킬 시작, 1: 스킬(방어막) 시전중, 2: 방어막 피격, 3:스킬 종료
    private bool isResetting;
    public Joanna joanna;
    public Rei rei;

    private void Awake()
    {
        joanna = GameObject.FindWithTag("Joanna").GetComponent<Joanna>();
    }
    //레이의 자체 매직쉴드와, 조안나에게도 적용되는 매직쉴드를 전부 레이의 stateMachine에서 관리.
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rei = animator.GetComponent<Rei>();

        if(skillPhase == 0)//베리어 생성시작
        {
            joanna.extraSkills[0].GetComponent<Animator>().SetTrigger("MagicShield");
        }
        if (skillPhase == 1)//배리어 생성완료
        {
            animator.SetBool("IsBlocking", true);
            joanna.extraSkills[0].GetComponent<Animator>().SetBool("IsBlocking",true);
            joanna.isMagicShield = true;
            rei.isMagicShield = true;
            //5초 뒤 IsBlocking을 false로 바꿔줌.
            Count3Second(animator);//3초후 방패스킬 해제(phase 3 으로 진행)
        }
        if(skillPhase == 2)//베리어에 스킬이 피격됨
        {
            //피격 판정을 개별로 진행

        }
       if(skillPhase == 3)//베리어 해제
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
    private void Count3Second(Animator anim)//5초후 방패스킬 해제
    {
        UniTask.WaitForSeconds(3f).ContinueWith(() =>
        {
            anim.GetComponent<Animator>().SetBool("IsBlocking", false);
        }).Forget();
    }
    private IEnumerator ResetTrigger(Animator anim,string triggerName)//트리거를 실행할 수 없는 조건일 경우 5초후 리셋.
    {
        isResetting = true;
        yield return new WaitForSeconds(3f);
        anim.GetComponent<Animator>().ResetTrigger(triggerName);
        isResetting = false;
    }
}

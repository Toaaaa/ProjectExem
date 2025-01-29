using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class Joanna : CharacterData
{
    private bool isResetting;//트리거의 리셋 상태 확인.

    void Update()
    {
        HpStaminaUpdate();

        if (Input.GetKeyDown(KeyCode.F5))
        {
            skill2Test();
        }
        if(Input.GetKeyDown(KeyCode.F6))
        {
            if(GetComponent<Animator>().GetBool("IsBlocking") == true)
                BlockedSkill();
        }
    }

    public void BlockedSkill()//방패 스킬을 시전중 피격 당했을시.
    {
        GetComponent<Animator>().SetTrigger("BlockedSkill");
        if(!isResetting)//리셋중(5초)이 아닐경우 리셋 타이머 시작.
            StartCoroutine(ResetTrigger("BlockedSkill"));
    }
    public void skill2Test()//방패 스킬 사용.
    {
        GetComponent<Animator>().SetTrigger("Skill2");
        Count5Second();
    }
    private void Count5Second()//5초후 방패스킬 해제
    {
        UniTask.WaitForSeconds(5f).ContinueWith(() =>
        {
            GetComponent<Animator>().SetBool("IsBlocking", false);
        }).Forget();
    }
    private IEnumerator ResetTrigger(string triggerName)//트리거를 실행할 수 없는 조건일 경우 5초후 리셋.
    {
        isResetting = true;
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().ResetTrigger(triggerName);
        isResetting = false;
    }
}

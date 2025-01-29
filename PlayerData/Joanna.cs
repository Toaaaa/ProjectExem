using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class Joanna : CharacterData
{
    private bool isResetting;//Ʈ������ ���� ���� Ȯ��.

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

    public void BlockedSkill()//���� ��ų�� ������ �ǰ� ��������.
    {
        GetComponent<Animator>().SetTrigger("BlockedSkill");
        if(!isResetting)//������(5��)�� �ƴҰ�� ���� Ÿ�̸� ����.
            StartCoroutine(ResetTrigger("BlockedSkill"));
    }
    public void skill2Test()//���� ��ų ���.
    {
        GetComponent<Animator>().SetTrigger("Skill2");
        Count5Second();
    }
    private void Count5Second()//5���� ���н�ų ����
    {
        UniTask.WaitForSeconds(5f).ContinueWith(() =>
        {
            GetComponent<Animator>().SetBool("IsBlocking", false);
        }).Forget();
    }
    private IEnumerator ResetTrigger(string triggerName)//Ʈ���Ÿ� ������ �� ���� ������ ��� 5���� ����.
    {
        isResetting = true;
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().ResetTrigger(triggerName);
        isResetting = false;
    }
}

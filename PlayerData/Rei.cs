using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rei : CharacterData
{
    //��ų ������Ʈ�� �����ϴ� ������Ʈ Ǯ��.
    public GameObject[] skillObject;//������� ���δ� �ش� ������Ʈ�� animator�� isCasting bool���� ���� Ȯ��.

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("Skill13");
        }
    }

    public void UseSkill(int i, string skillName)
    {
        skillObject[i].GetComponent<Animator>().SetBool("IsCasting", true);
        skillObject[i].GetComponent<Animator>().SetTrigger(skillName);
    }
}

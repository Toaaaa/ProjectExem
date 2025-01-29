using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rei : CharacterData
{
    //스킬 오브젝트를 저장하는 오브젝트 풀링.
    public GameObject[] skillObject;//사용중의 여부는 해당 오브젝트의 animator의 isCasting bool값을 통해 확인.
    public Animator reiPersonal;//레이의 개인 스킬 애니메이터.

    void Update()
    {
        HpStaminaUpdate();

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

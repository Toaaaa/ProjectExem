using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CharacterManager : MonoBehaviour
{
    public GameObject Joanna;
    public GameObject Rei;
    public Animator joanAnim;
    public Animator reiAnim;


    private void Start()
    {
        joanAnim = Joanna.GetComponent<Animator>();
        reiAnim = Rei.GetComponent<Animator>();
        GameManager.Instance.characterManager = this;
    }
    

    public void AllWalk()//다음 스테이지로 이동할때 모든 캐릭터가 걷는 애니메이션 실행.
    {
        joanAnim.SetTrigger("MoveStart");
        reiAnim.SetTrigger("MoveStart");
    }
    public void WalkEnd()
    {
        joanAnim.SetTrigger("MoveEnd");
        reiAnim.SetTrigger("MoveEnd");
    }
    public void StartRest()
    {
        joanAnim.SetTrigger("RestStart");
        reiAnim.SetTrigger("RestStart");
    }
    public void EndRest()
    {
        joanAnim.SetTrigger("RestEnd");
        reiAnim.SetTrigger("RestEnd");
    }
    public void AllFlee()
    {
        joanAnim.SetTrigger("FleeStart");
        reiAnim.SetTrigger("FleeStart");
    }
    public void FleeEnd()
    {
        joanAnim.SetTrigger("FleeEnd");
        reiAnim.SetTrigger("FleeEnd");
    }
}

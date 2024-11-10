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
    }


    public void AllWalk()//���� ���������� �̵��Ҷ� ��� ĳ���Ͱ� �ȴ� �ִϸ��̼� ����.
    {
        joanAnim.SetTrigger("MoveStart");
        reiAnim.SetTrigger("MoveStart");
    }
    public void WalkEnd()
    {
        joanAnim.SetTrigger("MoveEnd");
        reiAnim.SetTrigger("MoveEnd");
    }

}

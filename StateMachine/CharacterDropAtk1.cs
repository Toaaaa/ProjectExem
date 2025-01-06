using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDropAtk1 : StateMachineBehaviour 
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player = animator.gameObject;
        animator.SetBool("IsOnAir", true);
        player.transform.DOMoveY(1.5f, stateInfo.length*0.7f).SetEase(Ease.OutQuad);
    }
}

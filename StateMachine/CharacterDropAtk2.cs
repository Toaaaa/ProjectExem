using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDropAtk2 : StateMachineBehaviour
{
    public float YPos;
    public float OriginalYPos;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player = animator.gameObject;
        player.transform.DOMoveY(YPos, stateInfo.length*0.95f).SetEase(Ease.InQuad).onComplete += () =>
        {
            animator.SetBool("IsOnAir", false);
            player.transform.position = new Vector3(player.transform.position.x, OriginalYPos, player.transform.position.z);
        };
    }
}

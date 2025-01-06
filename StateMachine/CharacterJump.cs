using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : StateMachineBehaviour
{
    private float _exitTime;
    private float _timer;
    private bool isFalling = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//캐릭터가 점프
    {
        _exitTime = 0.55f; // 원하는 시간 설정 (초 단위)
        _timer = 0f;
        isFalling = false;
        Jump(animator);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//캐릭터가 점프
    {
        _timer += Time.deltaTime;
        if (!isFalling && _timer >= stateInfo.length - _exitTime)
        {
            FallDown(animator);
        }
        if (isFalling && animator.gameObject.transform.position.y <= -1f)
        {
            animator.SetBool("IsOnAir", false);
        }
        if(animator.gameObject.transform.position.y <= -1f)
        {
            Debug.Log("OnGround");
        }
    }

    private void Jump(Animator anim)
    {
        GameObject player = anim.gameObject;
        anim.SetBool("IsOnAir", true);
        player.transform.DOMoveY(1f, 0.25f).SetEase(Ease.OutQuad);
    }

    private void FallDown(Animator anim)
    {
        GameObject player = anim.gameObject;
        player.transform.DOMoveY(-1.3f, 0.5f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            isFalling = true;
            player.transform.position = new Vector3(player.transform.position.x, -1f, player.transform.position.z);
        });
    }
}


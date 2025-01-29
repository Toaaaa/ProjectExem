using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillUseOnAnim : StateMachineBehaviour//몬스터의 스킬 애니메이션 동작에서 데미지가 들어가는 타이밍에 호출되는 함수.
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//데미지가 들어가는 타이밍에 호출되는 함수.
    {
        MonsterObject monsterObject = animator.GetComponent<MonsterObject>();
        monsterObject.skillOnWaiting?.Invoke();//스킬의 fx, 데미지, 데미지 출력 실행.
        monsterObject.skillOnWaiting = null;
        monsterObject.SkillQueNext();//스킬의 큐를 한개씩 앞으로 당긴다.
    }
}

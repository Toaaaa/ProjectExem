using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillHitMonster : MonoBehaviour //해당 스크립트를 가지고 있는 오브젝트가 몬스터의 hitbox에 충돌하였을때 실행되는 함수보유.
{
    public CharacterData characterData;//해당 오브젝트를 사용하는 메인 캐릭터의 데이터.
    public Tween moveTween;//skillShoting에서 발사된 스킬의 이동을 제어하기 위한 변수.
    public bool isUsing;//해당 스킬이 사용중인지 여부.

    public bool fastMultiAttack;//빠른 연속공격 여부.0.1초 주기로 공격.
    public int skillAttackCount;//스킬의 공격 횟수. 2이상이면 0.5초 or 0.1초 마다
    public float SkillDamageTime;//피격후 스킬의 데미지가 들어가는 시간
    public float SkillDamage;//스킬의 데미지 계수. 피격후 몬스터의 방어력+플레이어 공격력 에 따라 실제 적용될 데미지에 차이가 있음.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            moveTween?.Kill();//Tween이 존재하면 취소.
            GetComponent<Animator>().SetTrigger("HitMonster");//몬스터 충돌 트리거 실행.
        }
    }
    //범위스킬의 중간에 소환하는 경우 SkillShoting에서 직접 데미지연산 실행.
}

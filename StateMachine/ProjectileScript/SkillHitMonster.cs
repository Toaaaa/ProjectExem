using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillHitMonster : MonoBehaviour //해당 스크립트를 가지고 있는 오브젝트가 몬스터의 hitbox에 충돌하였을때 실행되는 함수보유.
{
    public CharacterData characterData;//해당 오브젝트를 사용하는 메인 캐릭터의 데이터.
    public Tween moveTween;//skillShoting에서 발사된 스킬의 이동을 제어하기 위한 변수.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            Debug.Log("Hit Monster");
            GetComponent<Animator>().SetTrigger("HitMonster");//몬스터 충돌 트리거 실행.
        }
    }
}

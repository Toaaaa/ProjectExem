using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillHitMonster : MonoBehaviour //�ش� ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� ������ hitbox�� �浹�Ͽ����� ����Ǵ� �Լ�����.
{
    public CharacterData characterData;//�ش� ������Ʈ�� ����ϴ� ���� ĳ������ ������.
    public Tween moveTween;//skillShoting���� �߻�� ��ų�� �̵��� �����ϱ� ���� ����.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            Debug.Log("Hit Monster");
            GetComponent<Animator>().SetTrigger("HitMonster");//���� �浹 Ʈ���� ����.
        }
    }
}

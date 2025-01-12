using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillHitMonster : MonoBehaviour //�ش� ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� ������ hitbox�� �浹�Ͽ����� ����Ǵ� �Լ�����.
{
    public CharacterData characterData;//�ش� ������Ʈ�� ����ϴ� ���� ĳ������ ������.
    public Tween moveTween;//skillShoting���� �߻�� ��ų�� �̵��� �����ϱ� ���� ����.
    public bool isUsing;//�ش� ��ų�� ��������� ����.

    public bool fastMultiAttack;//���� ���Ӱ��� ����.0.1�� �ֱ�� ����.
    public int skillAttackCount;//��ų�� ���� Ƚ��. 2�̻��̸� 0.5�� or 0.1�� ����
    public float SkillDamageTime;//�ǰ��� ��ų�� �������� ���� �ð�
    public float SkillDamage;//��ų�� ������ ���. �ǰ��� ������ ����+�÷��̾� ���ݷ� �� ���� ���� ����� �������� ���̰� ����.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            moveTween?.Kill();//Tween�� �����ϸ� ���.
            GetComponent<Animator>().SetTrigger("HitMonster");//���� �浹 Ʈ���� ����.
        }
    }
    //������ų�� �߰��� ��ȯ�ϴ� ��� SkillShoting���� ���� ���������� ����.
}

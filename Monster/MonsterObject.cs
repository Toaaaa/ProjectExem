using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObject : MonoBehaviour
{
    //ü�¹�
    //�ִϸ��̼�
    //���� ����� ǥ��
    public MonsterData monsterData;
    public string monsterName;
    public int MonsterID;
    public int health;
    public float attackPower;
    public float defense;
    public bool isElite; //����Ʈ ��������

    public bool combatWaiting = true;//���� ���� ����������. (������ ���츸 �ϰ� ������ ������ ���۵Ǳ� �� ����)



    private void OnEnable()
    {
        //���Ͱ� �����ɶ��� �ʿ��� ��� �ʱ�ȭ. (�ִϸ��̼�, ü�¹�, ����� ��ҵ�..)
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.enabled = true;//���� hp0�� ���� ������ false ������ boxcollider = true;
    }
    private void OnDisable()
    {
        //���Ͱ� �׾����� or �������� ó��.
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.enabled = false;
        isElite = false;
        combatWaiting = true;//���� ��� ���·� ����.
    }

    private void Update()
    {
        if (health <= 0)
        {
            MonsterDie();
        }
        if(!combatWaiting) //���� ��� ���°� �ƴҶ� ��� �ൿ�� ����.
        {
            
        }
    }

    
    public void SetMonsterData(MonsterData data)//�ش� ������ prefab�� �����ɶ� ȣ��Ǿ� ������ �����͸� �����Ѵ�.
    {
        int x = GameManager.Instance.stageManager.RoomNum/10; 

        monsterData = data;
        monsterName = data.monsterName;
        MonsterID = data.MonsterID;
        //health = data.health;
        //attackPower = data.attackPower;
        defense = (5*x + 5 + data.extraDefense); //5x+5+�߰� ���� == �� ����. (x�� 10�� ������ ����)
    }
    void MonsterDie()
    {
        Debug.Log("Monster Die");
        gameObject.SetActive(false);
    }
}

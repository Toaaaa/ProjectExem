using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
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
    public int maxHealth;
    public float attackPower;
    public float defense;
    public float energy;//��ų ���� �Ҹ�Ǵ� cost. 
    public float generateEnergeyPer;//1��ŭ �������� �����ϴµ� �ʿ��� �ð�. ���� ���ۿ��� 1 >> ���� �������� �پ����� ����.
    public bool isElite; //����Ʈ ��������
    public bool combatWaiting = true;//���� ���� ����������. (������ ���츸 �ϰ� ������ ������ ���۵Ǳ� �� ����)
    public bool isAlive;

    public MonsterSkillData[] skillQue;//������ ����� ��ų�� �̸� �����صδ� ť.
    private CancellationTokenSource skillCts;

    private void OnEnable()
    {
        //���Ͱ� �����ɶ��� �ʿ��� ��� �ʱ�ȭ. (�ִϸ��̼�, ü�¹�, ����� ��ҵ�..)
        skillQue = new MonsterSkillData[3];//��ų�� ť�� 3�������� ����.
        SkillQueSet();//��ų ť�� 3���� ��ų�� ������ Ȯ���� ���� �־��ش�.
        generateEnergeyPer = 1;//������ ���� �ӵ� �ʱ�ȭ.
        isAlive = true;//�츮��
        health = maxHealth;//ü�� �ʱ�ȭ
        energy = 0;
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.enabled = true;//���� hp0�� ���� ������ false ������ boxcollider = true;
    }
    private void OnDisable()
    {
        //���Ͱ� �׾����� or �������� ó��.
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.enabled = false;
        isAlive = false;
        isElite = false;
        combatWaiting = true;//���� ��� ���·� ����.
    }

    private void Update()
    {
        if (health <= 0)
        {
            MonsterDie();
        }
        if(!combatWaiting && isAlive) //���� ��� ���°� �ƴҶ� ��� �ൿ�� ����.
        {
            energy += Time.deltaTime * generateEnergeyPer;
            if(energy >= skillQue[0].cost)
            {
                SkillUse();
            }
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
    void SkillQueSet()//��ų ť�� 3���� ��ų�� ������ Ȯ���� ���� �־��ش�.
    {
        if (monsterData == null)
        {
            Debug.LogError("MonsterData is null");
            return;
        }
        float random = UnityEngine.Random.Range(0, 100);
        float sum = 0;

        for(int i =0; i < 3; i++)
        {
            foreach (var skills in monsterData.monsterSkillList)
            {
                sum += skills.skillChance;
                if (random < sum)
                {
                    skillQue[i] = skills.monsterSkillData;
                    Debug.Log(gameObject.name + " : " + skillQue[i].skillName);
                    break;
                }
            }
        }
    }
    private void SkillUse()//��ų ť�� �ִ� ���� ���� "��ų�� ���" + ���� ��ų�� ����ָ� ä���ش�.
    {
        //��ų ��� ��ū�� �����߰� ������� �ʾҴٸ�, return.
        if(skillCts != null && !skillCts.Token.IsCancellationRequested)
            return;

        //return�� �ȵ̴ٸ�, ���ο� ��ū ����.
        skillCts = new CancellationTokenSource();

        //��ų ������ ���.
        energy -= skillQue[0].cost;

        //�񵿱�� ��ų ���.
        Skill(skillCts.Token).Forget();
    }

    async UniTask Skill(CancellationToken token)
    {
        try
        {
            // ��ų ��� �� ���� ������
            float delay = UnityEngine.Random.Range(0.2f, 1.2f);
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);

            if (token.IsCancellationRequested) return; // ��ū�� ��ҵǾ����� ���� �ߴ�

            // ��ų �ߵ� (���� ���,�ִϸ��̼�, SFX ��)
            Debug.Log($"{gameObject.name} uses {skillQue[0].skillName}");
            skillQue[0].UseSkill();

            // ��ų ť�� �� ĭ�� ���� ���ο� ��ų �߰�
            MonsterSkillData[] nextQue = { skillQue[1], skillQue[2], null };
            float random = UnityEngine.Random.Range(0, 100);
            float sum = 0;

            foreach (var skills in monsterData.monsterSkillList)
            {
                sum += skills.skillChance;
                if (random < sum)
                {
                    nextQue[2] = skills.monsterSkillData;
                    skillQue = nextQue;
                    break;
                }
            }
        }
        catch (OperationCanceledException)//��ų ��� ��ū�� ��ҵǾ�����.
        {
            Debug.Log("Skill execution was canceled.");
        }
        finally
        {
            // �۾� �Ϸ� �� ��ū ����
            skillCts?.Dispose();
            skillCts = null;
        }
    }
}

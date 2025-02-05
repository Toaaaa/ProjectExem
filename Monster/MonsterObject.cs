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
    public Action skillOnWaiting;
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

    public GameObject skillFXObject;//��ų ���� Ÿ���� ��ġ�� �����Ǵ� FX ������Ʈ.

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
        GetComponent<Animator>().SetBool("Death", false);//death bool �ʱ�ȭ.
        //���Ͱ� �׾����� or �������� ó��.
        GetComponent<Animator>().runtimeAnimatorController = null;//�ִϸ��̼� ��Ʈ�ѷ��� null�� �ʱ�ȭ.
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
        if (!combatWaiting && isAlive) //���� ��� ���°� �ƴҶ� ��� �ൿ�� ����.
        {
            energy += Time.deltaTime * generateEnergeyPer;
            if (energy >= skillQue[0].cost)
            {
                SkillUse();
            }
        }
    }


    public void SetMonsterData(MonsterData data)//�ش� ������ prefab�� �����ɶ� ȣ��Ǿ� ������ �����͸� �����Ѵ�.
    {
        int x = GameManager.Instance.stageManager.RoomNum / 10;
        if (data == null)
        {
            Debug.LogError("MonsterData is null");
            return;
        }
        GetComponent<Animator>().runtimeAnimatorController = data.animCon;
        monsterData = data;
        monsterName = data.monsterName;
        MonsterID = data.MonsterID;
        //health = data.health;
        //attackPower = data.attackPower;
        defense = (5 * x + 5 + data.extraDefense); //5x+5+�߰� ���� == �� ����. (x�� 10�� ������ ����)
    }
    void MonsterDie()
    {
        //���� ����� ��ų ��ū ���.
        skillCts?.Cancel();
        skillCts?.Dispose();
        skillCts = null;
        //���� ��� �ִϸ��̼�.
        GetComponent<Animator>().SetBool("Death", true);
        //������ ���� ������Ʈ ��Ȱ��ȭ.
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

        for (int i = 0; i < 3; i++)
        {
            foreach (var skills in monsterData.monsterSkillList)
            {
                sum += skills.skillChance;
                if (random < sum)
                {
                    skillQue[i] = skills.monsterSkillData;
                    break;
                }
            }
        }
    }
    private void SkillUse()//��ų ť�� �ִ� ���� ���� "��ų�� ���" + ���� ��ų�� ����ָ� ä���ش�.
    {
        //��ų ��� ��ū�� �����߰� ������� �ʾҴٸ�, return.
        if (skillCts != null && !skillCts.Token.IsCancellationRequested)
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
            float ran = UnityEngine.Random.Range(0f, 1f);
            if (ran < 0.5f)
            {
                GetComponent<Animator>().SetTrigger("Attack");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Skill");
            }

            float delay = GetRandomValue();//0.2~0.4 or 0.9~1.2 ������ ������ ����.
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);

            if (token.IsCancellationRequested) return; // ��ū�� ��ҵǾ����� ���� �ߴ�

            // ��ų �ߵ� (�ִϸ��̼�, SFX ��)
            for (int i = 0; i < monsterData.monsterSkillList.Count; i++)
            {
                var skill = monsterData.monsterSkillList[i];
                if (skill.monsterSkillData == skillQue[0])
                {
                    GetComponent<Animator>().SetTrigger($"Skill{i}");//������ ��ų�� �ش��ϴ� �ִϸ��̼� Ʈ���� ����.
                    break;
                }
            }//�ִϸ��̼� ȿ��
            // ��ų�� ���� ��� ����()
            skillOnWaiting = () => //skillOnWaiting�� ����Ϸ��� skillOnWaiting.Invoke()�� ����� = null �� �ʱ�ȭ. (skillOnWaiting �� null üŷ �� ���)
            {
                //���� ��ų ����� Cancel�� �Ǿ��ٸ� skillOnWaiting = null�� �ʱ�ȭ.
                skillQue[0].UseSkill(attackPower,skillFXObject);
            };
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
    public float GetRandomValue()
    {
        // 0�� 1 ������ ���� ���� ����
        float randomValue = UnityEngine.Random.Range(0f, 1f);

        // ������ ���� 0.5���� ������ ù ��° ����(0.2~0.4)���� �� ����
        if (randomValue < 0.5f)
        {
            return UnityEngine.Random.Range(0.2f, 0.4f);
        }
        // ������ ���� 0.5���� ũ�� �� ��° ����(0.9~1.2)���� �� ����
        else
        {
            return UnityEngine.Random.Range(0.9f, 1.2f);
        }
    }
    public void SkillQueNext()
    {
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

    //��ų���� ��ȣ�ۿ�//
    public void SkillCanceled()//���ϵ��� ������ ��ų ���.
    {
        //���� �ִϸ��̼� ����.
        skillOnWaiting = null;
        skillCts?.Cancel();
        skillCts?.Dispose();
        skillCts = null;
    }
}

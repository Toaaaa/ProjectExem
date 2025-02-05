using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterObject : MonoBehaviour
{

    //체력바
    //애니메이션
    //버프 디버프 표시
    public Action skillOnWaiting;
    public MonsterData monsterData;

    public string monsterName;
    public int MonsterID;
    public int health;
    public int maxHealth;
    public float attackPower;
    public float defense;
    public float energy;//스킬 사용시 소모되는 cost. 
    public float generateEnergeyPer;//1만큼 에너지를 생산하는데 필요한 시간. 전투 시작에는 1 >> 몬스터 마릿수가 줄어들수록 증가.
    public bool isElite; //엘리트 몬스터인지
    public bool combatWaiting = true;//전투 시작 대기상태인지. (몬스터의 조우만 하고 실제로 전투가 시작되기 전 상태)
    public bool isAlive;

    public GameObject skillFXObject;//스킬 사용시 타겟의 위치에 생성되는 FX 오브젝트.

    public MonsterSkillData[] skillQue;//다음에 사용할 스킬을 미리 저장해두는 큐.
    private CancellationTokenSource skillCts;

    private void OnEnable()
    {
        //몬스터가 생성될때의 필요한 요소 초기화. (애니메이션, 체력바, 디버프 요소등..)
        skillQue = new MonsterSkillData[3];//스킬의 큐는 3개까지만 저장.
        SkillQueSet();//스킬 큐에 3개의 스킬을 정해진 확률에 따라 넣어준다.
        generateEnergeyPer = 1;//에너지 생성 속도 초기화.
        isAlive = true;//살리고
        health = maxHealth;//체력 초기화
        energy = 0;
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.enabled = true;//몬스터 hp0시 죽음 판정후 false 상태의 boxcollider = true;
    }
    private void OnDisable()
    {
        GetComponent<Animator>().SetBool("Death", false);//death bool 초기화.
        //몬스터가 죽었을때 or 도망시의 처리.
        GetComponent<Animator>().runtimeAnimatorController = null;//애니메이션 컨트롤러를 null로 초기화.
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.enabled = false;
        isAlive = false;
        isElite = false;
        combatWaiting = true;//전투 대기 상태로 변경.
    }

    private void Update()
    {
        if (health <= 0)
        {
            MonsterDie();
        }
        if (!combatWaiting && isAlive) //전투 대기 상태가 아닐때 모든 행동을 수행.
        {
            energy += Time.deltaTime * generateEnergeyPer;
            if (energy >= skillQue[0].cost)
            {
                SkillUse();
            }
        }
    }


    public void SetMonsterData(MonsterData data)//해당 몬스터의 prefab이 생성될때 호출되어 몬스터의 데이터를 설정한다.
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
        defense = (5 * x + 5 + data.extraDefense); //5x+5+추가 방어력 == 총 방어력. (x는 10층 단위로 증가)
    }
    void MonsterDie()
    {
        //몬스터 사망시 스킬 토큰 취소.
        skillCts?.Cancel();
        skillCts?.Dispose();
        skillCts = null;
        //몬스터 사망 애니메이션.
        GetComponent<Animator>().SetBool("Death", true);
        //끝나면 몬스터 오브젝트 비활성화.
        gameObject.SetActive(false);
    }
    void SkillQueSet()//스킬 큐에 3개의 스킬을 정해진 확률에 따라 넣어준다.
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
    private void SkillUse()//스킬 큐에 있는 가장 앞의 "스킬을 사용" + 뒤의 스킬을 당겨주며 채워준다.
    {
        //스킬 사용 토큰을 생성했고 취소하지 않았다면, return.
        if (skillCts != null && !skillCts.Token.IsCancellationRequested)
            return;

        //return이 안됫다면, 새로운 토큰 생성.
        skillCts = new CancellationTokenSource();

        //스킬 에너지 사용.
        energy -= skillQue[0].cost;

        //비동기로 스킬 사용.
        Skill(skillCts.Token).Forget();
    }

    async UniTask Skill(CancellationToken token)
    {
        try
        {
            // 스킬 사용 전 랜덤 딜레이
            float ran = UnityEngine.Random.Range(0f, 1f);
            if (ran < 0.5f)
            {
                GetComponent<Animator>().SetTrigger("Attack");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Skill");
            }

            float delay = GetRandomValue();//0.2~0.4 or 0.9~1.2 사이의 랜덤값 리턴.
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);

            if (token.IsCancellationRequested) return; // 토큰이 취소되었으면 실행 중단

            // 스킬 발동 (애니메이션, SFX 등)
            for (int i = 0; i < monsterData.monsterSkillList.Count; i++)
            {
                var skill = monsterData.monsterSkillList[i];
                if (skill.monsterSkillData == skillQue[0])
                {
                    GetComponent<Animator>().SetTrigger($"Skill{i}");//실행할 스킬에 해당하는 애니메이션 트리거 실행.
                    break;
                }
            }//애니메이션 효과
            // 스킬의 실제 기능 저장()
            skillOnWaiting = () => //skillOnWaiting을 사용하려면 skillOnWaiting.Invoke()로 사용후 = null 로 초기화. (skillOnWaiting 의 null 체킹 후 사용)
            {
                //만약 스킬 사용전 Cancel이 되었다면 skillOnWaiting = null로 초기화.
                skillQue[0].UseSkill(attackPower,skillFXObject);
            };
        }
        catch (OperationCanceledException)//스킬 사용 토큰이 취소되었을때.
        {
            Debug.Log("Skill execution was canceled.");
        }
        finally
        {
            // 작업 완료 후 토큰 정리
            skillCts?.Dispose();
            skillCts = null;
        }
    }
    public float GetRandomValue()
    {
        // 0과 1 사이의 랜덤 값을 생성
        float randomValue = UnityEngine.Random.Range(0f, 1f);

        // 생성된 값이 0.5보다 작으면 첫 번째 범위(0.2~0.4)에서 값 생성
        if (randomValue < 0.5f)
        {
            return UnityEngine.Random.Range(0.2f, 0.4f);
        }
        // 생성된 값이 0.5보다 크면 두 번째 범위(0.9~1.2)에서 값 생성
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

    //스킬과의 상호작용//
    public void SkillCanceled()//스턴등의 이유로 스킬 취소.
    {
        //스턴 애니메이션 실행.
        skillOnWaiting = null;
        skillCts?.Cancel();
        skillCts?.Dispose();
        skillCts = null;
    }
}

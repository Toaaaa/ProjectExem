using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DampCave : Stage
{
    public bool isMonster;//���Ͱ� �ִ��� ������.
    public bool monsterSpanwed;//���Ͱ� �����ߴ��� ���� >> ��������
    public bool isSearch;//Ž���� �ߴ��� ����.
    public bool isClear;//�������� Ŭ���� ����.
    public float monsterEncounterRate = 60f;//���� ���� Ȯ��.(ó�� �����, Ž����, ���ֽ� �ش� ��ġ�� ������� ���͸� �����Ŵ) 85%�� ����.


    public int stateCondition;//�ش� �������� ������ ����. 0: ó�� ����(���� ����x), 1: ó�� ����(���� ����o), 2:Ž����, 3:������ 4:���� �й�.
    //public Item item;//�ش� ������������ Ž���� ������ ������.

    protected override void OnEnable()
    {
        base.OnEnable();

        isClear = false;
        isSearch = false;
        monsterSpanwed = false;
        isMonster = MonsterPossibility();
        MonsterEncounterRate();//������ ���� Ȯ�� + ����
    }
    private void Update()
    {
        if(GameManager.Instance.stageManager.duringCombat)//�������� ��
        {
            stateCondition = 4;//���� ����
        }
        else
        {
            if (isClear)//������ �־���, �¸����� ��
            {
                stateCondition = 3;//���� ��
                                   //2(��),3
            }
            else//������ �ϱ� ��.
            {
                if (isMonster)//���Ͱ� �ִ� ��
                {
                    if (monsterSpanwed)//���Ͱ� �������� ��
                    {
                        stateCondition = 1;//ó�� ����(���� ����o)
                                           //1(��),3
                    }
                    else//���Ͱ� �������� �ʾ��� ��
                    {
                        if (isSearch)//Ž���� ���� ��
                        {
                            stateCondition = 2;//Ž�� ��
                        }
                        else//Ž���� ���� �ʾ��� ��
                        {
                            stateCondition = 0;//ó�� ����(���� ����x)
                        }
                    }
                }
                else//���Ͱ� ���� ��
                {
                    stateCondition = 0;//ó�� ����(���� ����x)
                }
            }
        }

        switch (stateCondition)//��ư�� Ȱ��ȭ ���� ����.
        {
            case 0://ó�� ����(���� ����x)
                buttons[0].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(false);
                buttons[4].gameObject.SetActive(true);
                //Ž�� + �������� �̵�
                break;
            case 1://ó�� �����(���� ����o)
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(true);
                buttons[4].gameObject.SetActive(false);
                //���� + ����
                break;
            case 2://Ž�� ��
                if (isClear)
                {
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[2].gameObject.SetActive(true);
                    buttons[3].gameObject.SetActive(false);
                    buttons[4].gameObject.SetActive(true);
                    //���Ͱ�� + �������� �̵�
                }
                else
                {
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(true);
                    buttons[2].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                    buttons[4].gameObject.SetActive(true);
                    //Ž��(ȸ��) + �������� �̵�
                }
                break;
            case 3://���� ��               
                if (isSearch)
                {
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[2].gameObject.SetActive(true);
                    buttons[3].gameObject.SetActive(false);
                    buttons[4].gameObject.SetActive(true);
                    //���Ͱ�� + �������� �̵�
                }
                else
                {
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(true);
                    buttons[2].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                    buttons[4].gameObject.SetActive(true);
                    //Ž�� + �������� �̵�
                }
                break;
            case 4://���� ����
                buttons[0].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(false);
                buttons[4].gameObject.SetActive(false);
                break;
            case 5://���� �й�
                break;
            default:
                break;
        }//��ư�� Ȱ��ȭ ���� ����.
        if (Input.GetKeyDown(KeyCode.Space))//�ӽ÷� ���� �¸� ���� �޼�.
        {
            CombatWin();
        }
    }

    bool MonsterPossibility()//���Ϳ� ����
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        float a = 35 + GameManager.Instance.stageManager.RoomNum * 0.5f - 15;//���������� �ö󰥼��� ���� ���� Ȯ���� �ö�.(�⺻ 35�ۼ�Ʈ - ���ൿ�� 15�ۼ�Ʈ ����)
        if (Random.Range(0, 100) <= a ? true : false)
        {
            return true;
        }
        return false;
    }

    void MonsterEncounterRate()
    {
        if (isClear)
        {
            return;
        }//�̹� ��� óġ�� �ߴٸ� ����.

        if (isMonster)//���Ͱ� �ִ� ��
        {
            if (Random.Range(0, 100) <= monsterEncounterRate ? true : false)//monsterEncounterRate�� Ȯ���� ���� ����.
            {
                SpawnMonster();
            }
            else
            {
                //���Ͱ� ������ ����.
            }
        }
        else
        {
            //���Ͱ� ���� ��.
        }
    }
    void SpawnMonster()
    {
        monsterSpanwed = true;
        Debug.Log("���� ����");
    }
    public void StartCombat()//���� ����.
    {
        GameManager.Instance.stageManager.duringCombat = true;
        Debug.Log("���� ����");
        //���� ����.
    }
    public void CombatWin()//���� �¸�.
    {
        GameManager.Instance.stageManager.duringCombat = false;
        isClear = true;
    }
    public void EatMonsterMeat()
    {
        Debug.Log("���� ��⸦ �Դ´�.");
    }
    public async void Search()
    {
        isSearch = true;
        await UniTask.Delay(500);
        //Ž������ �ִϸ��̼� ���.

        //ã�� ��� >> 1.���� ����, 2 ������ �߰� ui
        if (isMonster)//���Ͱ� �ִ� ��
        {
            if (Random.Range(0, 100) <= monsterEncounterRate ? true : false)//���� ���� 
            {
                buttons[1].interactable = true;//Ž�� ���� ����� Ž�� ��ư ��Ȱ��ȭ.
                SpawnMonster();
            }
            else
            {
                Debug.Log("������ �߰�");
            }
        }
        else
        {
            Debug.Log("������ �߰�");
        }
    }
    public void MonsterRoomNextStage()
    {
        if (isMonster)//���Ͱ� �ִ� ��
        {
            if (isClear)//�̹� ���͸� óġ��
            {
                GameManager.Instance.stageManager.NextStage();
            }
            else
            {
                if (Random.Range(0, 100) <= monsterEncounterRate ? true : false)//monsterEncounterRate�� Ȯ���� ���� ����.
                {
                    buttons[4].interactable = true;
                    SpawnMonster();
                }
                else//���Ͱ� ���� ���� ����.
                {
                    GameManager.Instance.stageManager.NextStage();
                }
            }
        }
        else//���Ͱ� ���� ��
        {
            GameManager.Instance.stageManager.NextStage();
        }
    }
}

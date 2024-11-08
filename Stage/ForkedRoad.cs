using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ForkedRoad : Stage
{
    public bool isLeftClue;
    public bool isRightClue;

    void Start()
    {
        ResetAll();
        Clue();
    }


    void ResetAll()
    {
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Go Left";
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Go Right";
        isLeftClue = false;
        isRightClue = false;
    }
    void Clue()// ��� ��η� ���� Ȯ���� �� �������� �˷��ش�.
    {
        int a = Random.Range(0, 100);
        if (a <= 4)//5�ۼ�Ʈ�� Ȯ���� ��Ʈ ����.
        {

            if(Random.Range(0,2) == 0)
            {
                isLeftClue = true;
                buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Go Left<br>There is footstep on this way.";
            }
            else
            {
                isRightClue = true;
                buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Go Right<br>There is footstep on this way.";
            }
            
        }

    }

    public void GoLeft()//�������� ���� ��ư
    {
        if (isLeftClue)
        {
            GameManager.Instance.stageManager.EndPercent += 0.5f;//������ ���� Ȯ�� 0.5% ���.
            GameManager.Instance.stageManager.NextStage();//���� ���������� �̵�.
        }
        else
        {
            GameManager.Instance.stageManager.NextStage();//���� ���������� �̵�.
        }
    }
    public void GoRight()//���������� ���� ��ư
    {
        if (isRightClue)
        {
            GameManager.Instance.stageManager.EndPercent += 0.5f;//������ ���� Ȯ�� 0.5% ���.
            GameManager.Instance.stageManager.NextStage();//���� ���������� �̵�.
        }
        else
        {
            GameManager.Instance.stageManager.NextStage();//���� ���������� �̵�.
        }
    }

}

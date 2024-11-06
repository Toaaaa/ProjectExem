using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ForkedRoad : Stage
{
    public TextMeshProUGUI leftClueText;
    public TextMeshProUGUI rightClueText;
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
        leftClueText.text = "";
        rightClueText.text = "";
        isLeftClue = false;
        isRightClue = false;
    }
    void Clue()// ��� ��η� ���� Ȯ���� �� �������� �˷��ش�.
    {
        if(Random.Range(0,100) <= 4)//5�ۼ�Ʈ�� Ȯ���� ��Ʈ ����.
        {
            if(Random.Range(0,2) == 0)
            {
                isLeftClue = true;
                leftClueText.text = "There is footstep on this way.";
            }
            else
            {
                isRightClue = true;
                rightClueText.text = "There is footstep on this way.";
            }
            
        }

    }

    public void GoLeft()//�������� ���� ��ư
    {
        if (isLeftClue)
        {
            GameManager.Instance.stageManager.EndPercent += 0.5f;//������ ���� Ȯ�� 0.5% ���.
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
        }
        else
        {
            GameManager.Instance.stageManager.NextStage();//���� ���������� �̵�.
        }
    }

}

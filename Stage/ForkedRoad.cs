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
    void Clue()// 어느 경로로 가면 확률이 더 오르는지 알려준다.
    {
        if(Random.Range(0,100) <= 4)//5퍼센트의 확률로 힌트 등장.
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

    public void GoLeft()//왼쪽으로 가는 버튼
    {
        if (isLeftClue)
        {
            GameManager.Instance.stageManager.EndPercent += 0.5f;//목적지 조우 확률 0.5% 상승.
        }
        else
        {
            GameManager.Instance.stageManager.NextStage();//다음 스테이지로 이동.
        }
    }
    public void GoRight()//오른쪽으로 가는 버튼
    {
        if (isRightClue)
        {
            GameManager.Instance.stageManager.EndPercent += 0.5f;//목적지 조우 확률 0.5% 상승.
        }
        else
        {
            GameManager.Instance.stageManager.NextStage();//다음 스테이지로 이동.
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public List<Button> buttons;

    private void Start()
    {
        if (GameManager.Instance.stageManager.isMoving)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = true;
            }
        }
    }
    protected virtual void OnEnable()
    {
        if(GameManager.Instance.stageManager == null)
        {
            GameManager.Instance.stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
            GameManager.Instance.characterManager = GameManager.Instance.stageManager.characterManager;
        }


        if (GameManager.Instance.stageManager.isMoving)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = true;
            }
        }
    }

    void CantButton()//��� ��ư�� ��Ȱ��ȭ.
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = false;
        }
    }
}

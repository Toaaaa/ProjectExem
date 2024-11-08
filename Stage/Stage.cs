using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public List<Button> buttons;

    private void Start()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
    }
    protected virtual void OnEnable()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BloodCave : Stage //stage ��ũ��Ʈ ���
{
    public bool isMonster;//���Ͱ� �ִ��� ������.
    public bool isClear;//�������� Ŭ���� ����.
    //public Item item;//�ش� ������������ Ž���� ������ ������.

    private void OnEnable()
    {
        isClear = false;
        isMonster = Random.Range(0, 100) >= 5 ? true : false;//���Ͱ� 95%Ȯ���� ����.

    }
}

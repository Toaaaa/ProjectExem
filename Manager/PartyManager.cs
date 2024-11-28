using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    //��Ƽ�� ���� �ڿ� �� ���� ����//
    [SerializeField] private int bagpackSize = 20;//�ʱ� ���� ũ�� = 20
    [SerializeField] private int gold = 0;//������
    
    //â��(���� ������) ������ ���� �߰�.

    public int BagpackSize { get { return bagpackSize; } }
    public int Gold { get { return gold; } }


    void Start()
    {
        GameManager.Instance.partyManager = this;
    }

    public void BagUpgrade()
    {
        bagpackSize += 4;
    }//���� ũ�� +4
    public void AddGold(int amount)
    {
        gold += amount;
    }//������(int amount) �߰�
}

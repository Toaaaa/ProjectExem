using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop", menuName = "Shop/New Shop")]
public class ShopScriptableObject : ScriptableObject
{
    public List<Item> items = new List<Item>();//itemdata�� �������� ������, quantity�� ���� ������, �ش� ������ �ִ� ���Ű��� ���� �μ��� ��Ȱ�� ��.
    public List<Item> specialItems = new List<Item>();//itemdata�� �������� ������, quantity�� ���� ������, �ش� ������ �ִ� ���Ű��� ���� �μ��� ��Ȱ�� ��.

    public Item RandomItem()
    {
        //�ӽ÷� ������ ���� ������ �Լ�.
        Random.InitState(System.DateTime.Now.Millisecond);
        int i = Random.Range(0, 100);
        if(i < 10)
        {
            return specialItems[Random.Range(0, specialItems.Count)];//����� �������� ������. (10������ Ȯ��)
        }
        else
        {
            return items[0];//���߿� ������ ��� randomitem�� ����� �� items[0]�� �����͸� �޾��� ���, ���ο� ������ �߰��ϴ� �۾��� ��ŵ�ϵ��� ��.
        }
    }
}

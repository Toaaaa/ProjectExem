using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyThis : MonoBehaviour
{
    private void Awake()
    {
        // ������ Canvas�� ���� �� �������� �ʵ��� ����
        if (FindObjectsOfType<DontDestroyThis>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // Canvas�� �� ��ȯ �Ŀ��� ����
        DontDestroyOnLoad(gameObject);
    }
}

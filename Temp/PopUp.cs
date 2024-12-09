using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    //���� �˾�����(������ 2�̻���)�� ui���� ������Ʈ�� �߰����ִ� ������� ���.
    private void OnEnable()
    {
        GameManager.Instance.isPopupOn = true;
    }
    private void OnDisable()
    {
        GameManager.Instance.isPopupOn = false;
    }
    private void Update()
    {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
    }
}

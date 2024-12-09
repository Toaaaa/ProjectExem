using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    //������ 1�� ���� ���� ui�鿡�� ������Ʈ�� �߰����ִ� ������� ���.
    private void OnEnable()
    {
        GameManager.Instance.isMainUIOn = true;
    }
    private void OnDisable()
    {
        GameManager.Instance.isMainUIOn = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!GameManager.Instance.isPopupOn)
        {
            gameObject.SetActive(false);
        }
    }
}

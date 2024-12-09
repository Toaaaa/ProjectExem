using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    //뎁스가 1인 각종 메인 ui들에게 컴포넌트로 추가해주는 방식으로 사용.
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

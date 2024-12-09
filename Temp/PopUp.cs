using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    //작은 팝업형식(뎁스가 2이상의)의 ui에게 컴포넌트로 추가해주는 방식으로 사용.
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyThis : MonoBehaviour
{
    private void Awake()
    {
        // 동일한 Canvas가 여러 개 존재하지 않도록 방지
        if (FindObjectsOfType<DontDestroyThis>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // Canvas를 씬 전환 후에도 유지
        DontDestroyOnLoad(gameObject);
    }
}

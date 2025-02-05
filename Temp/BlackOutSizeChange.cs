using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOutSizeChange : MonoBehaviour
{
    //현재 해상도를 가져와서 그것에 맞는 크기로 조정하기에는 작업량이 많기에 FHD, QHD 2가지 해상도만 지원하도록 한다.
    [SerializeField] Canvas canvas;
    int heightData;
    RectTransform rectTransform;

    private void Awake()
    {
        heightData = Screen.height;
        rectTransform = GetComponent<RectTransform>();
        if(heightData == 1080)//FHD
        {
            Vector2 size = rectTransform.sizeDelta;
            size.x = 1920;
            size.y = 620;
            rectTransform.sizeDelta = size;
        }
        else//QHD
        {
            Vector2 size = rectTransform.sizeDelta;
            size.x = 2560;
            size.y = 825;
            rectTransform.sizeDelta = size;
        }
    }
    private void Update()
    {
        if(heightData == 1080)//FHD
        {
            if(Screen.height == 1440)//QHD 로 변경되었을 경우.
            {
                heightData = 1440;
                Vector2 size = rectTransform.sizeDelta;
                size.x = 2560;
                size.y = 825;
                rectTransform.sizeDelta = size;
            }

        }
        else//QHD
        {
            if(Screen.height == 1080)//FHD 로 변경되었을 경우.
            {
                heightData = 1080;
                Vector2 size = rectTransform.sizeDelta;
                size.x = 1920;
                rectTransform.sizeDelta = size;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBack : MonoBehaviour
{
    [SerializeField]
    private RectTransform MainSlot;
    [SerializeField]
    private bool isItemSlot; //뒷배경 슬롯이 아닌 아이템 이미지가 들어가는 슬롯의 경우 (크기를 약간 줄일것)
    [SerializeField]
    private int space; //아이템 슬롯과의 간격

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        //mainSlot의 rectTransform의 width와 height를 가져와서 해당 슬롯의 rectTransform의 width와 height에 대입한다.
        RectTransform mainSlotRect = MainSlot;

        if(isItemSlot)
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(mainSlotRect.rect.width - space, mainSlotRect.rect.height - space);
        else
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(mainSlotRect.rect.width, mainSlotRect.rect.height);

    }
}

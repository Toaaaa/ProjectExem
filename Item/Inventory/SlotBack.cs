using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBack : MonoBehaviour
{
    [SerializeField]
    private RectTransform MainSlot;
    [SerializeField]
    private bool isItemSlot; //�޹�� ������ �ƴ� ������ �̹����� ���� ������ ��� (ũ�⸦ �ణ ���ϰ�)
    [SerializeField]
    private int space; //������ ���԰��� ����

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        //mainSlot�� rectTransform�� width�� height�� �����ͼ� �ش� ������ rectTransform�� width�� height�� �����Ѵ�.
        RectTransform mainSlotRect = MainSlot;

        if(isItemSlot)
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(mainSlotRect.rect.width - space, mainSlotRect.rect.width - space); //������ ���簢�������� width�� �����ͼ� ���� (��ũ�� �䶧���� height�� ������ ����)
        else
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(mainSlotRect.rect.width, mainSlotRect.rect.width);

    }
}

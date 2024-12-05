using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEffect : MonoBehaviour
{
    private Button button;
    private Vector3 originalScale; // ��ư�� ���� ũ�� ����

    public int currentSize;
    public int maxSize;

    void Start()
    {
        button = GetComponent<Button>();
        originalScale = transform.localScale;

        // ��ư Ŭ�� �̺�Ʈ�� �޼��� ����
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // 1. ��ư �ִϸ��̼� ȿ��
        PlayClickEffect(() =>
        {
            
        });
    }

    void PlayClickEffect(TweenCallback onComplete)
    {
        transform.DOKill(); // ���� �ִϸ��̼� ����
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(originalScale * 0.9f, 0.1f)) // ���
                .Append(transform.DOScale(originalScale, 0.1f)) // ����
                .SetEase(Ease.OutQuad) // �ε巴��
                .OnComplete(onComplete); // �ִϸ��̼� �Ϸ� �� ȣ��
    }
}

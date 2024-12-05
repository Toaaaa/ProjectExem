using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEffect : MonoBehaviour
{
    private Button button;
    private Vector3 originalScale; // 버튼의 원래 크기 저장

    public int currentSize;
    public int maxSize;

    void Start()
    {
        button = GetComponent<Button>();
        originalScale = transform.localScale;

        // 버튼 클릭 이벤트에 메서드 연결
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // 1. 버튼 애니메이션 효과
        PlayClickEffect(() =>
        {
            
        });
    }

    void PlayClickEffect(TweenCallback onComplete)
    {
        transform.DOKill(); // 기존 애니메이션 정리
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(originalScale * 0.9f, 0.1f)) // 축소
                .Append(transform.DOScale(originalScale, 0.1f)) // 복원
                .SetEase(Ease.OutQuad) // 부드럽게
                .OnComplete(onComplete); // 애니메이션 완료 시 호출
    }
}

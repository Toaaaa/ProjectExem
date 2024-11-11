using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FirePlaceFlick : MonoBehaviour
{
    private Light2D light2D;
    public float minOuterRadius = 4f;
    public float maxOuterRadius = 5.5f;
    public float flickerSpeedMin = 0.1f; // 최소 깜빡임 속도
    public float flickerSpeedMax = 0.3f; // 최대 깜빡임 속도
    public float fadeDuration = 0.5f; // 모닥불이 켜지는 속도
    public float fadeOutDuration = 1f; // 도닥불이 꺼지는 속도

    private Tween flickerTween;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        if (light2D == null)
        {
            Debug.LogError("Light2D component is missing on this GameObject.");
            return;
        }
    }

    private void OnEnable()
    {
        light2D = GetComponent<Light2D>();
        if (light2D == null)
        {
            Debug.LogError("Light2D component is missing on this GameObject.");
            return;
        }

        light2D.pointLightOuterRadius = 0f;// 0으로 초기화
        FadeInAndStartFlicker();
    }

    private void FadeInAndStartFlicker()
    {
        // OuterRadius를 0에서 minOuterRadius로 서서히 증가
        light2D.pointLightOuterRadius = 0f;
        DOTween.To(() => light2D.pointLightOuterRadius, x => light2D.pointLightOuterRadius = x, minOuterRadius, fadeDuration)
            .SetEase(Ease.InOutSine)
            .OnComplete(StartFlicker);
    }

    private void StartFlicker()
    {
        float targetRadius = Random.Range(minOuterRadius, maxOuterRadius);
        float duration = Random.Range(flickerSpeedMin, flickerSpeedMax);

        // 기존 flickerTween이 있다면 중단
        flickerTween?.Kill();

        // 불규칙한 깜빡임 애니메이션 시작
        flickerTween = DOTween.To(() => light2D.pointLightOuterRadius, x => light2D.pointLightOuterRadius = x, targetRadius, duration)
            .SetEase(Ease.InOutSine)
            .OnComplete(StartFlicker); // 애니메이션이 끝나면 다시 StartFlicker 호출
    }

    public void FadeOutAndStopFlicker()
    {
        // 기존 flickerTween이 있다면 중단
        flickerTween?.Kill();

        // OuterRadius를 0으로 감소시켜 빛을 서서히 끄기
        DOTween.To(() => light2D.pointLightOuterRadius, x => light2D.pointLightOuterRadius = x, 0f, fadeOutDuration)
            .SetEase(Ease.InOutSine);
    }
}

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
    public float flickerSpeedMin = 0.1f; // �ּ� ������ �ӵ�
    public float flickerSpeedMax = 0.3f; // �ִ� ������ �ӵ�
    public float fadeDuration = 0.5f; // ��ں��� ������ �ӵ�
    public float fadeOutDuration = 1f; // ���ں��� ������ �ӵ�

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

        light2D.pointLightOuterRadius = 0f;// 0���� �ʱ�ȭ
        FadeInAndStartFlicker();
    }

    private void FadeInAndStartFlicker()
    {
        // OuterRadius�� 0���� minOuterRadius�� ������ ����
        light2D.pointLightOuterRadius = 0f;
        DOTween.To(() => light2D.pointLightOuterRadius, x => light2D.pointLightOuterRadius = x, minOuterRadius, fadeDuration)
            .SetEase(Ease.InOutSine)
            .OnComplete(StartFlicker);
    }

    private void StartFlicker()
    {
        float targetRadius = Random.Range(minOuterRadius, maxOuterRadius);
        float duration = Random.Range(flickerSpeedMin, flickerSpeedMax);

        // ���� flickerTween�� �ִٸ� �ߴ�
        flickerTween?.Kill();

        // �ұ�Ģ�� ������ �ִϸ��̼� ����
        flickerTween = DOTween.To(() => light2D.pointLightOuterRadius, x => light2D.pointLightOuterRadius = x, targetRadius, duration)
            .SetEase(Ease.InOutSine)
            .OnComplete(StartFlicker); // �ִϸ��̼��� ������ �ٽ� StartFlicker ȣ��
    }

    public void FadeOutAndStopFlicker()
    {
        // ���� flickerTween�� �ִٸ� �ߴ�
        flickerTween?.Kill();

        // OuterRadius�� 0���� ���ҽ��� ���� ������ ����
        DOTween.To(() => light2D.pointLightOuterRadius, x => light2D.pointLightOuterRadius = x, 0f, fadeOutDuration)
            .SetEase(Ease.InOutSine);
    }
}

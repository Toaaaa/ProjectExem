using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LightFlick : MonoBehaviour
{
    private Light2D light2D;
    public float minFalloff = 0.4f;
    public float maxFalloff = 0.6f;
    public float duration = 2f;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        if (light2D == null)
        {
            Debug.LogError("Light2D component is missing on this GameObject.");
            return;
        }

        // DOTween을 사용해 falloffIntensity를 반복적으로 조절
        DOTween.To(() => light2D.falloffIntensity, x => light2D.falloffIntensity = x, maxFalloff, duration)
            .SetEase(Ease.InOutSine)  // 부드러운 전환
            .SetLoops(-1, LoopType.Yoyo);  // Yoyo 반복: 0.4 -> 0.6 -> 0.4 무한 반복
    }
}

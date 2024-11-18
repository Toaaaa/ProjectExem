using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackViewManager : MonoBehaviour
{
    public Canvas canvas;

    [SerializeField]
    SpriteRenderer backView;
    [SerializeField]
    Image fadeInBlack;
    public float fadeDuration = 1f;  // 페이드 시간 설정 (초)

    public List<Sprite> backViewList;//배경 리스트. 0:tutorial, 1:StartingPoint, 2:QuietHideout, 3:BloodCave, 4:DampCave, 5:ForkedRoad, 6:CaveAlley, 7:EndPoint.

    private void Start()
    {
        fadeInBlack.color = new Color(0, 0, 0, 0);
    }


    public void TransitionToNextStage(System.Action onFadeOutComplete = null)
    {
        StartCoroutine(FadeOutIn(onFadeOutComplete));
    }
    public void TransitionToNextFlee(System.Action onFadeOutComplete = null)
    {
        StartCoroutine(FadeOutFlee(onFadeOutComplete));
    }

    private IEnumerator FadeOutIn(System.Action onFadeOutComplete)
    {
        // 페이드 아웃 (화면을 검게 만듦)
        GameManager.Instance.characterManager.AllWalk();
        yield return StartCoroutine(Fade(1.2f));

        // 이전 스테이지 비활성화 및 새로운 스테이지 활성화 로직
        backView.sprite = backViewList[GameManager.Instance.stageManager.nextStageType];
        onFadeOutComplete?.Invoke();

        // 잠시 대기 후 페이드 인
        yield return new WaitForSeconds(1.2f);  // 검은색 상태 유지 시간 (1초)

        // 페이드 인 (화면을 다시 투명하게 만듦)
        GameManager.Instance.characterManager.WalkEnd();
        yield return StartCoroutine(Fade(0f));
    }
    private IEnumerator FadeOutFlee(System.Action onFadeOutComplete)//위와 동일한 함수지만 도망시의 애니메이션 연출.
    {
        // 페이드 아웃 (화면을 검게 만듦)
        GameManager.Instance.characterManager.AllFlee();
        yield return StartCoroutine(Fade(1.2f));

        // 이전 스테이지 비활성화 및 새로운 스테이지 활성화 로직
        backView.sprite = backViewList[GameManager.Instance.stageManager.nextStageType];
        onFadeOutComplete?.Invoke();

        // 잠시 대기 후 페이드 인
        yield return new WaitForSeconds(1.2f);  // 검은색 상태 유지 시간 (1초)

        // 페이드 인 (화면을 다시 투명하게 만듦)
        GameManager.Instance.characterManager.FleeEnd();
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeInBlack.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeInBlack.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }

        fadeInBlack.color = new Color(0, 0, 0, targetAlpha);  // 최종 Alpha 설정
    }


}

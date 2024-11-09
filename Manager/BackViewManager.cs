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
    public float fadeDuration = 1f;  // ���̵� �ð� ���� (��)

    public List<Sprite> backViewList;//��� ����Ʈ. 0:tutorial, 1:StartingPoint, 2:QuietHideout, 3:BloodCave, 4:DampCave, 5:ForkedRoad, 6:CaveAlley, 7:EndPoint.

    private void Start()
    {
        fadeInBlack.color = new Color(0, 0, 0, 0);
    }


    public void TransitionToNextStage(System.Action onFadeOutComplete = null)
    {
        StartCoroutine(FadeOutIn(onFadeOutComplete));
    }

    private IEnumerator FadeOutIn(System.Action onFadeOutComplete)
    {
        // ���̵� �ƿ� (ȭ���� �˰� ����)
        GameManager.Instance.characterManager.AllWalk();
        yield return StartCoroutine(Fade(1f));

        // ���� �������� ��Ȱ��ȭ �� ���ο� �������� Ȱ��ȭ ����
        backView.sprite = backViewList[GameManager.Instance.stageManager.nextStageType];
        onFadeOutComplete?.Invoke();

        // ��� ��� �� ���̵� ��
        yield return new WaitForSeconds(1f);  // ������ ���� ���� �ð� (1��)

        // ���̵� �� (ȭ���� �ٽ� �����ϰ� ����)
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

        fadeInBlack.color = new Color(0, 0, 0, targetAlpha);  // ���� Alpha ����
    }


}

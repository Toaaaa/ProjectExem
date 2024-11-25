using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Hideout : Stage
{
    public bool isResting;
    bool isRestingButton;//휴식 버튼을 눌렀는지 여부.
    bool OnResting;//휴식 애니메이션을 시작하여 아직 휴식을 끝내지 못하는 상태.

    [SerializeField]
    GameObject FireLight;//모닥불 라이팅.
    [SerializeField]
    GameObject Fire;//모닥불 오브젝트.

    private void Start()
    {
        SpriteRenderer spriteRenderer = Fire.GetComponent<SpriteRenderer>();

        // 초기 알파 값을 0으로 설정
        Color color = spriteRenderer.color;
        color.a = 0;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        isResting = false;
        isRestingButton = false;
        OnResting = false;
    }

    private void Update()
    {
        if(!GameManager.Instance.stageManager.isMoving)
        {
            if (isRestingButton)
            {
                buttons[0].interactable = false;//휴식 버튼 비활성화.
                buttons[2].interactable = false;//다음방 버튼 비활성화.
                buttons[0].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(true);
                if (OnResting)
                {
                    buttons[1].interactable = false;
                }
                else
                {
                    buttons[1].interactable = true;
                }
            }
            else
            {
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
            }
        }
    }

    public async void StartResting()
    {
        GameManager.Instance.characterManager.StartRest();
        isRestingButton = true;
        OnResting = true;
        LightsOff();
        await UniTask.Delay(800);
        StartFire();
    }
    public void SetIsResting()//휴식 애니메이션(OnStateExit)이 시작되고 난뒤 1초후 부터 휴식중으로 설정.
    {
        isResting = true;
        OnResting = false;
    }
    public async void EndResting()
    {
        GameManager.Instance.characterManager.EndRest();
        isResting = false;
        OnResting = true;
        LightsOn();
        EndFire();
        await UniTask.Delay(1200);
        buttons[0].interactable = true;//휴식 버튼 활성화.
        buttons[2].interactable = true;//다음방 버튼 활성화.

    }
    public void SetIsEndResting()
    {
        OnResting = false;
        isRestingButton = false;
    }
    void LightsOff()//글로벌 라이트 낮추기
    {
        Light2D light2D = GameManager.Instance.stageManager.globalLight.GetComponent<Light2D>();
        DOTween.To(() => light2D.intensity, x => light2D.intensity = x, 0.1f, 1f);//글로벌 라이트를 0.1로 낮춤. + 1초동안.
    }
    void LightsOn()//글로벌 라이트 복구
    {
        Light2D light2D = GameManager.Instance.stageManager.globalLight.GetComponent<Light2D>();
        DOTween.To(() => light2D.intensity, x => light2D.intensity = x, 1f, 1f);//글로벌 라이트를 1로 복구. + 1초동안.
    }
    async void StartFire()//모닥불 시작
    {
        FireFadeIn();
        await UniTask.Delay(1000);//모닥불 세팅후 불이 켜지는 딜레이.
        FireLight.SetActive(false);
        FireLight.SetActive(true);
    }
    void EndFire()//모닥불 끄기
    {
        FireFadeOut();
        FireLight.GetComponent<FirePlaceFlick>().FadeOutAndStopFlicker();
    }
    void FireFadeIn()
    {
        SpriteRenderer spriteRenderer = Fire.GetComponent<SpriteRenderer>();

        // 초기 알파 값을 0으로 설정
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        // 알파 값을 1로 1초 동안 서서히 변경
        spriteRenderer.DOFade(1f, 1f);
    }
    void FireFadeOut()
    {
        SpriteRenderer spriteRenderer = Fire.GetComponent<SpriteRenderer>();

        // 초기 알파 값을 1로 설정
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;

        // 알파 값을 0으로 1초 동안 서서히 변경
        spriteRenderer.DOFade(0f, 1f);
    }
}

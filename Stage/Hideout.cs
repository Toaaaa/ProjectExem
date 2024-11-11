using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Hideout : Stage
{
    public bool isResting;
    bool isRestingButton;//�޽� ��ư�� �������� ����.
    bool OnResting;//�޽� �ִϸ��̼��� �����Ͽ� ���� �޽��� ������ ���ϴ� ����.

    [SerializeField]
    GameObject FireLight;//��ں� ������.
    [SerializeField]
    GameObject Fire;//��ں� ������Ʈ.

    private void Start()
    {
        Color color = Fire.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        Fire.GetComponent<SpriteRenderer>().color = color;
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
        if (isRestingButton)
        {
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(true);
            buttons[2].interactable = false;
            if(OnResting)
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
            buttons[2].interactable = true;
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
    public void SetIsResting()//�޽� �ִϸ��̼�(OnStateExit)�� ���۵ǰ� ���� 1���� ���� �޽������� ����.
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
        await UniTask.Delay(800);
        EndFire();
    }
    public void SetIsEndResting()
    {
        OnResting = false;
        isRestingButton = false;
    }
    void LightsOff()//�۷ι� ����Ʈ ���߱�
    {
        Light2D light2D = GameManager.Instance.stageManager.globalLight.GetComponent<Light2D>();
        DOTween.To(() => light2D.intensity, x => light2D.intensity = x, 0.1f, 1f);//�۷ι� ����Ʈ�� 0.1�� ����. + 1�ʵ���.
    }
    void LightsOn()//�۷ι� ����Ʈ ����
    {
        Light2D light2D = GameManager.Instance.stageManager.globalLight.GetComponent<Light2D>();
        DOTween.To(() => light2D.intensity, x => light2D.intensity = x, 1f, 1f);//�۷ι� ����Ʈ�� 1�� ����. + 1�ʵ���.
    }
    async void StartFire()//��ں� ����
    {
        FireFadeIn();
        await UniTask.Delay(1000);//��ں� ������ ���� ������ ������.
        FireLight.SetActive(false);
        FireLight.SetActive(true);
    }
    void EndFire()//��ں� ����
    {
        FireFadeOut();
        FireLight.GetComponent<FirePlaceFlick>().FadeOutAndStopFlicker();
    }
    void FireFadeIn()
    {
        Color color = Fire.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        Fire.GetComponent<SpriteRenderer>().color = color;

        GetComponent<SpriteRenderer>().DOFade(1, 1);
    }
    void FireFadeOut()
    {
        GetComponent<SpriteRenderer>().DOFade(0, 1);
    }
}

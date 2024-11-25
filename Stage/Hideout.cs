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
        SpriteRenderer spriteRenderer = Fire.GetComponent<SpriteRenderer>();

        // �ʱ� ���� ���� 0���� ����
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
                buttons[0].interactable = false;//�޽� ��ư ��Ȱ��ȭ.
                buttons[2].interactable = false;//������ ��ư ��Ȱ��ȭ.
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
        EndFire();
        await UniTask.Delay(1200);
        buttons[0].interactable = true;//�޽� ��ư Ȱ��ȭ.
        buttons[2].interactable = true;//������ ��ư Ȱ��ȭ.

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
        SpriteRenderer spriteRenderer = Fire.GetComponent<SpriteRenderer>();

        // �ʱ� ���� ���� 0���� ����
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        // ���� ���� 1�� 1�� ���� ������ ����
        spriteRenderer.DOFade(1f, 1f);
    }
    void FireFadeOut()
    {
        SpriteRenderer spriteRenderer = Fire.GetComponent<SpriteRenderer>();

        // �ʱ� ���� ���� 1�� ����
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;

        // ���� ���� 0���� 1�� ���� ������ ����
        spriteRenderer.DOFade(0f, 1f);
    }
}

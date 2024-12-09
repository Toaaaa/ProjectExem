using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    public TextMeshProUGUI OptionWindowToggle;
    public TextMeshProUGUI resolution;
    public TextMeshProUGUI language;
    public TextMeshProUGUI exit;

    private void OnEnable()
    {
        canvasGroup.blocksRaycasts = true;
    }
    private void OnDisable()
    {
        canvasGroup.blocksRaycasts = false;
    }

    private void Start()
    {
        OptionWindowToggle.text = GameManager.Instance.localizationManager.GetLocalizedString("OptionWindowToggle");
        resolution.text = GameManager.Instance.localizationManager.GetLocalizedString("Resolution");
        language.text = GameManager.Instance.localizationManager.GetLocalizedString("OptionLanguage");
        exit.text = GameManager.Instance.localizationManager.GetLocalizedString("OptionEndGame");
    }

    public void LanguageChange()//만약 세팅 창에서 언어를 변경 + 클릭하여 적용시 해당 함수를 호출.
    {
        OptionWindowToggle.text = GameManager.Instance.localizationManager.GetLocalizedString("OptionWindowToggle");
        resolution.text = GameManager.Instance.localizationManager.GetLocalizedString("Resolution");
        language.text = GameManager.Instance.localizationManager.GetLocalizedString("OptionLanguage");
        exit.text = GameManager.Instance.localizationManager.GetLocalizedString("OptionEndGame");
    }
}

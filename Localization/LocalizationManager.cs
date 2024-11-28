using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    [SerializeField]
    private string defaultLanguage = "en";

    private string currentLanguage;
    private Dictionary<string, string> localizedTexts;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentLanguage = defaultLanguage;
        LoadLocalizationData();
    }

    private void LoadLocalizationData()
    {
        localizedTexts = new Dictionary<string, string>
        {
            // { "언어_Key", "Value" } Key == ItemData.itemNameKey

            //id = 0
            { "en_Cheap_Portion", "Cheap Portion" },
            { "kr_Cheap_Portion", "싸구려 포션" },
            { "jp_Cheap_Portion", "安いポーション" },
            { "en_Cheap_Portion_Desc", "A cheap portion that restores 10% of HP." },
            { "kr_Cheap_Portion_Desc", "HP 10% 를 회복하는 싸구려 포션." },
            { "jp_Cheap_Portion_Desc", "HP 10% を回復する安いポーション"},
            //id = 1
            { "en_Normal_Portion", "Normal Portion" },
            { "kr_Normal_Portion", "평범한 포션" },
            { "jp_Normal_Portion", "通常ポーション" },
            { "en_Normal_Portion_Desc", "A normal portion that restores 20% of HP." },
            { "kr_Normal_Portion_Desc", "HP 20% 를 회복하는 평범한 포션." },
            { "jp_Normal_Portion_Desc", "HP 20% を回復する通常ポーション"},
            


        };
    }

    public string GetLocalizedString(string key)
    {
        //UI등에서 가져올 텍스트 데이터의 경우
        //text.text = LocalizationManager.Instance.GetLocalizedString(ItemData.itemNameKey); 으로 사용.

        string fullKey = $"{currentLanguage}_{key}";
        if (localizedTexts.TryGetValue(fullKey, out string localizedText))
        {
            return localizedText;
        }

        // 키가 없을 경우 기본값 반환
        return key;
    }

    public void SetLanguage(string languageCode) //설정에서 해당 함수에 접근하여 언어 변경.
    {
        currentLanguage = languageCode;
        // en, kr, jp 등의 언어 코드를 받아서 변경
    }
}

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
            { "en_Cheap_Potion", "Cheap Potion" },
            { "kr_Cheap_Potion", "싸구려 포션" },
            { "jp_Cheap_Potion", "安いポーション" },
            { "en_Cheap_Potion_Desc", "A cheap portion that restores 10% of HP." },
            { "kr_Cheap_Potion_Desc", "HP 10% 를 회복하는 싸구려 포션." },
            { "jp_Cheap_Potion_Desc", "HP 10% を回復する安いポーション"},
            //id = 1
            { "en_Normal_Potion", "Normal Potion" },
            { "kr_Normal_Potion", "평범한 포션" },
            { "jp_Normal_Potion", "通常ポーション" },
            { "en_Normal_Potion_Desc", "A normal portion that restores 25% of HP." },
            { "kr_Normal_Potion_Desc", "HP 25% 를 회복하는 평범한 포션." },
            { "jp_Normal_Potion_Desc", "HP 25% を回復する通常ポーション"},
            //id = 2
            { "en_Great_Potion", "Great Potion" },
            { "kr_Great_Potion", "훌륭한 포션" },
            { "jp_Great_Potion", "素晴らしいポーション" },
            { "en_Great_Potion_Desc", "A great portion that restores 50% of HP." },
            { "kr_Great_Potion_Desc", "HP 50% 를 회복하는 훌륭한 포션." },
            { "jp_Great_Potion_Desc", "HP 50% を回復する素晴らしいポーション"},
            //id = 3
            { "en_Elixir", "Elixir" },
            { "kr_Elixir", "엘릭서" },
            { "jp_Elixir", "エリクサー" },
            { "en_Elixir_Desc", "A powerful portion that restores 100% of HP." },
            { "kr_Elixir_Desc", "HP 100% 를 회복하는 강력한 포션." },
            { "jp_Elixir_Desc", "HP 100% を回復する強力なポーション"},
            //id = 4
            { "en_Mythic_Elixir", "Mythic Elixir" },
            { "kr_Mythic_Elixir", "신화의 엘릭서" },
            { "jp_Mythic_Elixir", "神話のエリクサー" },
            { "en_Mythic_Elixir_Desc", "Revives the dead with 50% of HP." },
            { "kr_Mythic_Elixir_Desc", "HP 50% 를 회복하며 죽은 자를 부활시키는 엘릭서." },
            { "jp_Mythic_Elixir_Desc", "HP 50% を回復し、死者を蘇らせるエリクサー"},

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

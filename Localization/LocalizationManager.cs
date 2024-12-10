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
        //GameManager.Instance.localizationManager = this;
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

            //UI 텍스트//
            //상점
            { "en_ShopText1", "Are you going to buy this item? " },
            { "kr_ShopText1", "이 아이템을 구매하시겠습니까?" },
            { "jp_ShopText1", "このアイテムを購入しますか？" },
            { "en_ShopText2", "Yes" },
            { "kr_ShopText2", "네" },
            { "jp_ShopText2", "はい" },
            { "en_ShopText3", "No" },
            { "kr_ShopText3", "아니요" },
            { "jp_ShopText3", "いいえ" },
            { "en_ShopText4", "You don't have enough gold." },
            { "kr_ShopText4", "골드가 부족합니다." },
            { "jp_ShopText4", "ゴールドが足りません。" },
            { "en_ShopText5", "Close" },
            { "kr_ShopText5", "닫기" },
            { "jp_ShopText5", "閉じる" },

            //메인 로비
            { "en_ShopButton", "Go to Shop" },
            { "kr_ShopButton", "상점으로 이동" },
            { "jp_ShopButton", "ショップへ" },
            { "en_TrainingButton", "Go to Training" },
            { "kr_TrainingButton", "훈련장으로 이동" },
            { "jp_TrainingButton", "トレーニングへ" },
            { "en_PrepareButton", "Prepare for the Dungeon" },
            { "kr_PrepareButton", "던전입장 준비" },
            { "jp_PrepareButton", "ダンジョンへ" },
            { "en_InventoryButton", "Go to Inventory" },
            { "kr_InventoryButton", "인벤토리로 이동" },
            { "jp_InventoryButton", "インベントリへ" },
            { "en_OptionButton", "Option" },
            { "kr_OptionButton", "옵션" },
            { "jp_OptionButton", "オプション" },
            { "en_ExitButton", "Exit" },
            { "kr_ExitButton", "게임종료" },
            { "jp_ExitButton", "ゲーム終了" },

            //옵션
            { "en_OptionWindowToggle", "Window mode Toggle" },
            { "kr_OptionWindowToggle", "창모드 토글" },
            { "jp_OptionWindowToggle", "ウィンドウモードトグル" },
            { "en_OptionLanguage", "Language :" },
            { "kr_OptionLanguage", "언어 :" },
            { "jp_OptionLanguage", "言語 :" },
            { "en_OptionEndGame", "End Game" },
            { "kr_OptionEndGame", "게임종료" },
            { "jp_OptionEndGame", "ゲーム終了" },
            { "en_Resolution", "Resolution" },
            { "kr_Resolution", "해상도" },
            { "jp_Resolution", "解像度" },
            //아이템 텍스트//
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

    public void SetLanguage(string languageCode)//해당 함수를 호출하여 언어 변경
    {
        currentLanguage = languageCode;
        // en, kr, jp 등의 언어 코드를 받아서 변경

        // PlayerPrefs에 저장
        PlayerPrefs.SetString("Language", languageCode);
        PlayerPrefs.Save();

        Debug.Log($"언어가 변경되었습니다: {languageCode}");

        // 변경된 언어를 UI에 반영
        UpdateAllLocalizedTexts();
    }

    public void LoadLanguage()
    {
        // 저장된 언어가 있다면 로드, 없으면 기본값 사용
        currentLanguage = PlayerPrefs.GetString("Language", defaultLanguage);
        Debug.Log($"언어 로드: {currentLanguage}");
    }

    private void UpdateAllLocalizedTexts()
    {
        // UI 텍스트 컴포넌트를 모두 찾아서 갱신
        LocalizedText[] localizedTextComponents = FindObjectsOfType<LocalizedText>();
        foreach (var localizedText in localizedTextComponents)
        {
            localizedText.UpdateText();
        }
    }
}

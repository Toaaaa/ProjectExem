using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageDropdown : MonoBehaviour
{
    public TMP_Dropdown languageDropdown; // 연결된 Dropdown
    private string[] supportedLanguages = { "en", "kr", "jp" }; // 지원 언어 목록

    private void Start()
    {
        InitializeDropdown();
    }

    private void InitializeDropdown()
    {
        languageDropdown.ClearOptions();

        // 언어 이름 표시 (UI 텍스트와 무관한 단순 표시용)
        var options = new List<string> { "English", "한국어", "日本語" };
        languageDropdown.AddOptions(options);

        // 현재 언어를 선택 상태로 설정
        string currentLanguage = PlayerPrefs.GetString("Language", "en");
        int selectedIndex = System.Array.IndexOf(supportedLanguages, currentLanguage);
        languageDropdown.value = selectedIndex >= 0 ? selectedIndex : 0;
        languageDropdown.RefreshShownValue();

        // Dropdown 값 변경 이벤트 연결
        languageDropdown.onValueChanged.AddListener(SetLanguageFromDropdown);
    }

    private void SetLanguageFromDropdown(int index)
    {
        if (index >= 0 && index < supportedLanguages.Length)
        {
            string selectedLanguage = supportedLanguages[index];
            LocalizationManager.Instance.SetLanguage(selectedLanguage);
        }
    }
}

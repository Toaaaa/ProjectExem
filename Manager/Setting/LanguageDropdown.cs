using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageDropdown : MonoBehaviour
{
    public TMP_Dropdown languageDropdown; // ����� Dropdown
    private string[] supportedLanguages = { "en", "kr", "jp" }; // ���� ��� ���

    private void Start()
    {
        InitializeDropdown();
    }

    private void InitializeDropdown()
    {
        languageDropdown.ClearOptions();

        // ��� �̸� ǥ�� (UI �ؽ�Ʈ�� ������ �ܼ� ǥ�ÿ�)
        var options = new List<string> { "English", "�ѱ���", "������" };
        languageDropdown.AddOptions(options);

        // ���� �� ���� ���·� ����
        string currentLanguage = PlayerPrefs.GetString("Language", "en");
        int selectedIndex = System.Array.IndexOf(supportedLanguages, currentLanguage);
        languageDropdown.value = selectedIndex >= 0 ? selectedIndex : 0;
        languageDropdown.RefreshShownValue();

        // Dropdown �� ���� �̺�Ʈ ����
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown; // ����� Dropdown
    public Toggle fullscreenToggle; // ��ü ȭ�� ���
    private Resolution[] resolutions;  // ���� �ػ� ���
    private List<string> options = new List<string>(); // �ػ� �ɼ� ����Ʈ

    void Start()
    {
        // ���� ��ü ȭ�� ���¸� Toggle�� �ݿ�
        fullscreenToggle.isOn = Screen.fullScreen;

        // Toggle �̺�Ʈ ������ �߰�
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // ���� �ػ� ��������
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions(); // ���� �ɼ� �ʱ�ȭ

        // �ɼ� ����Ʈ�� �ػ� �߰�
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width < 1024 || resolutions[i].width > 4000) continue; // Ư�� �ػ� ����
            string option = $"{resolutions[i].width} x {resolutions[i].height} ";
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options); // �ɼ� �߰�

        // ����� �ػ� �ε�
        currentResolutionIndex = LoadResolution();
        resolutionDropdown.value = currentResolutionIndex; // ���� �ػ󵵷� �ʱ�ȭ
        resolutionDropdown.RefreshShownValue(); // UI ����

        // Dropdown �̺�Ʈ ������ �߰�
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        aspectRatioSet();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SaveResolution(Screen.width, Screen.height, isFullscreen);
    }
    // �ػ� ���� �޼���
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        SetRes(resolution.width, resolution.height);
        SaveResolution(resolution.width, resolution.height, Screen.fullScreen);

        aspectRatioSet();

        // �ػ� ���� �� ��Ӵٿ� ����
        resolutionDropdown.value = resolutionIndex; // ��Ӵٿ� ����
        resolutionDropdown.RefreshShownValue(); // UI ����

        Debug.Log($"�ػ� ����: {resolution.width} x {resolution.height}");
    }

    public void SaveResolution(int width, int height, bool isFullscreen)
    {
        // �ػ󵵿� ��ü ȭ�� ���� ����
        PlayerPrefs.SetInt("ResolutionWidth", width);
        PlayerPrefs.SetInt("ResolutionHeight", height);
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetRes(int setWidth,int setHeight)
    {

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }
    public int LoadResolution()
    {
        // ����� ���� �ִ��� Ȯ��
        if (PlayerPrefs.HasKey("ResolutionWidth") && PlayerPrefs.HasKey("ResolutionHeight"))
        {
            int width = PlayerPrefs.GetInt("ResolutionWidth");
            int height = PlayerPrefs.GetInt("ResolutionHeight");
            bool isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;
            fullscreenToggle.isOn = isFullscreen;

            // �ػ� ���� ����
            Screen.SetResolution(width, height, isFullscreen);

            // ����� �ػ󵵸� ���� ��Ͽ��� �˻�
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == width && resolutions[i].height == height)
                    return i; // �ش� �ػ� �ε��� ��ȯ
            }
        }

        // ����� ���� ���ų� �ػ󵵰� ���� ��� �⺻�� (1920x1080, ��ü ȭ��)
        Screen.SetResolution(1920, 1080, true);
        Debug.Log("�⺻ �ػ� �ε�: 1920 x 1080");

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == 1920 && resolutions[i].height == 1080)
                return i; // �⺻ �ػ� �ε��� ��ȯ
        }

        return 0; // �⺻���� ���� ��� ù ��° �ɼ� ����
    }

    void aspectRatioSet()
    {
        float targetAspect = 16.0f / 9.0f;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }
    }
}

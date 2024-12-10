using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown; // 연결된 Dropdown
    public Toggle fullscreenToggle; // 전체 화면 토글
    private Resolution[] resolutions;  // 지원 해상도 목록
    private List<string> options = new List<string>(); // 해상도 옵션 리스트

    void Start()
    {
        // 현재 전체 화면 상태를 Toggle에 반영
        fullscreenToggle.isOn = Screen.fullScreen;

        // Toggle 이벤트 리스너 추가
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // 지원 해상도 가져오기
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions(); // 기존 옵션 초기화

        // 옵션 리스트에 해상도 추가
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width < 1024 || resolutions[i].width > 4000) continue; // 특정 해상도 제외
            string option = $"{resolutions[i].width} x {resolutions[i].height} ";
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options); // 옵션 추가

        // 저장된 해상도 로드
        currentResolutionIndex = LoadResolution();
        resolutionDropdown.value = currentResolutionIndex; // 현재 해상도로 초기화
        resolutionDropdown.RefreshShownValue(); // UI 갱신

        // Dropdown 이벤트 리스너 추가
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        aspectRatioSet();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SaveResolution(Screen.width, Screen.height, isFullscreen);
    }
    // 해상도 변경 메서드
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        SetRes(resolution.width, resolution.height);
        SaveResolution(resolution.width, resolution.height, Screen.fullScreen);

        aspectRatioSet();

        // 해상도 변경 후 드롭다운 갱신
        resolutionDropdown.value = resolutionIndex; // 드롭다운 갱신
        resolutionDropdown.RefreshShownValue(); // UI 갱신

        Debug.Log($"해상도 변경: {resolution.width} x {resolution.height}");
    }

    public void SaveResolution(int width, int height, bool isFullscreen)
    {
        // 해상도와 전체 화면 설정 저장
        PlayerPrefs.SetInt("ResolutionWidth", width);
        PlayerPrefs.SetInt("ResolutionHeight", height);
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetRes(int setWidth,int setHeight)
    {

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }
    public int LoadResolution()
    {
        // 저장된 값이 있는지 확인
        if (PlayerPrefs.HasKey("ResolutionWidth") && PlayerPrefs.HasKey("ResolutionHeight"))
        {
            int width = PlayerPrefs.GetInt("ResolutionWidth");
            int height = PlayerPrefs.GetInt("ResolutionHeight");
            bool isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;
            fullscreenToggle.isOn = isFullscreen;

            // 해상도 설정 적용
            Screen.SetResolution(width, height, isFullscreen);

            // 저장된 해상도를 지원 목록에서 검색
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == width && resolutions[i].height == height)
                    return i; // 해당 해상도 인덱스 반환
            }
        }

        // 저장된 값이 없거나 해상도가 없을 경우 기본값 (1920x1080, 전체 화면)
        Screen.SetResolution(1920, 1080, true);
        Debug.Log("기본 해상도 로드: 1920 x 1080");

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == 1920 && resolutions[i].height == 1080)
                return i; // 기본 해상도 인덱스 반환
        }

        return 0; // 기본값이 없을 경우 첫 번째 옵션 선택
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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    Toggle fullScreenToggle;

    [SerializeField]
    Slider audioSlider;
    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    Dropdown graphicsQualityDropdown;

    [SerializeField]
    Dropdown resolutionDropdown;
    Resolution[] permissions;

    [SerializeField]
    Button mainMenuButton;
    [SerializeField]
    GameObject mainMenuObj;

    void Start()
    {
        fullScreenToggle.isOn = Screen.fullScreen;

        //Устанавливаем ползунок громкости в позицию соответствующую audioMixer
        audioMixer.GetFloat("masterVol", out float x);
        audioSlider.value  = x;

        //Устанавливаем позицию графики в текущую
        graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();

        fullScreenToggle.onValueChanged.AddListener(delegate {
            FullScreenToggle(fullScreenToggle);
        });

        audioSlider.onValueChanged.AddListener(delegate {
            AudioVolume(audioSlider);
        });

        graphicsQualityDropdown.onValueChanged.AddListener(delegate
        {
            GraphicsQuality(graphicsQualityDropdown.value);
        });

        resolutionDropdown.onValueChanged.AddListener(delegate
        {
            AppointResolution(permissions[resolutionDropdown.value], fullScreenToggle.isOn);
        });

        mainMenuButton.onClick.AddListener(delegate
        {
            MainPressed(mainMenuObj);
        });
    }

    private void Awake()
    {
        //List нужен для заполнения resolutionDropdown
        List<string> resolutionStr = new List<string>();
        permissions = Screen.resolutions;
        int currentRsl = 0;
        int i = 0;
        foreach (var permission in permissions)
        {
            resolutionStr.Add(permission.width + "x" + permission.height);

            //Выставляем значение resolutionDropdown на текущее
            Resolution currentResolution  = Screen.currentResolution;
            currentResolution.refreshRate = permission.refreshRate;
            if (currentResolution.Equals(permission))
                currentRsl = i;
            i++;
        }

        //Очищаем Dropdown Resolution
        resolutionDropdown.ClearOptions();
        //Заполняем Dropdown списком resolutionStr
        resolutionDropdown.AddOptions(resolutionStr);

        resolutionDropdown.value = currentRsl;
    }

    void FullScreenToggle(Toggle toggle)
    {
        Screen.fullScreen = toggle.isOn;
    }

    void AudioVolume(Slider slider)
    {
        audioMixer.SetFloat("masterVol", slider.value);
    }

    void GraphicsQuality(int dropdown)
    {
        QualitySettings.SetQualityLevel(dropdown);
    }

    void AppointResolution(Resolution resolutions, bool fullscreen)
    {
        _ = resolutions;
        Screen.SetResolution(resolutions.width, resolutions.height, fullscreen);
        _ = Screen.currentResolution;
    }

    void MainPressed(GameObject obj)
    {
        gameObject.SetActive(false);
        obj.SetActive(true);
    }
}

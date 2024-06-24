using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    private const string FULLSCREEN_KEY = "fullscreen";
    private const string RESOLUTION_WIDTH_KEY = "resolution_width";
    private const string RESOLUTION_HEIGHT_KEY = "resolution_height";
    private const string SENSITIVITY_KEY = "sensitivity";

    public Dropdown fullscreenDropdown;
    public Dropdown resolutionDropdown;
    public Slider sensitivitySlider;
    public Text sensitivityValueText;

    // Adicione as resoluções disponíveis
    private Resolution[] resolutions = new Resolution[]
    {
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1366, height = 768 },
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1024, height = 768 }
    };

    void Start()
    {
        // Define a opção selecionada no Dropdown com base nas configurações salvas
        fullscreenDropdown.value = GetFullscreen() ? 0 : 1;
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionOptions.Add(resolutions[i].width + "x" + resolutions[i].height);
            if (resolutions[i].width == GetResolutionWidth() && resolutions[i].height == GetResolutionHeight())
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);

        resolutionDropdown.value = currentResolutionIndex;

        sensitivitySlider.value = GetSensitivity(); // Define o valor inicial do slider
        sensitivitySlider.onValueChanged.AddListener(OnSensitivitySliderValueChanged); // Listener para mudanças no slider

        UpdateSensitivityText(sensitivitySlider.value);
        // Aplica as configurações salvas
        ApplyScreenSettings();
    }

    // Função chamada quando a opção do dropdown é alterada
    public void OnDropdownValueChanged()
    {
        SetFullscreen(fullscreenDropdown.value == 0);
        ApplyScreenSettings();
    }

    public void OnResolutionDropdownValueChanged()
    {
        SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height);
        ApplyScreenSettings();
    }

    private void ApplyScreenSettings()
    {
        // Aplica as configurações de fullscreen
        Screen.fullScreen = GetFullscreen();

        // Aplica a resolução
        Screen.SetResolution(GetResolutionWidth(), GetResolutionHeight(), GetFullscreen());
    }

    public void SetFullscreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt(FULLSCREEN_KEY, isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool GetFullscreen()
    {
        return PlayerPrefs.GetInt(FULLSCREEN_KEY, 1) == 1;
    }

    public void SetResolution(int width, int height)
    {
        PlayerPrefs.SetInt(RESOLUTION_WIDTH_KEY, width);
        PlayerPrefs.SetInt(RESOLUTION_HEIGHT_KEY, height);
        PlayerPrefs.Save();
    }

    public int GetResolutionWidth()
    {
        return PlayerPrefs.GetInt(RESOLUTION_WIDTH_KEY, 1920);
    }

    public int GetResolutionHeight()
    {
        return PlayerPrefs.GetInt(RESOLUTION_HEIGHT_KEY, 1080);
    }

    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat(SENSITIVITY_KEY, sensitivity);
        PlayerPrefs.Save();
    }

    public float GetSensitivity()
    {
        return PlayerPrefs.GetFloat(SENSITIVITY_KEY, 1.0f); // Valor padrão 1.0f
    }

    public void OnSensitivitySliderValueChanged(float value)
    {
        SetSensitivity(value);
        UpdateSensitivityText(value);
    }

    private void UpdateSensitivityText(float value)
    {
        sensitivityValueText.text = value.ToString("F2"); // Formata o valor para duas casas decimais
    }
}

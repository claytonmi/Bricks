﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    private const string FULLSCREEN_KEY = "fullscreen";
    private const string RESOLUTION_WIDTH_KEY = "resolution_width";
    private const string RESOLUTION_HEIGHT_KEY = "resolution_height";

    public Dropdown fullscreenDropdown;
    public Dropdown resolutionDropdown;

    // Adicione as resoluções disponíveis
    private Resolution[] resolutions = new Resolution[]
    {
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1366, height = 768 },
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1024, height = 768 },
        new Resolution { width = 854, height = 480 }
    };

    void Start()
    {
        // Define a opção selecionada no Dropdown com base nas configurações salvas
        fullscreenDropdown.value = GetFullscreen() ? 0 : 1;
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionOptions.Add(resolutions[i].width + "x" + resolutions[i].height);
            if (resolutions[i].width == GetResolutionWidth() && resolutions[i].height == GetResolutionHeight())
            {
                resolutionDropdown.value = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);

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
}
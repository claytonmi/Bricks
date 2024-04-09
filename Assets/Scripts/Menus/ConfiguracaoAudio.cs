using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfiguracaoAudio : MonoBehaviour
{
    public Toggle toggleMudo;
    public Slider sliderVolume;
    public AudioSource audioSource;
    public GameObject panelConfiguracao;
    public bool mutado;
    public Button btNovoJogo;
    public Button btInfo;
    public Button btConfiguracao;
    public Button btSair;
    public InputField TextoInput;

    private void Start()
    {
        // Inicialize o estado dos elementos de UI com base nas configurações de áudio atuais
        toggleMudo.isOn = audioSource.mute;
        sliderVolume.value = audioSource.volume * 10f;
        mutado = true;
    }

    public void SetMute()
    {
        if (mutado)
        {
            audioSource.mute = mutado;
            mutado = false;
        }
        else
        {
            audioSource.mute = mutado;
            mutado = true;
        }
    }

    public void SetVolume()
    {
        audioSource.volume = sliderVolume.value;
    }

    public void FecharConfig()
    {
        panelConfiguracao.gameObject.SetActive(false);
        TextoInput.interactable = true;
        btNovoJogo.interactable = true;
        btInfo.interactable = true;
        btConfiguracao.interactable = true;
        btSair.interactable = true;
    }
}

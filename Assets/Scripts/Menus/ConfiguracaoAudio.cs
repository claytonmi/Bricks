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
    public Button btSalvarClassificacao;
    public Button btConfiguracao;
    public Button btCreditos;
    public Button BtClassificacao;
    public Button btSair;
    public InputField TextoInput;

    private void Start()
    {
        // Se não retornou de uma fase, define o volume máximo e som ligado
        if (!PlayerPrefs.HasKey("RetornouDaFase"))
        {
            mutado = false; // Som ligado
            audioSource.volume = 1f; // Volume máximo
            PlayerPrefs.SetInt("Muted", mutado ? 1 : 0); // Salva o estado do som nos PlayerPrefs
            PlayerPrefs.SetFloat("Volume", 1f); // Salva o volume nos PlayerPrefs                                                
            toggleMudo.isOn = audioSource.mute;
            sliderVolume.value = audioSource.volume;
        }
        else // Se retornou de uma fase, carrega as configurações dos PlayerPrefs
        {
             // Carrega o estado do som dos PlayerPrefs
            audioSource.mute = PlayerPrefs.GetInt("Muted") == 1;
            audioSource.volume = PlayerPrefs.GetFloat("Volume", 1f); // Carrega o volume dos PlayerPrefs
            toggleMudo.isOn = audioSource.mute;
            sliderVolume.value = audioSource.volume;
        }
    }

    public void SetMute()
    {
        mutado = !mutado; // Inverte o estado de mutado

        audioSource.mute = mutado;

        PlayerPrefs.SetInt("Muted", mutado ? 1 : 0); // Salva o estado do som nos PlayerPrefs
        PlayerPrefs.Save();
    }

    public void SetVolume()
    {
        audioSource.volume = sliderVolume.value;
        PlayerPrefs.SetFloat("Volume", sliderVolume.value);
        PlayerPrefs.Save();
    }

    public void FecharConfig()
    {
        panelConfiguracao.gameObject.SetActive(false);
        TextoInput.interactable = true;
        btNovoJogo.interactable = true;
        btInfo.interactable = true;
        btSalvarClassificacao.interactable = true;
        btConfiguracao.interactable = true;
        btCreditos.interactable = true;
        BtClassificacao.interactable = true;
        btSair.interactable = true;
    }

    public void FecharConfigEmJogo()
    {
        panelConfiguracao.gameObject.SetActive(false);
    }

    public void AbrirConfigEmJogo()
    {
        panelConfiguracao.gameObject.SetActive(true);
    }

}

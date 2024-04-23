using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Image playerImage; // Referência ao sprite do jogador
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    private Color playerColor; // Cor atual do jogador

    private void Start()
    {
        // Carrega a cor do jogador do PlayerPrefs ou usa azul como padrão
        SetPlayerColorFromPrefs();
    }

    private void SetPlayerColorFromPrefs()
    {
        Color playerColor;

        // Verifica se há uma cor salva no PlayerPrefs
        if (PlayerPrefs.HasKey("PlayerColorR") && PlayerPrefs.HasKey("PlayerColorG") && PlayerPrefs.HasKey("PlayerColorB"))
        {
            float r = PlayerPrefs.GetFloat("PlayerColorR");
            float g = PlayerPrefs.GetFloat("PlayerColorG");
            float b = PlayerPrefs.GetFloat("PlayerColorB");
            playerColor = new Color(r, g, b);
        }
        else
        {
            playerColor = Color.blue; // Cor azul padrão
        }

        // Aplica a cor ao jogador
        playerImage.color = playerColor;

        // Atualiza os valores dos sliders com base na cor atual do jogador
        redSlider.value = playerColor.r;
        greenSlider.value = playerColor.g;
        blueSlider.value = playerColor.b;
    }

    // Método chamado a cada frame
    private void Update()
    {
        // Atualiza a cor do jogador com base nos valores dos sliders
        playerColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        playerImage.color = playerColor;

        // Salva a cor selecionada no PlayerPrefs
        PlayerPrefs.SetFloat("PlayerColorR", playerColor.r);
        PlayerPrefs.SetFloat("PlayerColorG", playerColor.g);
        PlayerPrefs.SetFloat("PlayerColorB", playerColor.b);
        PlayerPrefs.Save(); // Salva as alterações
    }
}
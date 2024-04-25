using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    public float speed = 5.0f; // Velocidade de rolagem dos créditos
    public Text textCreditos; // Referência ao objeto de texto dos créditos
    public float creditEndPositionY = 500f;

    private bool isCreditsFinished = false;
    void Update()
    {
       // Verifica se os créditos terminaram
        if (!isCreditsFinished)
        {
            // Move os créditos para cima
            textCreditos.transform.Translate(Vector3.up* speed * Time.deltaTime);

            // Verifica se os créditos alcançaram a posição final
            if (textCreditos.rectTransform.localPosition.y >= creditEndPositionY)
            {
                // Define a flag para indicar que os créditos terminaram
                isCreditsFinished = true;
                
                // Chama a função para carregar a cena do menu após um pequeno atraso
                Invoke("LoadMenuScene", 1f);
}
        }
    }

    // Função para carregar a cena do menu
    void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
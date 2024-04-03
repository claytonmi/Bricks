using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{

    public Text TextoDoInput;
    public string nomeJogador;


    public void ChamaFase()
    {
        if (TextoDoInput.text != "")
        {
            SalvarNome();
            SceneManager.LoadScene("Fase 1");
        }        
    }

    public void Sair()
    {
        Application.Quit();
    }

   public void SalvarNome()
    {
        nomeJogador = TextoDoInput.text;
        Debug.Log(nomeJogador);
        PlayerPrefs.SetString("Nome", nomeJogador);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("Nome"));
    }

    public void Informacoes()
    {
        SceneManager.LoadScene("Informacoes");
    }

    public void VoltaMenu()
    {
         SceneManager.LoadScene("Menu");        
    }
}

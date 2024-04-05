using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{

    public Text TextoDoInput;
    public string nomeJogador;
    public Button botaoFase1;
    public Button botaoFase2;
    public Button botaoFase3;

    public void ChamaFase()
    {
        
        if (TextoDoInput.text == "Test")
        {
            botaoFase1.gameObject.SetActive(true);
            botaoFase2.gameObject.SetActive(true);
            botaoFase3.gameObject.SetActive(true);

        }
        else if (TextoDoInput.text != "")
        {
            botaoFase1.gameObject.SetActive(false);
            botaoFase2.gameObject.SetActive(false);
            botaoFase3.gameObject.SetActive(false);
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

    public void IniciarFase1()
    {
        SalvarNome();
        SceneManager.LoadScene("Fase 1");
    }

    public void IniciarFase2()
    {
        SalvarNome();
        SceneManager.LoadScene("Fase 2");
    }

    public void IniciarFase3()
    {
        SalvarNome();
        SceneManager.LoadScene("Fase 3");
    }
}

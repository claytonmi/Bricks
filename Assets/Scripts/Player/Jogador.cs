using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{
    public Text nomeJogador;

    // Start is called before the first frame update
    void Start()
    {
        if (nomeJogador != null)
        {
            nomeJogador.text = PlayerPrefs.GetString("Nome");
        }
        else
        {
            nomeJogador.text = "Teste";
            Debug.LogWarning("O componente Text nomeJogador não foi atribuído corretamente no Editor Unity.");
        }
    }

    public string getNomePlayer()
    {
        return nomeJogador.text;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

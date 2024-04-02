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
        nomeJogador.text = PlayerPrefs.GetString("Nome");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

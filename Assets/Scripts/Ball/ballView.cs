using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ballView : MonoBehaviour
{
    // Start is called before the first frame update

    private ballController _ballController;
    private ballModel _ballModel;
    public brickModel _brickModel;
    public SpriteState _Vida;
    public GameObject _PainelGameOver, PainelVitoria, Vida1, Vida2, Vida3, DestinoTeleport;
    public GameObject Brick;
    public int Pontuacao;
    public Text PontuacaoTexto;
    public Text PontuacaoVitoria;    
   
    public Text pontuacaoGameOver;
  


    void Start()
    {
        _ballController = GetComponent<ballController>();
        _ballModel = GetComponent<ballModel>();
        Time.timeScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            brickView _brickView = collision.gameObject.GetComponent<brickView>();
            _brickView.PerformTakeDamage(1, collision);
          }
        else  if (collision.gameObject.tag == "Enemy2")
        {
            brickView _brickView = collision.gameObject.GetComponent<brickView>();
            _brickView.PerformTakeDamage(1, collision);
        }
        else  if (collision.gameObject.tag == "Enemy3")
        {
            brickView _brickView = collision.gameObject.GetComponent<brickView>();
            _brickView.PerformTakeDamage(1, collision);
        }
        else if (collision.gameObject.tag == "Enemy4")
        {
            brickView _brickView = collision.gameObject.GetComponent<brickView>();
            _brickView.PerformTakeDamage(1, collision);
        }

        if (collision.gameObject.tag == "Finish")
        { 
            _ballModel.Speed = 0f;
            _ballModel.Power = 0f;

            ReiniciarFase();
        }

        if (collision.gameObject.tag != "Player")
        {
            _ballController.PerfectAngleReflect(collision);
        }
        else
        {
            Vector2 newBalldirection = _ballController.CalcBallAngleReflect(collision);
            PerformAngleChange(newBalldirection);
        }

    }

    public void Update()
    {
        ganhamoMae();
    }

    public void atualizaPontuacao(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Pontuacao = Pontuacao + 10;
            PontuacaoTexto.text = Pontuacao.ToString();
        }
        else if (collision.gameObject.tag == "Enemy2")
        {
            Pontuacao = Pontuacao + 2;
            PontuacaoTexto.text = Pontuacao.ToString();
        }
        else if (collision.gameObject.tag == "Enemy3")
        {
            Pontuacao = Pontuacao + 5;
            PontuacaoTexto.text = Pontuacao.ToString();
        }
        else if (collision.gameObject.tag == "Enemy4")
        {
            Pontuacao = Pontuacao + 15;
            PontuacaoTexto.text = Pontuacao.ToString();
        }

        PlayerPrefs.SetInt("PontuacaoMaxima", Pontuacao);
        PontuacaoVitoria.text = PlayerPrefs.GetInt("PontuacaoMaxima").ToString();
        pontuacaoGameOver.text = PlayerPrefs.GetInt("PontuacaoMaxima").ToString();
        Debug.Log(PlayerPrefs.GetInt("PontuacaoMaxima"));
    }

    public void ganhamoMae()
    {

        if (GameObject.FindWithTag("Enemy") == null)
        {
            if (GameObject.FindWithTag("Enemy2") == null)
            {
                if (GameObject.FindWithTag("Enemy3") == null)
                {
                    if (GameObject.FindWithTag("Enemy4") == null)
                    {

                        if (SceneManager.GetActiveScene().name == "Fase 1")
                        {
                            SceneManager.LoadScene("Fase 2");
                        }
                        else
                        {
                            Time.timeScale = 0;
                            PainelVitoria.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    public void PerformAngleChange(Vector2 direcao)
    {
        _ballController.AngleChange(direcao);
    }

    public void Sair() {
        Application.Quit();
    }

    public void ReiniciarFase1()
    {
        SceneManager.LoadScene("Fase 1");        
    }

    public void ReiniciarFase2()
    {
        SceneManager.LoadScene("Fase 2");
    }

    public void ReiniciarFase3()
    {
        SceneManager.LoadScene("Fase 3");
    }
    public void VoltarMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void ReiniciarFase()
    {
        if(Vida3.activeInHierarchy == true)
        {
            Vida3.SetActive(false);
            transform.position = DestinoTeleport.transform.position;
            _ballModel.Speed = 3f;
            _ballModel.Power = 1f;
        }
        else if (Vida2.activeInHierarchy == true)
        {
            Vida2.SetActive(false);
            transform.position = DestinoTeleport.transform.position;
            _ballModel.Speed = 3f;
            _ballModel.Power = 1f;
        }
        else if(Vida1.activeInHierarchy == true)
        {
            Vida1.SetActive(false);
            transform.position = DestinoTeleport.transform.position;
            _ballModel.Speed = 3f;
            _ballModel.Power = 1f;
        }
        else
        {
            _PainelGameOver.SetActive(true);
        }
    }
}

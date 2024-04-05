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

    public GameObject _PainelGameOver, PainelVitoria, Vida1, Vida2, Vida3, DestinoTeleport, Brick; 
    public Text PontuacaoTexto, PontuacaoVitoria, pontuacaoGameOver;


    public int Pontuacao;
    private bool jogoPausado = false;

    private void Start()
    {

        InitializeComponents();
        Time.timeScale = 1;
        // Definindo o texto inicial como zero
        if (PontuacaoTexto != null)
        {
            PontuacaoTexto.text = "0";
        }
        if (PontuacaoVitoria != null)
        {
            PontuacaoVitoria.text = "0";
        }
        if (pontuacaoGameOver != null)
        {
            pontuacaoGameOver.text = "0";
        }
    }

    private void InitializeComponents()
    {
        _ballController = GetComponent<ballController>();
        _ballModel = GetComponent<ballModel>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy2" || collision.gameObject.tag == "Enemy3" || collision.gameObject.tag == "Enemy4")
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
        ResetarVelocidade();

    }

    public void OnClick()
    {

        if (_ballController != null)
        {
            if (jogoPausado)
            {
                Debug.Log(" RetomarBola");
                _ballController.RetomarBola();
            }
            else
            {
                Debug.Log("PauseBola: ");
                _ballController.PausarBola();
            }
            jogoPausado = !jogoPausado;
        }
        else
        {
            Debug.LogWarning("O ballController não está inicializado corretamente. Verifique se ele está anexado ao GameObject ou se o InitializeComponents() está sendo chamado.");
        }
    }

    public void Update()
    {
        ganhamoMae();
    }

    public void atualizaPontuacao(Collision2D collision)
    {
        if (collision.gameObject != null)
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                    Pontuacao += 10;
                    break;
                case "Enemy2":
                    Pontuacao += 2;
                    break;
                case "Enemy3":
                    Pontuacao += 5;
                    break;
                case "Enemy4":
                    Pontuacao += 15;
                    break;
            }

            PontuacaoTexto.text = Pontuacao.ToString();
            PlayerPrefs.SetInt("PontuacaoMaxima", Pontuacao);
            PontuacaoVitoria.text = PlayerPrefs.GetInt("PontuacaoMaxima").ToString();
            pontuacaoGameOver.text = PlayerPrefs.GetInt("PontuacaoMaxima").ToString();
            Debug.Log(PlayerPrefs.GetInt("PontuacaoMaxima"));
        }
        else
        {
            Debug.LogWarning("Collision object is null");
        }
    }

    public void ganhamoMae()
    {
        bool noEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length == 0 &&
                         GameObject.FindGameObjectsWithTag("Enemy2").Length == 0 &&
                         GameObject.FindGameObjectsWithTag("Enemy3").Length == 0 &&
                         GameObject.FindGameObjectsWithTag("Enemy4").Length == 0;

        if (noEnemies)
        {
            string activeSceneName = SceneManager.GetActiveScene().name;

            switch (activeSceneName)
            {
                case "Fase 1":
                    SceneManager.LoadScene("Fase 2");
                    break;
                case "Fase 2":
                    SceneManager.LoadScene("Fase 3");
                    break;
                default:
                    Time.timeScale = 0;
                    PainelVitoria.SetActive(true);
                    break;
            }
        }
    }

        public void PerformAngleChange(Vector2 direcao)
    {
        _ballController.AngleChange(direcao);
    }

    public void Sair()
    {
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
    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ReiniciarFase()
    {
        if (Vida3.activeInHierarchy || Vida2.activeInHierarchy || Vida1.activeInHierarchy)
        {
            DesativarVida();
            TransformarBola();
            ResetarVelocidade();
        }
        else
        {
            _PainelGameOver.SetActive(true);
        }
    }

    private void DesativarVida()
    {
        if (Vida3.activeInHierarchy)
        {
            Vida3.SetActive(false);
        }
        else if (Vida2.activeInHierarchy)
        {
            Vida2.SetActive(false);
        }
        else if (Vida1.activeInHierarchy)
        {
            Vida1.SetActive(false);
        }
    }

    private void TransformarBola()
    {
        transform.position = DestinoTeleport.transform.position;
    }

    private void ResetarVelocidade()
    {

        switch (SceneManager.GetActiveScene().name)
        {
            case "Fase 1":
                _ballModel.Speed = 3f;
                break;
            case "Fase 2":
                _ballModel.Speed = 4f;
                break;
            default:
                _ballModel.Speed = 5f;
                break;
        }

        _ballModel.Power = 1f;
    }
}
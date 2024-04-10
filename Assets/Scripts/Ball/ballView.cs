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
    private gameManager _gameManager;
    private Jogador _jogador;
    private RankingManager _RankingManager;

    public brickModel _brickModel;
    public SpriteState _Vida;

    public GameObject _PainelGameOver, PainelVitoria, Vida1, Vida2, Vida3, DestinoTeleport, Brick; 
    public Text PontuacaoTexto, PontuacaoVitoria, pontuacaoGameOver;

    public Button BtVoltarMenu;
    private AudioSource audioSource;
    private bool primeiraColisao = true;

    public int Pontuacao;

    private void Start()
    {
        InitializeComponents();
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

        audioSource = GetComponent<AudioSource>();

        // Verifica se as chaves de PlayerPrefs existem
        if (PlayerPrefs.HasKey("Muted"))
        {
           
            bool muted = PlayerPrefs.GetInt("Muted")==1;
            Debug.Log("Entrou no Muted"+ muted);
            audioSource.mute = muted;
        }
        else
        {
            Debug.Log("Não entrou no Muted");
            audioSource.mute = true;
        }

        if (PlayerPrefs.HasKey("Volume"))
        {
         
            float volume = PlayerPrefs.GetFloat("Volume");
            Debug.Log("Entrou no volume"+ volume);
            audioSource.volume = volume;
        }
        else
        {
            Debug.Log("Não entrou no volume");
            audioSource.volume = 1f;
        }
        audioSource.enabled = false;
    }

    private void InitializeComponents()
    {
        _gameManager = GameObject.FindObjectOfType<gameManager>();
        _RankingManager = GameObject.FindObjectOfType<RankingManager>();
        _jogador = GameObject.FindObjectOfType<Jogador>();
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

        if (primeiraColisao)
        {
            audioSource.enabled = true;
            primeiraColisao = false; // Marque que a primeira colisão já ocorreu
        }
        if (audioSource != null)
        {
            audioSource.Play();
        }

        ResetarVelocidade();

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
                case "Fase 3":
                    SceneManager.LoadScene("Fase 4");
                    break;
                default:
                    Time.timeScale = 0;
                    PainelVitoria.SetActive(true);
                    _RankingManager.AdicionarAoRanking(_jogador.getNomePlayer(), PontuacaoVitoria.text);
                    TransformarBola();
                    _ballController.PausarBola();
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

    public void ReiniciarFase4()
    {
        SceneManager.LoadScene("Fase 4");
    }
    public void VoltarMenu()
    {
        PlayerPrefs.SetInt("RetornouDaFase", 1);
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
            _RankingManager.AdicionarAoRanking(_jogador.getNomePlayer(), pontuacaoGameOver.text);
            TransformarBola();
            _ballController.PausarBola();
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

    public void RetomarBola()
    {
        _ballController.RetomarBola();
    }

    private void ResetarVelocidade()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Fase 1":
                _ballModel.Speed = _ballController.VelicidadeDaBola();
                break;
            case "Fase 2":
                _ballModel.Speed = _ballController.VelicidadeDaBola();
                break;
            case "Fase 3":
                _ballModel.Speed = _ballController.VelicidadeDaBola();
                break;
            default:
                _ballModel.Speed = _ballController.VelicidadeDaBola();
                break;
        }
        _ballModel.Power = 1f;
    }

    public void ReiniciarBolaEJogo()
    {
        transform.position = DestinoTeleport.transform.position;
        ResetarVelocidade();
    }
}
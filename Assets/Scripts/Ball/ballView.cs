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

    private int pontuacaoInicialFase;

    public int Pontuacao;

    private void Start()
    {
        InitializeComponents();
        if (PontuacaoTexto != null)
        {
            // Verifica se é a fase 1, se sim, zera a pontuação
            if (SceneManager.GetActiveScene().name == "Fase 1")
            {
                PlayerPrefs.SetInt("Pontuacao", 0);
            }

            // Atualiza o texto com a pontuação atual do jogador
            PontuacaoTexto.text = PlayerPrefs.GetInt("Pontuacao", 0).ToString();
            // Carrega a pontuação inicial da fase
            pontuacaoInicialFase = PlayerPrefs.GetInt("Pontuacao", 0);

        }
        if (PontuacaoVitoria != null)
        {
            PontuacaoVitoria.text = PlayerPrefs.GetInt("Pontuacao", 0).ToString();
        }
        if (pontuacaoGameOver != null)
        {
            pontuacaoGameOver.text = PlayerPrefs.GetInt("Pontuacao", 0).ToString();
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

        if (_RankingManager == null)
        {
            Debug.LogError("Erro: _RankingManager não foi inicializado corretamente.");
        }

        if (_ballController == null)
        {
            Debug.LogError("Erro: _ballController não foi inicializado corretamente.");
        }
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
        Pontuacao = PlayerPrefs.GetInt("Pontuacao", 0);

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

            Debug.Log("Pontuação está: " + Pontuacao + " e o PlayerPrefs: " + PlayerPrefs.GetInt("Pontuacao", 0));
            PlayerPrefs.SetInt("Pontuacao", Pontuacao);

            // Atualiza os textos com o novo valor da pontuação
            if (PontuacaoTexto != null)
            {
                PontuacaoTexto.text = Pontuacao.ToString();
            }

            if (PontuacaoVitoria != null)
            {
                PontuacaoVitoria.text = Pontuacao.ToString();
            }

            if (pontuacaoGameOver != null)
            {
                pontuacaoGameOver.text = Pontuacao.ToString();
            }
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
                case "Fase 4":
                    SceneManager.LoadScene("Fase 5");
                    break;
                default:
                        PainelVitoria.SetActive(true);
                    if (_RankingManager != null)
                    {
                        _RankingManager.AdicionarAoRanking(_jogador.getNomePlayer(), PontuacaoVitoria.text);
                    }
                    else
                    {
                        Debug.LogError("Erro: _RankingManager  não foi inicializado corretamente.");
                    }                                          
                    TransformarBola();                   
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

    public void ReiniciarFaseAtual()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;

        // Reseta a pontuação para a pontuação inicial da fase
        PlayerPrefs.SetInt("Pontuacao", pontuacaoInicialFase);
        PontuacaoTexto.text = pontuacaoInicialFase.ToString();

        // Recarrega a cena da fase atual
        SceneManager.LoadScene(activeSceneName);
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
            AudioSource gameOverAudioSource = _PainelGameOver.GetComponent<AudioSource>(); // Obtendo o componente AudioSource do GameObject
            if (gameOverAudioSource != null)
            {
                if (PlayerPrefs.HasKey("Muted"))
                {
                    bool muted = PlayerPrefs.GetInt("Muted") == 1;
                    gameOverAudioSource.mute = muted;
                }
                else
                {
                    gameOverAudioSource.mute = true;
                }

                if (PlayerPrefs.HasKey("Volume"))
                {
                    float volume = PlayerPrefs.GetFloat("Volume");
                    gameOverAudioSource.volume = volume;
                }
                else
                {
                    gameOverAudioSource.volume = 1f;
                }
            }

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
            case "Fase 4":
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
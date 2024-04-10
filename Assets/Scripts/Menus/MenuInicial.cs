using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{

    public Text TextoDoInput;
    public InputField TextoInput;
    public string nomeJogador;
    public Button botaoFase1;
    public Button botaoFase2;
    public Button botaoFase3;
    public Button btNovoJogo;
    public Button btConfiguracao;
    public Button btInfo;
    public Button btSair;
    public GameObject panelConfiguracao;

    public Button botaoFacil;
    public Button botaoMedio;
    public Button botaoDificil;

    public Text TextRank1;
    public Text TextRank2;
    public Text TextRank3;
    public Text TextRank4;
    public Text TextRank5;
    public Text TextRank6;

    public float ballSpeed = 3f;

    private RankingManager rankingManager;

    void Start()
    {
        rankingManager = FindObjectOfType<RankingManager>();
        if (rankingManager != null)
        {
            AtualizarRanking();
        }
        else
        {
            Debug.LogError("RankingManager não encontrado na cena.");
        }
        TextoInput.gameObject.SetActive(true);
        btNovoJogo.gameObject.SetActive(true);
        btInfo.gameObject.SetActive(true);
        btConfiguracao.gameObject.SetActive(true);
        btSair.gameObject.SetActive(true);

        botaoFacil.gameObject.SetActive(false);
        botaoMedio.gameObject.SetActive(false);
        botaoDificil.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("BallSpeed"))
        {
            ballSpeed = PlayerPrefs.GetFloat("BallSpeed");
        }
    }
    void AtualizarRanking()
    {
        // Verifica se o RankingManager está configurado corretamente
        if (rankingManager == null)
        {
            Debug.LogError("RankingManager não encontrado.");
            return;
        }
        rankingManager.StartGame();
        // Obter o ranking do RankingManager
        string ranking = rankingManager.LerRanking();

        // Verifica se o ranking foi lido corretamente
        if (string.IsNullOrEmpty(ranking))
        {
            Debug.LogWarning("Ranking vazio ou não encontrado.");
            return;
        }

        // Dividir o ranking em linhas
        string[] linhas = ranking.Split('\n');

        // Preencher os Textos com as linhas do ranking
        TextRank1.text = (linhas.Length > 1) ? linhas[1] : "";
        TextRank2.text = (linhas.Length > 2) ? linhas[2] : "";
        TextRank3.text = (linhas.Length > 3) ? linhas[3] : "";
        TextRank4.text = (linhas.Length > 4) ? linhas[4] : "";
        TextRank5.text = (linhas.Length > 5) ? linhas[5] : "";
        TextRank6.text = (linhas.Length > 6) ? linhas[6] : "";
    }

    
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
            SalvarNome();
            TextoInput.gameObject.SetActive(false);
            botaoFase1.gameObject.SetActive(false);
            botaoFase2.gameObject.SetActive(false);
            botaoFase3.gameObject.SetActive(false);

            btNovoJogo.gameObject.SetActive(false);
            btInfo.gameObject.SetActive(false);
            btConfiguracao.gameObject.SetActive(false);
            btSair.gameObject.SetActive(false);


            botaoFacil.gameObject.SetActive(true);
            botaoMedio.gameObject.SetActive(true);
            botaoDificil.gameObject.SetActive(true);
        }
    }

    public void BotaoFacil_Click()
    {

        setBallSpeed(3f);
        IniciarFase1();
    }

    public void BotaoMedio_Click()
    {
        setBallSpeed(4f);
        IniciarFase1();
    }

    public void BotaoDificil_Click()
    {
        setBallSpeed(6f);
        IniciarFase1();
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
        PlayerPrefs.SetInt("RetornouDaFase", 1);
        SceneManager.LoadScene("Menu");        
    }

    public void IniciarFase1()
    {
        
        SceneManager.LoadScene("Fase 1");
    }

    public void IniciarFase2()
    {
        ballSpeed = 4f;
        SceneManager.LoadScene("Fase 2");
    }

    public void IniciarFase3()
    {
        ballSpeed = 4f;
        SceneManager.LoadScene("Fase 3");
    }

    public void IniciarFase4()
    {
        ballSpeed = 4f;
        SceneManager.LoadScene("Fase 4");
    }

    public void setBallSpeed(float velocidade)
    {
        ballSpeed = velocidade;
        PlayerPrefs.SetFloat("BallSpeed", ballSpeed);

    }

    public void Configuracao()
    {
        panelConfiguracao.gameObject.SetActive(true);
        TextoInput.interactable = false;
        btNovoJogo.interactable = false;
        btInfo.interactable = false;
        btConfiguracao.interactable = false;
        btSair.interactable = false;
    }


    public float getBallSpeed()
    {
        return ballSpeed;
    }
}

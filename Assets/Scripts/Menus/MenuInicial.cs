using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
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
    public Button btSalvarClassificacao;
    public Button btConfiguracao;
    public Button btCreditor;
    public Button BtClassificacao;
    public Button btInfo;
    public Button btSair;
    public GameObject panelConfiguracao;
    public ConfigManager configManager;


    public Button botaoFacil;
    public Button botaoMedio;
    public Button botaoDificil;
    public Text SalvandoDados;

    public Text TextRank1;
    public Text TextRank2;
    public Text TextRank3;
    public Text TextRank4;
    public Text TextRank5;
    public Text TextRank6;

    public float ballSpeed = 3f;

    private RankingManager rankingManager;
    private JSONUploader _JSONuploader;

    void Start()
    {
        if (configManager != null)
        {
            // Aplica as configurações de tela salvas
            bool isFullscreen = configManager.GetFullscreen();
            Screen.fullScreen = isFullscreen;
            int width = configManager.GetResolutionWidth();
            int height = configManager.GetResolutionHeight();
            Screen.SetResolution(width, height, isFullscreen);
        }

        rankingManager = FindObjectOfType<RankingManager>();

        TextoInput.gameObject.SetActive(true);
        btNovoJogo.gameObject.SetActive(true);
        btInfo.gameObject.SetActive(true);
        btSalvarClassificacao.gameObject.SetActive(true);
        btConfiguracao.gameObject.SetActive(true);
        btCreditor.gameObject.SetActive(true);
        BtClassificacao.gameObject.SetActive(true);
        btSair.gameObject.SetActive(true);
        SalvandoDados.gameObject.SetActive(false);

        botaoFacil.gameObject.SetActive(false);
        botaoMedio.gameObject.SetActive(false);
        botaoDificil.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("BallSpeed"))
        {
            ballSpeed = PlayerPrefs.GetFloat("BallSpeed");
        }

        if (rankingManager != null)
        {
            AtualizarRanking();
        }
        else
        {
            Debug.LogError("RankingManager não encontrado na cena.");
        }

    }

    public void AtualizarRanking()
    {
        // Verifica se o RankingManager está configurado corretamente
        if (rankingManager == null)
        {
            Debug.LogError("RankingManager não encontrado.");
            return;
        }

        // Obtém a lista de entradas de ranking do RankingManager
        List<(string, string)> rankingEntries = rankingManager.LerRankingJson();

        // Verifica se o ranking foi lido corretamente
        if (rankingEntries == null || rankingEntries.Count == 0)
        {
            Debug.LogWarning("Ranking vazio ou não encontrado.");
            return;
        }

        // Atualiza os campos de texto com os dados do ranking
        for (int i = 0; i < rankingEntries.Count && i < 6; i++)
        {
            switch (i)
            {
                case 0:
                    TextRank1.text = $"{rankingEntries[i].Item1}= {rankingEntries[i].Item2}";
                    break;
                case 1:
                    TextRank2.text = $"{rankingEntries[i].Item1}= {rankingEntries[i].Item2}";
                    break;
                case 2:
                    TextRank3.text = $"{rankingEntries[i].Item1}= {rankingEntries[i].Item2}";
                    break;
                case 3:
                    TextRank4.text = $"{rankingEntries[i].Item1}= {rankingEntries[i].Item2}";
                    break;
                case 4:
                    TextRank5.text = $"{rankingEntries[i].Item1}= {rankingEntries[i].Item2}";
                    break;
                case 5:
                    TextRank6.text = $"{rankingEntries[i].Item1}= {rankingEntries[i].Item2}";
                    break;
            }
        }
    }

    string FormatRankingEntry((string, string) entry)
    {
        // Formata a entrada do ranking como uma string para exibição
        return string.Format("{0}: {1}", entry.Item1, entry.Item2);
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
            btSalvarClassificacao.gameObject.SetActive(false);
            btConfiguracao.gameObject.SetActive(false);
            btCreditor.gameObject.SetActive(false);
            BtClassificacao.gameObject.SetActive(false);
            btSair.gameObject.SetActive(false);


            botaoFacil.gameObject.SetActive(true);
            botaoMedio.gameObject.SetActive(true);
            botaoDificil.gameObject.SetActive(true);
        }
    }

    public void BotaoFacil_Click()
    {
        PlayerPrefs.SetString("NivelSelecionado", "Facil");
        setBallSpeed(3f);
        IniciarFase1();
    }

    public void BotaoMedio_Click()
    {
        PlayerPrefs.SetString("NivelSelecionado", "Medio");
        setBallSpeed(4f);
        IniciarFase1();
    }

    public void BotaoDificil_Click()
    {
        PlayerPrefs.SetString("NivelSelecionado", "Dificil");
        setBallSpeed(6f);
        IniciarFase1();
    }

    public async void Sair()
    {
        TextoInput.interactable = false;
        btNovoJogo.interactable = false;
        btInfo.interactable = false;
        btSalvarClassificacao.interactable = false;
        btConfiguracao.interactable = false;
        btCreditor.interactable = false;
        BtClassificacao.interactable = false;
        btSair.interactable = false;
        SalvandoDados.gameObject.SetActive(true);
        await Task.Delay(2000);
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

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
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

    public void AbrirClassificaoWeb()
    {
        string url = "http://leilamd.com.br/";

        // Verifica se a URL é válida
        if (!string.IsNullOrEmpty(url))
        {
            // Abre a página web no navegador padrão
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("URL inválida!"); // Avisa no console se a URL for inválida
        }
    }

    public void Configuracao()
    {
        TextoInput.interactable = false;
        btNovoJogo.interactable = false;
        btInfo.interactable = false;
        btSalvarClassificacao.interactable = false;
        btConfiguracao.interactable = false;
        btCreditor.interactable = false;
        BtClassificacao.interactable = false;
        btSair.interactable = false;
        panelConfiguracao.gameObject.SetActive(true);

    }


    public float getBallSpeed()
    {
        return ballSpeed;
    }
    
}

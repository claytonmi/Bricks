using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public string filePath { get; private set; }

    void Start()
    {
        if (!File.Exists(filePath))
        {
            StartGame();
        }

    }

    public void StartGame()
    {
        // Caminho para o arquivo de texto onde o ranking será armazenado (por exemplo, "ranking.txt" na pasta "Assets")
        filePath = Path.Combine(Application.dataPath, "ranking.txt");
        Debug.Log("Caminho do arquivo: " + filePath);

        if (!File.Exists(filePath))
        {
            Debug.Log("Arquivo não encontrado. Tentando criar...");
            try
            {
                File.Create(filePath);
                Debug.Log("Arquivo criado com sucesso.");
            }
            catch (IOException e)
            {
                Debug.LogError("Erro ao criar o arquivo: " + e.Message);
            }
        }
        else
        {
            Debug.Log("Arquivo já existe.");
        }
    }

    // Método para adicionar uma nova entrada ao ranking
    public void AdicionarAoRanking(string nomeJogador, string pontuacao)
    {
        
        // Verifica se o arquivo existe
        if (!File.Exists(filePath))
        {
            Debug.LogError("O arquivo de ranking não existe.");
            return;
        }

        // Lê todas as linhas do arquivo de texto
        List<string> linhas = new List<string>(File.ReadAllLines(filePath));

        // Variável para controlar se o jogador já está presente no ranking
        bool jogadorEncontrado = false;

        // Percorre todas as linhas para verificar se o jogador já está presente
        for (int i = 0; i < linhas.Count; i++)
        {
            // Divide a linha atual pelo caractere '=' para obter o nome do jogador e sua pontuação
            string[] dadosJogador = linhas[i].Split('=');

            // Verifica se o nome do jogador na linha atual corresponde ao nome do jogador fornecido
            if (dadosJogador[0].Trim().Equals(nomeJogador))
            {
                jogadorEncontrado = true;

                // Obtém a pontuação atual do jogador na linha
                string pontuacaoAtual = dadosJogador[1].Trim();

                // Verifica se a nova pontuação é maior que a pontuação atual
                if (string.Compare(pontuacao, pontuacaoAtual) > 0)
                {
                    // Atualiza a pontuação do jogador com a nova pontuação
                    linhas[i] = nomeJogador + "= " + pontuacao;
                    Debug.Log("Pontuação atualizada para o jogador " + nomeJogador + "= " + pontuacao);
                }
                else
                {
                    Debug.Log("A pontuação do jogador " + nomeJogador + " não foi atualizada, pois é menor ou igual à pontuação existente.");
                }

                // Sai do loop, pois o jogador já foi encontrado e processado
                break;
            }
        }

        // Se o jogador não foi encontrado, adiciona uma nova linha para o jogador com sua pontuação
        if (!jogadorEncontrado)
        {
            // Adiciona o jogador ao ranking
            linhas.Add(nomeJogador + "= " + pontuacao);
            Debug.Log("Novo jogador adicionado ao ranking: " + nomeJogador + "= " + pontuacao);
        }

        // Reescreve todo o conteúdo do arquivo com as linhas atualizadas
        File.WriteAllLines(filePath, linhas.ToArray());
    }

    // Método para ler o ranking do arquivo de texto e retornar como uma string formatada
    public string LerRanking()
    {
        try
        {
            Debug.Log("Caminho do arquivo 1: " + filePath);
            // Verifica se o arquivo de texto existe
            if (File.Exists(filePath))
            {
                // Leia todas as linhas do arquivo de texto
                string[] linhas = File.ReadAllLines(filePath);

                // Verifique se o arquivo não está vazio
                if (linhas.Length > 0)
                {
                    // Preparar um array para armazenar o ranking formatado
                    string[] rankingFormatado = new string[6];

                    // Preencher o ranking formatado com as seis primeiras linhas ou deixar em branco se não houver informação suficiente
                    for (int i = 0; i < 6; i++)
                    {
                        rankingFormatado[i] = (i < linhas.Length) ? linhas[i] : "";
                    }

                    // Concatenar as linhas formatadas para formar o ranking final
                    string rankingFinal = "\n";
                    foreach (string linha in rankingFormatado)
                    {
                        rankingFinal += linha + "\n";
                    }

                    // Limpar o conteúdo do arquivo após as 6 primeiras linhas
                    LimparRankingAposSeisPrimeiros();

                    // Retornar o ranking final
                    return rankingFinal;
                }
                else
                {
                    Debug.LogWarning("O arquivo de ranking está vazio.");
                    return "Ranking vazio";
                }
            }
            else
            {
                Debug.LogWarning("O arquivo de ranking não foi encontrado.");
                return "Arquivo de ranking não encontrado";
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Erro ao ler o arquivo de ranking: " + ex.Message);
            return "Erro ao ler o arquivo de ranking";
        }
    }

    private void LimparRankingAposSeisPrimeiros()
    {
        // Leia todas as linhas do arquivo de texto
        string[] linhas = File.ReadAllLines(filePath);

        // Se houver mais de 6 linhas, mantenha apenas as 6 primeiras
        if (linhas.Length > 6)
        {
            // Crie um novo array contendo apenas as 6 primeiras linhas
            string[] seisPrimeirasLinhas = new string[6];
            for (int i = 0; i < 6; i++)
            {
                seisPrimeirasLinhas[i] = linhas[i];
            }

            // Escreva as seis primeiras linhas de volta no arquivo
            File.WriteAllLines(filePath, seisPrimeirasLinhas);
        }
    }
}

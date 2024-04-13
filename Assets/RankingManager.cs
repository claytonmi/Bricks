﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Linq;


public class RankingManager : MonoBehaviour
{
    [Serializable]
    public class Jogador
    {
        public string ID;
        public string nome;
        public int rank;
        public string Data;
        public string Hora;
    }

    private bool isInitialized = false;
    public bool IsInitialized => isInitialized;

    [Serializable]
    public class RankingData
    {
        public List<Jogador> rankingList;
    }

    public string filePath { get; private set; }

    private void Start()
    {
        StartCoroutine(InitializeRankingManager());
    }

    private IEnumerator InitializeRankingPathAsync()
    {
        // Define o caminho para o arquivo JSON onde o ranking será armazenado
        filePath = Path.Combine(Application.dataPath, "ranking.json");
        Debug.Log("Caminho do arquivo: " + filePath);

        // Verifica se o arquivo JSON não existe
        if (!File.Exists(filePath))
        {
            Debug.Log("Arquivo JSON não encontrado. Tentando criar...");
            CriarRankingInicial();
            // Espere até que o arquivo seja criado antes de prosseguir
            yield return new WaitUntil(() => File.Exists(filePath));
        }

        // Defina isInitialized como verdadeiro para indicar que a inicialização foi concluída
        isInitialized = true;
    }

    private IEnumerator InitializeRankingManager()
    {
        // Aguarde até que a inicialização do caminho do arquivo seja concluída
        yield return InitializeRankingPathAsync();

        Debug.Log("Caminho do arquivo dentro do Manager: " + filePath);

        // Verifica se o arquivo JSON não existe
        if (!File.Exists(filePath))
        {
            Debug.Log("Arquivo JSON não encontrado. Tentando criar...");
            CriarRankingInicial();
            // Espere até que o arquivo seja criado antes de prosseguir
            yield return new WaitUntil(() => File.Exists(filePath));
        }
    }

    void CriarRankingInicial()
    {
        RankingData rankingData = new RankingData();
        rankingData.rankingList = new List<Jogador>();
        SalvarRankingJson(rankingData, filePath);
    }

    void SalvarRankingJson(RankingData rankingData, string filePath)
    {
        try
        {
            string json = JsonUtility.ToJson(rankingData);
            File.WriteAllText(filePath, json);
            Debug.Log("Arquivo JSON criado com sucesso em: " + filePath);
        }
        catch (IOException e)
        {
            Debug.LogError("Erro ao criar o arquivo JSON: " + e.Message);
        }
    }

    public void SalvarRanking(RankingData rankingData)
    {
        string json = JsonUtility.ToJson(rankingData);
        File.WriteAllText(filePath, json);
        Debug.Log("Arquivo JSON do ranking salvo em: " + filePath);
    }


    public RankingData LerRanking()
    {
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<RankingData>(json);
        }
        catch (Exception ex)
        {
            Debug.LogError("Erro ao ler o arquivo JSON do ranking: " + ex.Message);
            return null;
        }
    }

    public List<(string, string)> LerRankingJson()
    {
        List<(string, string)> rankingEntries = new List<(string, string)>();

        try
        {
            string json = File.ReadAllText(Path.Combine(Application.dataPath, "ranking.json"));
            RankingData rankingData = JsonUtility.FromJson<RankingData>(json);
            if (rankingData != null && rankingData.rankingList != null)
            {
                foreach (var jogador in rankingData.rankingList)
                {
                    string nome = jogador.nome;
                    string rank = jogador.rank.ToString();
                    rankingEntries.Add((nome, rank));
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Erro ao ler o ranking JSON: " + ex.Message);
        }

        return rankingEntries;
    }

    public void AdicionarAoRanking(string nomeJogador, int novaPontuacao)
    {
        RankingData rankingData = LerRanking();
        if (rankingData != null)
        {
            // Verifica se o jogador já existe no ranking
            Jogador jogadorExistente = rankingData.rankingList.Find(j => j.nome == nomeJogador);
            if (jogadorExistente != null)
            {
                // Se o jogador já existir, verifica se a nova pontuação é maior ou igual à pontuação existente
                if (novaPontuacao > jogadorExistente.rank)
                {
                    // Atualiza a pontuação do jogador existente
                    jogadorExistente.rank = novaPontuacao;
                    jogadorExistente.Data = DateTime.Now.ToString("dd/MM/yyyy");
                    jogadorExistente.Hora = DateTime.Now.ToString("HH:mm");
                }
                else
                {
                    // Se a nova pontuação for menor ou igual, não faz nada
                    return;
                }
            }
            else
            {
                // Se o jogador não existir, adiciona um novo jogador ao ranking
                Jogador novoJogador = new Jogador
                {
                    ID = nomeJogador + DateTime.Now.ToString("ddMMyyHHmm"),
                    nome = nomeJogador,
                    rank = novaPontuacao,
                    Data = DateTime.Now.ToString("dd/MM/yyyy"),
                    Hora = DateTime.Now.ToString("HH:mm")
                };

                rankingData.rankingList.Add(novoJogador);
            }

            // Ordena a lista de jogadores por pontuação, data e hora
            rankingData.rankingList.Sort((a, b) =>
            {
                // Compara as pontuações
                int comparePontuacao = b.rank.CompareTo(a.rank);
                if (comparePontuacao != 0)
                {
                    return comparePontuacao;
                }
                else
                {
                    // Compara as datas e horas
                    DateTime dataHoraA = DateTime.ParseExact(a.Data + a.Hora, "dd/MM/yyyyHH:mm", null);
                    DateTime dataHoraB = DateTime.ParseExact(b.Data + b.Hora, "dd/MM/yyyyHH:mm", null);
                    return dataHoraB.CompareTo(dataHoraA);
                }
            });

            // Mantém apenas os top 6 jogadores
            if (rankingData.rankingList.Count > 6)
            {
                rankingData.rankingList = rankingData.rankingList.GetRange(0, 6);
            }

            // Salva o ranking atualizado
            SalvarRanking(rankingData);
        }
    }
}


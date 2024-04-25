using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public ballView _ballView;
    public bool isPaused = false;
    public GameObject canvaBt, canvaInicial;
    public Text countdownText;
    float startTime;
    float countdownDuration = 3f;
    private bool countingDown = false;

    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        canvaBt.SetActive(false);
        _ballView = GameObject.FindObjectOfType<ballView>();
        if (_ballView != null)
        {
            inicioFase();
        }
        else
        {
            Debug.LogError("Objeto ballView não encontrado.");
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Creditos")
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                TogglePause();
            }
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void TogglePause()
    {
        canvaBt.SetActive(!canvaBt.activeSelf);

        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0; // Pausa o tempo do jogo
        }
        else
        {
            Time.timeScale = 1; // Retoma o tempo normal do jogo
        }
    }

    public void PauseGame()
    {
        canvaBt.SetActive(true);
        Time.timeScale = 0;
    }

    public void inicioFase()
    {
        canvaInicial.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countingDown = true;
        while (Time.realtimeSinceStartup - startTime < countdownDuration)
        {
            float timeLeft = countdownDuration - (Time.realtimeSinceStartup - startTime);
            countdownText.text = Mathf.CeilToInt(timeLeft).ToString();
            yield return null;
        }
        countdownText.text = "Go!";
        countdownText.gameObject.SetActive(false);
    
        countingDown = false;

        // Inicia o jogo após o término do contador regressivo
        StartGameAfterCountdown();
    }

    void StartGameAfterCountdown()
    {
        canvaInicial.SetActive(false); // Desativa o canvas inicial
        Time.timeScale = 1; // Retoma o tempo do jogo
    }

    public bool IsCountingDown()
    {
        return countingDown;
    }

}

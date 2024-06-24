using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerView : MonoBehaviour
{
    // Start is called before the first frame update
	private playerController _playerController;
    private ConfigManager _configManager;
    private float sensitivity;

    void Start()
    {
        // Obtém o componente playerController anexado ao mesmo GameObject
        _playerController = GetComponent<playerController>();
        _configManager = FindObjectOfType<ConfigManager>();
        sensitivity = _configManager.GetSensitivity();
    }

    // Update is called once per frame
    void Update()
    {
        sensitivity = _configManager.GetSensitivity();
        float h = 0f;

        // Verifica se há entrada do teclado
        if (Input.GetAxis("Horizontal") != 0)
        {
            h = Input.GetAxis("Horizontal") * sensitivity;
        }
        // Verifica se há toques na tela
        if (Input.touchCount > 0)
        {
            // Obter o primeiro toque na tela
            Touch touch = Input.GetTouch(0);

            // Calcular a posição do toque em relação à largura da tela
            float touchPositionX = touch.position.x / Screen.width;

            // Determinar a direção do movimento com base na posição do toque
            if (touchPositionX < 0.5f)
            {
                h = -1f; // Mover para a esquerda
            }
            else
            {
                h = 1f; // Mover para a direita
            }
        }

        // Verifica se há entrada do controle de console
        if (Input.GetAxis("JoystickHorizontal") != 0)
        {
            h = Input.GetAxis("JoystickHorizontal") * sensitivity;
        }


        _playerController.Move(h);		
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerView : MonoBehaviour
{
    // Start is called before the first frame update
	private playerController _playerController;
	
    void Start()
    {
        _playerController = GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (Input.touchCount > 0)
        {
            // Obter o primeiro toque na tela
            Touch touch = Input.GetTouch(0);

            // Calcular a posição do toque em relação à largura da tela
            float touchPositionX = touch.position.x / Screen.width;

            // Determinar a direção do movimento com base na posição do toque
            h += (touchPositionX < 0.5f) ? -1f : 1f;
        }
        _playerController.Move(h);		
    }
}

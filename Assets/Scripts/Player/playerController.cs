using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
	private playerModel _playerModel;
	private Transform _playerTransform;
    private BoxCollider2D _boxCollider2D;
    private playerView _playerView;
    private SpriteRenderer _playerSpriteRenderer;
    private bool jogoPausado = false;

    void Start()
    {
        _playerModel = GetComponent<playerModel>();
		_playerTransform = GetComponent<Transform>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _playerView = GetComponent<playerView>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();

        SetPlayerColor();
    }

    // Update is called once per frame
    public void Move(float h)
    {
		if ((_playerTransform.position.x >= -1.43f && h < 0f) ||
		   (_playerTransform.position.x <= 1.43f && h > 0f))
		{
			_playerTransform.Translate(_playerModel.Speed * h,0f,0f);
		}
    }

    public void OnClick()
    {
        if (jogoPausado)
        {
            RetomarPlayer();      
        }
        else
        {
            PausarPlayer();
        }
        jogoPausado = !jogoPausado;
    
    }

    public void PausarPlayer()
    {
        _boxCollider2D.enabled = false;
        _playerView.enabled = false;
    }

    public void RetomarPlayer()
    {
        _boxCollider2D.enabled = true;
        _playerView.enabled = true;
    }

    public void SetPlayerColor(Color color)
    {
        _playerSpriteRenderer.color = color; // Define a cor do jogador usando o componente SpriteRenderer
    }

    public void SetPlayerColor()
    {
        // Verifique se há uma cor do jogador salva no PlayerPrefs
        if (PlayerPrefs.HasKey("PlayerColorR") && PlayerPrefs.HasKey("PlayerColorG") && PlayerPrefs.HasKey("PlayerColorB"))
        {
            // Carregar a cor salva do PlayerPrefs
            float r = PlayerPrefs.GetFloat("PlayerColorR");
            float g = PlayerPrefs.GetFloat("PlayerColorG");
            float b = PlayerPrefs.GetFloat("PlayerColorB");
            Color playerColor = new Color(r, g, b);

            // Aplicar a cor ao jogador
            SetPlayerColor(playerColor);
        }
    }

}

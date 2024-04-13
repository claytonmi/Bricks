using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
	private playerModel _playerModel;
	private Transform _playerTransform;
    private BoxCollider2D _boxCollider2D;
    private playerView _playerView;
    private bool jogoPausado = false;

    void Start()
    {
        _playerModel = GetComponent<playerModel>();
		_playerTransform = GetComponent<Transform>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _playerView = GetComponent<playerView>();

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

}

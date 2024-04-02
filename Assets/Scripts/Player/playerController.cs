using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
	private playerModel _playerModel;
	private Transform _playerTransform;
	
    void Start()
    {
        _playerModel = GetComponent<playerModel>();
		_playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    public void Move(float h)
    {
		if ((_playerTransform.position.x >= -1.45f && h < 0f) ||
		   (_playerTransform.position.x <= 1.45f && h > 0f))
		{
			_playerTransform.Translate(_playerModel.Speed * h,0f,0f);
		}
    }
}

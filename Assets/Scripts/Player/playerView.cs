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
		_playerController.Move(h);		
    }
}

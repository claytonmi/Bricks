using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickView : MonoBehaviour
{
    // Start is called before the first frame update
	private brickController _brickController;
	
    void Start()
    {
        _brickController = GetComponent<brickController>();
		
    }
	
	public void PerformTakeDamage(float damage, Collision2D collision)
	{
		_brickController.TakeDamage(damage,collision);
	}

	

}

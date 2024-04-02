using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class brickController : MonoBehaviour
{
    // Start is called before the first frame update
	private brickModel _brickModel;	
	private ballView scriptBallView;


	void Start()
    {
        _brickModel = GetComponent<brickModel>();
		scriptBallView = GameObject.FindObjectOfType(typeof(ballView)) as ballView;

	}

    public void TakeDamage(float damage, Collision2D collision)
	{
		_brickModel.Health -= damage;
		if (_brickModel.Health <= 0)
		{
			if (collision.gameObject.tag == "Enemy")
			{
				scriptBallView.atualizaPontuacao(collision);
				Destroy(gameObject);
			}
			else if (collision.gameObject.tag == "Enemy2")
			{
				scriptBallView.atualizaPontuacao(collision);
				Destroy(gameObject);
			}
			else if (collision.gameObject.tag == "Enemy3")
			{
				scriptBallView.atualizaPontuacao(collision);
				Destroy(gameObject);
			}
			else if (collision.gameObject.tag == "Enemy4")
			{
				scriptBallView.atualizaPontuacao(collision);
				Destroy(gameObject);
			}

		}
	}

	


	
}

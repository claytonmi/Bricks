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
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _brickModel = GetComponent<brickModel>();
        scriptBallView = FindObjectOfType<ballView>();
    }

    public void TakeDamage(float damage, Collision2D collision)
    {
        _brickModel.Health -= damage;
        if (_brickModel.Health <= 0)
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                case "Enemy2":
                case "Enemy3":
                case "Enemy4":
                    scriptBallView.atualizaPontuacao(collision);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
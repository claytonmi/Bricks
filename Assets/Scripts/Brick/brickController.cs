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
        if (_brickModel == null)
        {
            Debug.LogError("brickModel não foi encontrado em " + gameObject.name);
        }

        scriptBallView = FindObjectOfType<ballView>();
        if (scriptBallView == null)
        {
            Debug.LogError("ballView não foi encontrada em " + gameObject.name);
        }
    }

    public void TakeDamage(float damage, Collision2D collision)
    {
        if (_brickModel != null)
        {
            _brickModel.Health -= damage;
            if (_brickModel.Health <= 0)
            {
                if (collision != null && collision.gameObject != null)
                {
                    switch (collision.gameObject.tag)
                    {
                        case "Enemy":
                        case "Enemy2":
                        case "Enemy3":
                        case "Enemy4":
                            if (scriptBallView != null)
                            {
                                scriptBallView.atualizaPontuacao(collision);
                            }
                            else
                            {
                                Debug.LogWarning("scriptBallView é null em " + gameObject.name);
                            }
                            break;
                    }
                }
                else
                {
                    Debug.LogWarning("collision ou collision.gameObject é null em " + gameObject.name);
                }

                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogWarning("_brickModel é null em " + gameObject.name);
        }
    }
}
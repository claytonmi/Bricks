using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ballController : MonoBehaviour
{

    private ballModel _ballModel;
    private Rigidbody2D _rigidbody2D;
    private Vector2 direcaoAtualBola; // Variável para armazenar a direção atual da bola
    public float _velocidade;


    void Start()
    {
        _ballModel = GetComponent<ballModel>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        if (PlayerPrefs.HasKey("BallSpeed"))
        {
            _velocidade = PlayerPrefs.GetFloat("BallSpeed");
        }
        else
        {
            _velocidade = 3f; // Defina um valor padrão caso a velocidade da bola não esteja definida no PlayerPrefs
        }
        _ballModel.Speed = _velocidade;
        if (_rigidbody2D != null)
        {
            _rigidbody2D.velocity = (_ballModel.Direction * _ballModel.Speed);
            direcaoAtualBola = _rigidbody2D.velocity.normalized;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D não encontrado no GameObject 'Ball'. Certifique-se de que está anexado corretamente.");
        }
    }

    public void PerfectAngleReflect(Collision2D collision)
    {
        _ballModel.Direction = Vector2.Reflect(_ballModel.Direction, collision.contacts[0].normal);
        _rigidbody2D.velocity = _ballModel.Direction * _ballModel.Speed;

    }

    public Vector2 CalcBallAngleReflect(Collision2D playerCol)
    {
        float playerPixels = 120f;

        float unityScaleHalfPlayerPexels = playerPixels / 2f / 100;

        float scaleFactorToPutIn1do18Rage = 1.5f;


        float angleDegUnitScale = (playerCol.transform.position.x - transform.position.x + unityScaleHalfPlayerPexels) * scaleFactorToPutIn1do18Rage * 100f;
        float angleRad = angleDegUnitScale * Mathf.PI / 180f;

        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void AngleChange(Vector2 direcao)
    {
        _ballModel.Direction = direcao;

        _rigidbody2D.velocity = _ballModel.Direction * _ballModel.Speed;
    }

    public void PausarBola()
    {

        if (_rigidbody2D != null)
        {
            direcaoAtualBola = _rigidbody2D.velocity.normalized;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.isKinematic = true;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D não encontrado no GameObject 'Ball'. Não é possível pausar a bola.");
        }

    }

    public void RetomarBola()
    {
        if (_rigidbody2D != null)
        {
            _rigidbody2D.isKinematic = false;
            _rigidbody2D.velocity = (direcaoAtualBola * VelicidadeDaBola());
        }
        else
        {
            Debug.LogWarning("Rigidbody2D não encontrado no GameObject 'Ball'. Não é possível retomar a bola.");
        }
    }

    public float VelicidadeDaBola()
    {
        return _velocidade; 
    }

    public void ReiniciarFisicaBola()
    {
        if (_rigidbody2D != null)
        {
            _rigidbody2D.velocity = (_ballModel.Direction * _ballModel.Speed); // Reseta a velocidade linear
            _rigidbody2D.angularVelocity = 0f; // Reseta a velocidade angular
            _rigidbody2D.gravityScale = 0f; // Reseta a gravidade (se aplicável)
        }
    }
}
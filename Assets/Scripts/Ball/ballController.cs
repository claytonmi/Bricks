using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
  
    private ballModel _ballModel;
	private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _ballModel = GetComponent<ballModel>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = (_ballModel.Direction * _ballModel.Speed);

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

        float angleDegUnitScale = (playerCol.transform.position.x - transform.position.x + unityScaleHalfPlayerPexels) * scaleFactorToPutIn1do18Rage;

        float angleDeg = angleDegUnitScale * 100f;

        float angleRad = angleDeg * Mathf.PI / 180f;

        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void AngleChange(Vector2 direcao)
    {
        _ballModel.Direction = direcao;

        _rigidbody2D.velocity = _ballModel.Direction * _ballModel.Speed;
    }
}

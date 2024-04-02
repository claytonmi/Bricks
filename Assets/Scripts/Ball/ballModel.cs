using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballModel : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] private Vector2 _direction;
	[SerializeField] private float _power;
	[SerializeField] private float _speed;

	public Vector2 Direction { get => _direction; set => _direction = value; }
	public float Power { get => _power; set => _power = value; }
	public float Speed { get => _speed; set => _speed = value; }
}
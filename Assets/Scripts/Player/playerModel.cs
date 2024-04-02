using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerModel : MonoBehaviour
{
    // Start is called before the first frame update
	[SerializeField] private float _speed;
	[SerializeField] private float _life;
	[SerializeField] private float _scale;
	
	public float Speed { get => _speed; set => _speed = value; }
	public float Life { get => _life; set => _life = value; }
	public float Scale { get => _scale; set => _scale = value; }
}

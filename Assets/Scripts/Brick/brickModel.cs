using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickModel : MonoBehaviour
{
    // Start is called before the first frame update
	
	[SerializeField] private float _health;
	
	public float Health { get => _health; set => _health = value; }
}

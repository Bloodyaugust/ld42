using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceTowardPlayer : MonoBehaviour {

	public float MoveForce;

	GameObject _player;
	Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindWithTag("Player");
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		_rigidbody.AddForce((_player.transform.position - transform.position).normalized * MoveForce);
	}
}

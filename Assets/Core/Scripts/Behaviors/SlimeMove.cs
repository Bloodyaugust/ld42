using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : MonoBehaviour {

	public float BobAmount;
	public float MoveForce;
	public float MoveForceScalar;

	float _randomizer;
	float _startY;
	GameObject _player;
	Rigidbody2D _rigidbody;
	Toolbox _toolbox;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindWithTag("Player");
		_randomizer = Random.value * 8;
		_rigidbody = GetComponent<Rigidbody2D>();
		_startY = transform.localScale.y;
		_toolbox = Toolbox.Instance;

		MoveForce = MoveForce + (MoveForceScalar * _toolbox.CurrentWave);
	}

	// Update is called once per frame
	void Update () {
		float currentY = _startY + (Mathf.Sin(Time.time * 4 + _randomizer) / 2f) * BobAmount;
		transform.localScale = new Vector3(transform.localScale.x, currentY, transform.localScale.z);

		float force = (Mathf.Sin(Time.time * 4 + _randomizer) / 2f) * MoveForce + MoveForce / 2;
		if (_player != null) {
			_rigidbody.AddForce((_player.transform.position - transform.position).normalized * force);
		}
	}
}

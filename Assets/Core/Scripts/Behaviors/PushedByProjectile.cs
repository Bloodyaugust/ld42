using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedByProjectile : MonoBehaviour {

	public float PushForce;
	public float PushForceScalar;

	Rigidbody2D _rigidbody;
	Toolbox _toolbox;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody2D>();
		_toolbox = Toolbox.Instance;

		PushForce = PushForce + (PushForceScalar * _toolbox.CurrentWave);
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "PlayerProjectile") {
			_rigidbody.AddForce((collision.transform.position - transform.position).normalized * -PushForce, ForceMode2D.Impulse);
		}
	}
}

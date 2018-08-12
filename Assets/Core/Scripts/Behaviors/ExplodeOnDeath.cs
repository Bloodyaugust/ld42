using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnDeath : MonoBehaviour {

	public float ExplosionForce;
	public float ExplosionForceScalar;
	public float ExplosionRadius;

	Toolbox _toolbox;

	// Use this for initialization
	void Start () {
		_toolbox = Toolbox.Instance;

		ExplosionForce = ExplosionForce + (ExplosionForceScalar * _toolbox.CurrentWave);
	}

	void Die () {
		Collider2D[] affected = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

		for (int i = 0; i < affected.Length; i++) {
			Rigidbody2D rigidbody = affected[i].gameObject.GetComponent<Rigidbody2D>();

			if (rigidbody != null) {
				rigidbody.AddForce((affected[i].gameObject.transform.position - transform.position).normalized * ExplosionForce);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			Die();
			SendMessage("Die", null, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}

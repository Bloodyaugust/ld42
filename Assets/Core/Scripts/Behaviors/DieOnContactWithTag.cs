using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnContactWithTag : MonoBehaviour {

	public string DeathTag;

	bool _isDying;

	void LateUpdate () {
		if (_isDying) {
			SendMessage("Die", null, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == DeathTag) {
			_isDying = true;
		}
	}
}

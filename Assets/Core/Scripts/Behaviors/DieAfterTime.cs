using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour {

	public float Lifetime;

	bool _isDying;
	float _timeToDeath;

	void LateUpdate () {
		if (_isDying) {
			SendMessage("Die", null, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		_timeToDeath = Lifetime;
	}

	// Update is called once per frame
	void Update () {
		_timeToDeath -= Time.deltaTime;

		if (_timeToDeath <= 0) {
			_isDying = true;
		}
	}
}

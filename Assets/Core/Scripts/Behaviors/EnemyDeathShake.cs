using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathShake : MonoBehaviour {

	Toolbox _toolbox;

	// Use this for initialization
	void Start () {
		_toolbox = Toolbox.Instance;
	}

	void Die () {
		_toolbox.EnemyDied.Invoke();
	}
}

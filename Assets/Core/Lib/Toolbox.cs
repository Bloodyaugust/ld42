using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;

public class Toolbox : Singleton<Toolbox> {
	protected Toolbox () {}

	public AudioClip EnemyDiedAudioClip;

	public UnityEvent EnemyDied = new UnityEvent();

	AudioSource _audioSource;

	void Awake () {
		_audioSource = GetComponent<AudioSource>();

		EnemyDied.AddListener(OnEnemyDeath);
	}

	void OnEnemyDeath () {
		ProCamera2DShake.Instance.Shake("EnemyDeathShake");
		_audioSource.pitch = 1 + UnityEngine.Random.Range(-0.1f, 0.5f);
		_audioSource.PlayOneShot(EnemyDiedAudioClip);
	}

	static public T RegisterComponent<T> () where T: Component {
		return Instance.GetOrAddComponent<T>();
	}
}

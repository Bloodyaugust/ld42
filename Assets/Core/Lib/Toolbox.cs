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
	public AudioClip[] PlayerDamageAudioClips;
	public GameObject WaveCompletedText;

	public UnityEvent EnemyDied = new UnityEvent();
	public UnityEvent EnableWaves = new UnityEvent();
	public UnityEvent DisableWaves = new UnityEvent();
	public UnityEvent GameLoaded = new UnityEvent();
	public UnityEvent WaveCompleted = new UnityEvent();

	float _cameraScreenSize;
	AudioSource _audioSource;
	DamageFromContactWithTag _playerDamage;
	GameObject _player;
	ProCamera2DSpeedBasedZoom _cameraZoom;
	WaveController _waveController;

	void Awake () {
		_audioSource = GetComponent<AudioSource>();
		_cameraScreenSize = Camera.main.orthographicSize;
		_cameraZoom = GetComponent<ProCamera2DSpeedBasedZoom>();
		_waveController = GetComponent<WaveController>();

		EnemyDied.AddListener(OnEnemyDeath);
		WaveCompleted.AddListener(OnWaveCompleted);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnEnemyDeath () {
		ProCamera2DShake.Instance.Shake("EnemyDeathShake");
		_audioSource.pitch = 1 + UnityEngine.Random.Range(-0.1f, 0.5f);
		_audioSource.volume = 0.5f;
		_audioSource.PlayOneShot(EnemyDiedAudioClip);
	}

	void OnPlayerDamageHalfTaken () {
		_cameraZoom.enabled = false;
		ProCamera2D.Instance.UpdateScreenSize(3f, 0.5f);
	}

	void OnPlayerDamageReset () {
		_cameraZoom.enabled = true;
		ProCamera2D.Instance.UpdateScreenSize(_cameraScreenSize, 0.5f);
	}

	void OnPlayerDamageTaken () {
		_audioSource.pitch = 1 + UnityEngine.Random.Range(-0.1f, 0.1f);
		_audioSource.volume = 1f;
		_audioSource.PlayOneShot(PlayerDamageAudioClips[UnityEngine.Random.Range(0, PlayerDamageAudioClips.Length)]);
		ProCamera2DShake.Instance.Shake("PlayerDamageShake");
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		if (scene.name == "game") {
			GameLoaded.Invoke();

			_player = GameObject.FindWithTag("Player");
			_playerDamage = _player.GetComponent<DamageFromContactWithTag>();

			_playerDamage.DamageTaken.AddListener(OnPlayerDamageTaken);
			_playerDamage.DamageIntervalHalf.AddListener(OnPlayerDamageHalfTaken);
			_playerDamage.DamageReset.AddListener(OnPlayerDamageReset);

			ProCamera2D.Instance.AddCameraTarget(_player.transform);
		}
	}

	void OnWaveCompleted () {
		Instantiate(WaveCompletedText, new Vector3(0, 0, WaveCompletedText.transform.position.z), Quaternion.identity);

		StartCoroutine(StartNextWave());
	}

	static public T RegisterComponent<T> () where T: Component {
		return Instance.GetOrAddComponent<T>();
	}

	IEnumerator StartNextWave () {
		Debug.Log("Starting next wave in " + _waveController.TimeBetweenWaves);
		yield return new WaitForSeconds(_waveController.TimeBetweenWaves);
		_waveController.EnableWaves();
	}
}

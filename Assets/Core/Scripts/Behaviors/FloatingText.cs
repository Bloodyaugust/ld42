using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {

	public float FloatHeight;
	public float Lifetime;

	float _endY;
	float _timeToLive;
	float _startY;
	TextMesh _textMesh;

	// Use this for initialization
	void Start () {
		_endY = transform.position.y + FloatHeight;
		_startY = transform.position.y;
		_textMesh = GetComponent<TextMesh>();
		_timeToLive = Lifetime;
	}

	// Update is called once per frame
	void Update () {
		_timeToLive -= Time.deltaTime;

		_textMesh.color = new Color(_textMesh.color.r, _textMesh.color.g, _textMesh.color.b, _timeToLive / Lifetime);
		transform.position = new Vector3(transform.position.x, Mathf.Lerp(_startY, _endY, _timeToLive / Lifetime), transform.position.z);

		if (_timeToLive <= 0) {
			Destroy(gameObject);
		}
	}
}

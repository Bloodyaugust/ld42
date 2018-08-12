using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourWaySprite : MonoBehaviour {

	public enum Directions {
		Right,
		Left,
		Up,
		Down
	}

	public bool MotionControlled;
	public Sprite[] Sprites;

	Directions _currentDirection;
	Rigidbody2D _rigidbody2D;
	SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();

		SetDirection(Directions.Right);
	}

	// Update is called once per frame
	void Update () {
		if (MotionControlled) {
			if (Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Abs(_rigidbody2D.velocity.y)) {
				if (_rigidbody2D.velocity.x >= 0 && _currentDirection != Directions.Right) {
					SetDirection(Directions.Right);
				}
				if (_rigidbody2D.velocity.x < 0 && _currentDirection != Directions.Left) {
					SetDirection(Directions.Left);
				}
			} else {
				if (_rigidbody2D.velocity.y >= 0 && _currentDirection != Directions.Up) {
					SetDirection(Directions.Up);
				}
				if (_rigidbody2D.velocity.y < 0 && _currentDirection != Directions.Down) {
					SetDirection(Directions.Down);
				}
			}
		}
	}

	public void SetDirection (Directions direction) {
		_currentDirection = direction;

		switch (direction) {
			case Directions.Left:
				_spriteRenderer.sprite = Sprites[1];
				break;
			case Directions.Up:
				_spriteRenderer.sprite = Sprites[2];
				break;
			case Directions.Down:
				_spriteRenderer.sprite = Sprites[3];
				break;
			default:
				_spriteRenderer.sprite = Sprites[0];
				break;
		}
	}
}

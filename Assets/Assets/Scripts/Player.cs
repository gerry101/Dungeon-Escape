using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D _playerRigidbody;
	private PlayerAnimation _playerAnimation;
	private SpriteRenderer _playerSpriteRenderer;
	private bool _isPlayerflipped = false;
	private float _jumpForce = 6.0f;
	[SerializeField]
	private float _playerSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		_playerRigidbody = GetComponent<Rigidbody2D>();
		_playerAnimation = GetComponent<PlayerAnimation>();
		_playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
		GroundAttack();
	}

	// Handle movement of the player
	void Movement() {
		// Handle vertical movement of player
		float move = Input.GetAxisRaw("Horizontal");
		_playerRigidbody.velocity = new Vector2(move * _playerSpeed, _playerRigidbody.velocity.y);
		if(move == 1) {
			_isPlayerflipped = false;
		} else if(move == -1) {
			_isPlayerflipped = true;
		}
		_playerSpriteRenderer.flipX = _isPlayerflipped;
		_playerAnimation.Run(move);

		// Draw raycast below player
		Debug.DrawRay(transform.position, Vector2.down * 1.0f, Color.blue);

		// Handle jumping of player
		_playerAnimation.Jump(false);
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1)) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 8);
			if(hit.collider != null) {
				_playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _jumpForce);
				if(Input.GetKeyDown(KeyCode.Space)) {
					_playerAnimation.Jump(true);
				} else if(Input.GetKeyDown(KeyCode.Mouse1)) {
					_playerAnimation.JumpAttack();
				}
			}
		}
	}

	// Handle player attack functionality
	void GroundAttack() {
		if(Input.GetKeyDown(KeyCode.Mouse0)) {
			_playerAnimation.GroundAttack();
		}
	}
}

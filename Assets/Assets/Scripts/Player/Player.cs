using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable {

	private Rigidbody2D _playerRigidbody;
	private PlayerAnimation _playerAnimation;
	private SpriteRenderer _playerSpriteRenderer;
	private bool _isPlayerflipped = false;
	private float _jumpForce = 6.5f;
	[SerializeField]
	private float _playerSpeed = 3.0f;
	[SerializeField]
	private int _health;
	private bool _canDamage;

	public int Health{get; set;}

	// Use this for initialization
	void Start () {
		_playerRigidbody = GetComponent<Rigidbody2D>();
		_playerAnimation = GetComponent<PlayerAnimation>();
		_playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
		Health = _health;
		_canDamage = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!_playerAnimation.IsPlayerDead()) {
			Movement();
		}
	}

	// Handle movement of the player
	void Movement() {
		// Handle horizontal movement of player
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
			if(CheckPlayerGrounded()) {
				_playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _jumpForce);
				if(Input.GetKeyDown(KeyCode.Space)) {
					_playerAnimation.Jump(true);
				} else if(Input.GetKeyDown(KeyCode.Mouse1)) {
					_playerAnimation.JumpAttack();
				}
			}
		}

		// Handle player attack functionality
		if(Input.GetKeyDown(KeyCode.Mouse0)) {
			_playerAnimation.GroundAttack();
		}
	}

	// Check if player is grounded
	bool CheckPlayerGrounded() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 8);
		if(hit.collider != null) {
			return true;
		}

		return false;
	}

	// Damage the player
	public void Damage() {
		if(_health < 1) {
			return;
		}

		if(_canDamage) {
			_health -= 1;
			Health = _health;
			_canDamage = false;
			StartCoroutine(WaitForAttack());

			_playerAnimation.PlayerHit();
		}		

		if(_health < 1) {
			_playerAnimation.PlayerDeath();
		}
	}

	// Prevent multiple attacks in one frame
	IEnumerator WaitForAttack() {
		yield return new WaitForSeconds(0.5f);
		_canDamage = true;
	}
}
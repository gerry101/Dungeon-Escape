using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D _playerRigidbody;
	private float _jumpForce = 5.0f;
	[SerializeField]
	private float _playerSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		_playerRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
	}

	// Handle movement of the player
	void Movement() {
		// Handle vertical movement of player
		float move = Input.GetAxisRaw("Horizontal");
		_playerRigidbody.velocity = new Vector2(move * _playerSpeed, _playerRigidbody.velocity.y);

		// Draw raycast below player
		Debug.DrawRay(transform.position, Vector2.down * 1.0f, Color.blue);

		// Handle jumping of player
		if(Input.GetKeyDown(KeyCode.Space)) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 8);
			if(hit.collider != null) {
				_playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _jumpForce);
			}
		}
	}
}

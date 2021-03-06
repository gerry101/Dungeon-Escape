﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	private Animator _playerAnimator;

	// Use this for initialization
	void Start () {
		_playerAnimator = GetComponentInChildren<Animator>();
	}

	// Handle the run animation
	public void Run(float move) {
		_playerAnimator.SetFloat("Move", Mathf.Abs(move));
	}

	// Handle the jumping animation
	public void Jump(bool jump) {
		_playerAnimator.SetBool("Jump", jump);
	}

	// Handle the ground attack animation
	public void GroundAttack() {
		_playerAnimator.SetTrigger("Attack");
	}

	// Handle the jump attack animation
	public void JumpAttack() {
		_playerAnimator.SetTrigger("JumpAttack");
	}

	// Handle the hit animation
	public void PlayerHit() {
		_playerAnimator.SetTrigger("Hit");
	}

	// Handle the death animation
	public void PlayerDeath() {
		_playerAnimator.SetTrigger("Death");
	}

	// Check if player is dead
	public bool IsPlayerDead() {
		if(_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death")) {
			return true;
		}

		return false;
	}
}

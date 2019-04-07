using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	protected Transform waypoint_Left, waypoint_Right;
	[SerializeField]
	protected int health;
	[SerializeField]
	protected float speed;
	[SerializeField]
	protected int gems;
	[SerializeField]
	protected bool facingRight;
	protected Animator enemyAnimator;
	protected SpriteRenderer enemySpriteRenderer;

	
	void Start() {
		Init();
	}
	
	public virtual void Init() {
		facingRight = true;
		enemyAnimator = GetComponentInChildren<Animator>();
		enemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	public virtual void Update() {
		Movement();
	}

	// Handle movement of the enemy
	void Movement() {
		if(!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			if(facingRight) {
				if(transform.position == waypoint_Right.position) {
					enemySpriteRenderer.flipX = true;
					facingRight = false;
					return;
				}
				transform.position = Vector3.MoveTowards(transform.position, waypoint_Right.position, speed * Time.deltaTime);
				if(transform.position == waypoint_Right.position) {
					enemyAnimator.SetTrigger("Idle");
				}
			} else {
				if(transform.position == waypoint_Left.position) {
					enemySpriteRenderer.flipX = false;
					facingRight = true;
					return;
				}
				transform.position = Vector3.MoveTowards(transform.position, waypoint_Left.position, speed * Time.deltaTime);
				if(transform.position == waypoint_Left.position) {
					enemyAnimator.SetTrigger("Idle");
				}
			}
		} 
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable {
	

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
	[SerializeField]
	protected Transform playerTransform;
	private bool _canDamage;
	protected bool isAttackSelected;
	protected bool isAttacking;
	protected Animator enemyAnimator;
	protected SpriteRenderer enemySpriteRenderer;

	public int Health{get; set;}
	
	void Start() {
		Init();
		Health = health;
		_canDamage = true;
		isAttackSelected = false;
		isAttacking = false;
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
		if(enemyAnimator && (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
		 enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Bridge"))) {
			if(Vector2.Distance(gameObject.transform.localPosition, playerTransform.localPosition) <= 5.0f) {
				if(gameObject.transform.position.x > playerTransform.position.x) {
					enemySpriteRenderer.flipX = true;
					facingRight = false;
				} else {
					enemySpriteRenderer.flipX = false;
					facingRight = true;
				}

				if(!isAttackSelected) {
					if(Random.value > 0.6f) {
						isAttacking = true;
					} else {
						isAttacking = false;
					}

					isAttackSelected = true;
					StartCoroutine(WaitAndAttack());
				} else {
					if(isAttacking) {
						if(enemyAnimator && !enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) {
							StartCoroutine(HitCoolDown());
						}
					} else {
						Vector3 targetPlayerPosition = playerTransform.position;
						if(facingRight) {
							targetPlayerPosition.x -= 1.0f;
						} else {
							targetPlayerPosition.x += 1.0f;
						}

						if(targetPlayerPosition.x == transform.position.x &&
						 enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
							enemyAnimator.SetTrigger("Bridge");
						} else if(targetPlayerPosition.x != transform.position.x &&
							!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
								if(isGrounded()) {
									enemyAnimator.SetTrigger("Walk");
								}
						}

						if(isGrounded()) {
							transform.position = Vector2.MoveTowards(transform.position, new Vector2(
								targetPlayerPosition.x,
								transform.position.y
							), speed * Time.deltaTime);
						} else {
							if(enemyAnimator && enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
								enemyAnimator.SetTrigger("Bridge");
							}
						}
					}
				}
			} else {
				if(enemyAnimator && enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Bridge")) {
					enemyAnimator.SetTrigger("Walk");
				}

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

	// Run to damage the enemy
	public virtual void Damage() {
		if(health < 1) {
			return;
		}

		if(_canDamage) {
			health -= 1;
			Health = health;
			enemyAnimator.SetTrigger("Hit");

			_canDamage = false;
			StartCoroutine(WaitForAttack());
		}

		if(health < 1) {
			enemyAnimator.SetTrigger("Death");
		}
	}

	// Check if enemy is grounded
	protected bool isGrounded() {
		float enemyPosX = transform.position.x;

		if(facingRight) {
			enemyPosX += 1.2f;
		} else {
			enemyPosX -= 1.2f;
		}

		// Draw raycast
		Debug.DrawRay(new Vector2(
				enemyPosX,
				transform.position.y
			), Vector3.down * 1.5f, Color.blue);

		RaycastHit2D hit = Physics2D.Raycast(new Vector2(
			enemyPosX,
			transform.position.y
		), Vector2.down, 1.5f, 1 << 8);
		
		if(hit.collider != null) {
			return true;
		}

		return false;
	}

	// Prevent multiple attacks in one frame
	IEnumerator WaitForAttack() {
		yield return new WaitForSeconds(1.0f);
		_canDamage = true;
	}

	// Create random attack sequences
	protected IEnumerator WaitAndAttack() {
		float randomTimeA = Random.value + 1.5f;
		float randomTimeB = Random.value + 0.5f;
		float totalRandomTime = randomTimeA + randomTimeB;

		yield return new WaitForSeconds(totalRandomTime);

		if(enemyAnimator && enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
			enemyAnimator.SetTrigger("Bridge");
		}
		isAttackSelected = false;
	}

	// Allow the enemy to cool down after hit
	IEnumerator HitCoolDown() {
		yield return new WaitForSeconds(1.0f);
		enemyAnimator.SetTrigger("Attack");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy {

	[SerializeField]
	private GameObject _acid;

	// Use this for initialization
	public override void Init() {
		base.Init();
	}

	// Override spider update function
	public override void Update() {
		Movement();
	}

	// Handle movement of the spider
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
						enemyAnimator.SetTrigger("Attack");
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

	// Override enemy damage 
	public override void Damage() {
		if(health < 1) {
			return;
		}

		health -= 1;
		Health = health;

		if(health < 1) {
			enemyAnimator.SetTrigger("Death");
			GameObject intsantiatedGem = Instantiate(_gem, transform.position, Quaternion.identity);
			Gem gem = intsantiatedGem.GetComponent<Gem>();
			if(gem != null) {
				gem.enemyGemCount = gems;
			}
		}
	}

	// Instantiate acid
	public void Attack() {
		GameObject acid = Instantiate(_acid, transform.position, Quaternion.identity);
		AcidEffect acidEffect = acid.GetComponent<AcidEffect>();
		if(acidEffect != null) {
			acidEffect.facingRight = facingRight;
		}
	}
}

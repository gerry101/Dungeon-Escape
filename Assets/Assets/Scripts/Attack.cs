using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	// Run when hitbox collides with another collider
	void OnTriggerEnter2D(Collider2D other) {
		IDamagable idamagable = other.GetComponent<IDamagable>();
		if(idamagable != null) {
			idamagable.Damage();
		}
	}
}

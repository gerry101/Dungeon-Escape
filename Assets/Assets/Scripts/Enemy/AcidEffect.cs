using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour {

	private Vector3 _targetVectorDirection;
	public bool facingRight;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 5.0f);

		if(facingRight) {
			_targetVectorDirection = Vector3.right;
		} else {
			_targetVectorDirection = Vector3.left;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(_targetVectorDirection * 3 * Time.deltaTime);
	}

	// Check for collission with player
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			IDamagable idamagable = other.GetComponent<IDamagable>();
			if(idamagable != null) {
				idamagable.Damage();
				Destroy(gameObject);
			}
		}
	}
}

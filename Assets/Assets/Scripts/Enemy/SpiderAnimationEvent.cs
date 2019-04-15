using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour {

	private Spider _spider;

	// Use this for initialization
	void Start() {
		_spider = transform.parent.GetComponent<Spider>();
	}

	// Fire an acid from the spider
	public void Fire() {
		_spider.Attack();
	}
}

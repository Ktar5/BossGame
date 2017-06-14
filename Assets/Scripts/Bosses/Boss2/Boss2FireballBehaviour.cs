using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2FireballBehaviour : MonoBehaviour {

	public float speed;
	private float lifetime = 3.0f;
	private Vector3 targetPos;

	public void SetTarget(Transform t) {
		targetPos = t.position;
	}

	public void SetSpeed(float s) {speed = s;}

	public void FixedUpdate() {
		Move ();
	}

	public void Move() {

		transform.position += (targetPos - transform.position).normalized * speed;
		lifetime -= .1f;
		if (lifetime <= 0.0f) {
			Destroy (gameObject);
		}

	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Boss2CoreBehaviour : MonoBehaviour {

	public Transform target;
	public Transform leg1;
	public Transform leg2;
	Rigidbody2D mRigidbody2D;

	Vector3 leg1start;
	Vector3 leg2start;

	public float moveSpeed = .7f;
	float sin = 0;

	void Start() {
		mRigidbody2D = GetComponent<Rigidbody2D> ();
		leg1start = leg1.localPosition;
		leg2start = leg2.localPosition;
	}

	void FixedUpdate() {
		Move();
	}

	void Move() {

		mRigidbody2D.velocity = (target.position - transform.position).normalized * moveSpeed;
		Debug.Log (mRigidbody2D.velocity);
		if (sin >= Mathf.PI * 2f) {
			sin = 0.0f;
		} else {
			sin += .3f;
		}

		float v = Mathf.Log10(mRigidbody2D.velocity.sqrMagnitude);

		leg1.localPosition = (leg1start + v * new Vector3(0, (Mathf.Sin (sin) * .15f),0));
		leg2.localPosition = (leg2start + v * new Vector3(0, (Mathf.Sin (sin) * -.15f),0));

	}

}

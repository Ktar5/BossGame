using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Boss2CoreBehaviour : MonoBehaviour {

	public Transform mouth;
	public Transform target;
	public Transform leg1;
	public Transform leg2;

	public Boss2FireballBehaviour fireball;
	public float fireballMissileSpeed;
	public ParticleSystem charging;

	Rigidbody2D mRigidbody2D;

	Vector3 leg1start;
	Vector3 leg2start;

	private bool moving = true;
	public float moveSpeed = .7f;
	float sin = 0;

	void Start() {
		mRigidbody2D = GetComponent<Rigidbody2D> ();
		leg1start = leg1.localPosition;
		leg2start = leg2.localPosition;
		charging.Stop ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.G)) {
			StartCoroutine (Shoot());
		}
	}

	void FixedUpdate() {
		Move();
	}

	IEnumerator Shoot() {
		moving = false;
		charging.Play ();

		yield return new WaitForSeconds (1.5f);

		charging.Stop ();

		for (int i = 0; i < 5; i++) {
			Boss2FireballBehaviour fb = Instantiate (fireball, mouth.position, Quaternion.identity) as Boss2FireballBehaviour;
			fb.SetTarget (target);
			fb.SetSpeed (fireballMissileSpeed);
			yield return new WaitForSeconds (.1f);
		}


		moving = true;

	}

	void Move() {



		Vector2 targetVelocity = (target.position - transform.position).normalized * moveSpeed;

		mRigidbody2D.velocity = Vector2.Lerp (mRigidbody2D.velocity, (moving ? targetVelocity : Vector2.zero), 0.15f);

		if (sin >= Mathf.PI * 2f) {
			sin = 0.0f;
		} else {
			sin += .3f;
		}

		float v = mRigidbody2D.velocity.sqrMagnitude;

		Debug.Log (mRigidbody2D.velocity + " || " + v); 

		leg1.localPosition = (leg1start + v * new Vector3(0, (Mathf.Sin (sin) * .15f),0));
		leg2.localPosition = (leg2start + v * new Vector3(0, (Mathf.Sin (sin) * -.15f),0));

	}

}

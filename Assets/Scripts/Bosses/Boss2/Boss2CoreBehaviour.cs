using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Boss2CoreBehaviour : MonoBehaviour {

	public Transform mouth;
	public Transform target;

	public Transform body;
	private Transform head;
	public Transform leg1;
	public Transform leg2;

	public Boss2FireballBehaviour fireball;
	public float fireballMissileSpeed;
	public ParticleSystem charging;

	Rigidbody2D mRigidbody2D;
	Enemy enemy;

	SpriteRenderer[] renderers;

	Vector3 leg1start;
	Vector3 leg2start;

	private bool moving = true;
	public float moveSpeed = .7f;
	float sin = 0;



	void Start() {

		enemy = GetComponentInChildren<Enemy> ();
		mRigidbody2D = GetComponent<Rigidbody2D> ();
		leg1start = leg1.localPosition;
		leg2start = leg2.localPosition;
		charging.Stop ();
		renderers = new SpriteRenderer[5];

		head = body.GetChild (0);
		renderers [0] = head.gameObject.GetComponent<SpriteRenderer> ();
		renderers [1] = body.gameObject.GetComponent<SpriteRenderer> ();
		renderers [2] = leg1.gameObject.GetComponent<SpriteRenderer> ();
		renderers [3] = leg2.gameObject.GetComponent<SpriteRenderer> ();
		renderers [4] = enemy.gameObject.GetComponent<SpriteRenderer> ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.G)) {
			StartCoroutine (Shoot());
		}
	}

	void FixedUpdate() {
		Recoiling ();
		Move ();
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

	void SetColor(Color c) {
		foreach(SpriteRenderer r in renderers) {

			r.color = Color.Lerp(r.color, c, .5f);

		}
	}

	void Recoiling() {
		if (enemy.isRecoiling ()) {
			Time.timeScale = .5f;
			moving = false;
			SetColor (Color.red);
		} else {
			Time.timeScale = 1.0f;
			moving = true;
			SetColor (Color.white);
		}
	}

	void Move() {

		Vector2 targetVelocity = (target.position - transform.position).normalized * moveSpeed;

		mRigidbody2D.velocity = Vector2.Lerp (mRigidbody2D.velocity, (moving ? targetVelocity : Vector2.zero), 0.15f);

		if (sin >= Mathf.PI * 2f) {
			sin = 0.0f;
		} else {
			sin += .1f;
		}

		float v = mRigidbody2D.velocity.sqrMagnitude;

		leg1.localPosition = (leg1start + v * new Vector3(0, (Mathf.Sin (sin) * .15f),0));
		leg2.localPosition = (leg2start + v * new Vector3(0, (Mathf.Sin (sin) * -.15f),0));

	}

}

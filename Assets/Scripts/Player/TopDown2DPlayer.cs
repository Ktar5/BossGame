﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class TopDown2DPlayer : MonoBehaviour {



	public enum Direction {
		UP,
		DOWN,
		LEFT,
		RIGHT,
		NONE
	}


	[System.Serializable]
	public class Movement {



		public float speed = 7f; /*speed of the player*/
		public float smoothSpeed = .6f; /*smoothing velocity of the player. Higher values mean less smoothing.*/
		public Direction facing; /*faced direction.*/
		//Hidden
		[HideInInspector]
		public BoxCollider2D col;
		[HideInInspector]
		public bool isMoving;
		[HideInInspector]
		public Animator anim;
		[HideInInspector]
		public Vector2 movementDirection; /*direction of the player. Normalized to input.*/
		[HideInInspector]
		public Vector3 displacement; /*final displacement vector of player. Used in transform calculation.*/
		[HideInInspector]
		public Rigidbody2D body; /*attached rigidbody component*/
		[HideInInspector]
		public bool immobilized;
	}

	[System.Serializable]
	public class Combat {

		public bool useMouse;
		public int slashDamage;
		[HideInInspector]
		public Camera cam;

		[HideInInspector]
		public Vector2 mousePos;
		[HideInInspector]
		public bool slash;
		[HideInInspector]
		public bool slashing;
		[HideInInspector]
		public CircleCollider2D swordCol;

	}

	public Movement movement; /*Movement object.*/
	public Combat combat;

	/*
	START
	Run first frame.
	*/
	void Start() {
		InitComponents ();
	}

	/*
	INIT RIGIDBODY
	Initialize some values for the Rigidbody object.
	*/
	void InitComponents() {
		combat.cam = Camera.main;
		combat.swordCol = GetComponent<CircleCollider2D> ();
		combat.swordCol.isTrigger = true;
		movement.col = GetComponent<BoxCollider2D> ();
		movement.anim = GetComponent<Animator> ();
		movement.body = GetComponent<Rigidbody2D> ();
		movement.body.isKinematic = false;
		movement.body.gravityScale = 0.0f;
		movement.body.freezeRotation = true;
	}

	public Direction Vector2ToDirection(Vector2 v) {
		if (v.Equals (Vector2.up)) {
			return Direction.UP;
		}

		if (v.Equals (Vector2.down)) {
			return Direction.DOWN;
		}

		if (v.Equals (Vector2.left)) {
			return Direction.LEFT;
		}

		if (v.Equals (Vector2.right)) {
			return Direction.RIGHT;
		}

		return Direction.NONE;

	}

	public Vector2 DirectionToVector2(Direction d) {

		switch (d) {
		case Direction.DOWN:
			return Vector2.down;
		case Direction.LEFT:
			return Vector2.left;
		case Direction.RIGHT:
			return Vector2.right;
		case Direction.UP:
			return Vector2.up;
			default:
			return Vector2.zero;
		}

	}

	public void Animate() {



		Debug.Log (!movement.anim.GetCurrentAnimatorStateInfo (0).IsName ("Slash"));

		if (!movement.anim.GetCurrentAnimatorStateInfo (0).IsName ("Slash")) {
			movement.immobilized = false;
		}

		if (combat.slash) {
			movement.facing = Vector2ToDirection (combat.mousePos);
			movement.immobilized = true;
			movement.anim.SetTrigger ("attack");
			combat.slash = false;
		}

		movement.anim.SetBool ("isMoving", movement.isMoving);

		Vector2 v = DirectionToVector2 (movement.facing);
		float a = .25f+((Mathf.Atan2 (-v.x, v.y))/(Mathf.PI*2));
		if (a < 0) {
			a += 1.0f;
		}

		movement.anim.SetFloat ("direction", a);
	}

	/*
	MOVE
	Main movement function. Moves the player according to input.
	*/
	public void Move() {

		combat.swordCol.offset = DirectionToVector2 (movement.facing) * .5f;

		if (!movement.immobilized) {

			movement.movementDirection = new Vector2 (
				(Input.GetAxisRaw ("Horizontal")),
				(Input.GetAxisRaw ("Vertical"))).normalized; /*Set direction to the normalized input vector.*/

			Direction dir = Vector2ToDirection (new Vector2(Mathf.Round(movement.movementDirection.x), Mathf.Round(movement.movementDirection.y)));
			if (dir != Direction.NONE) {
				movement.facing = dir;
			}

			movement.displacement = 
				Vector3.Lerp(
					movement.displacement, 
					movement.movementDirection * movement.speed, 
					movement.smoothSpeed); /*Set displacement accordingly. LERP for smoothing.*/




				movement.body.velocity = movement.displacement; /*Apply the displacement to the rigidbody.*/
		} else {
			movement.body.velocity = Vector2.Lerp (movement.body.velocity, Vector2.zero, .1f);

		}
		movement.isMoving = (movement.body.velocity.sqrMagnitude > 0.2);

	}

	void OnTriggerStay2D(Collider2D other) {

		//Debug.Log ("Found enemy!");

		if (combat.slashing) {

			Debug.Log ("Slashing!");

			Enemy enemy = other.GetComponent<Enemy> ();

			if (enemy != null) {

				Debug.Log ("Enemy Damaged!");

				enemy.Damage(combat.slashDamage);

			}

		}
	}

	/*These are for animations*/
	public void StartSlash() {
		combat.slashing = true;
		movement.immobilized = true;
		movement.facing = Vector2ToDirection (combat.mousePos);
	}

	public void EndSlash() {
		combat.slashing = false;
		//movement.immobilized = false;
	}

	public void IO() {
		combat.slash = (Input.GetButtonDown("Fire1")? true : combat.slash);
        
		combat.mousePos = (Input.mousePosition) - combat.cam.WorldToScreenPoint (transform.position);
		combat.mousePos.Normalize ();
		combat.mousePos = (Mathf.Abs (combat.mousePos.x) > Mathf.Abs (combat.mousePos.y)) ?
			new Vector2 (Mathf.Round(combat.mousePos.x), 0) :
			new Vector2 (0, Mathf.Round(combat.mousePos.y));
<<<<<<< HEAD
    }
=======

		Debug.Log (combat.mousePos);

	}
>>>>>>> origin/master

	public void Update() {
		IO ();
	}

	/*
	FIXED UPDATE
	Normalized physics update function.
	*/
	public void FixedUpdate() {
		Move ();
		Animate ();
	}

}

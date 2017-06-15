using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
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
		public float smoothSpeed = 12f; /*smoothing velocity of the player. Higher values mean less smoothing.*/
		public Direction facing; /*faced direction.*/

		[HideInInspector]
		public Vector2 movementDirection; /*direction of the player. Normalized to input.*/
		[HideInInspector]
		public Vector3 displacement; /*final displacement vector of player. Used in transform calculation.*/
		[HideInInspector]
		public Rigidbody2D body; /*attached rigidbody component*/
		
	}

	public Movement movement; /*Movement object.*/

	/*
	START
	Run first frame.
	*/
	void Start() {
		InitRigidBody ();
	}

	/*
	INIT RIGIDBODY
	Initialize some values for the Rigidbody object.
	*/
	void InitRigidBody() {
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

	/*
	MOVE
	Main movement function. Moves the player according to input.
	*/
	public void Move() {
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
				movement.movementDirection * movement.speed * Time.fixedDeltaTime, 
				Time.fixedDeltaTime * movement.smoothSpeed); /*Set displacement accordingly. LERP for smoothing.*/

		movement.body.MovePosition(transform.position + movement.displacement); /*Apply the displacement to the rigidbody.*/
	}

	/*
	FIXED UPDATE
	Normalized physics update function.
	*/
	public void FixedUpdate() {
		Move();
	}

}

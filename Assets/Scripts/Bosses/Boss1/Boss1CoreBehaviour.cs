using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1CoreBehaviour : MonoBehaviour {

	Boss1HandBehaviour[] hands;



	public enum AttackMode {
		SLAM,
		SPIRAL,
		CLAP,
		PUSH
	}

	public AttackMode attackMode;

	//ATTACK VARS

	//SPIRAL
	float spiral_sin = -1f;
	float spiral_speed = .02f;
	[Range(0,1)]
	public float spiral_time = 0;
	[Range(1,2)]
	public int phase = 1;

	public void Start() {
		hands = GetComponentsInChildren<Boss1HandBehaviour> ();
	}

	public void FixedUpdate() {

		switch (attackMode) {
		case AttackMode.SPIRAL:
		default:
			Spiral (1);
			break;
		}

	}

	public bool Slam() {
		return false;
	}

	public bool Spiral(float distance) {

		float speed = Mathf.Lerp (spiral_speed, spiral_speed * 3, spiral_time);
		float magnitude = Mathf.Lerp (1, 6, spiral_time);

		for (int i = 0; i < hands.Length; i++) {

			Boss1HandBehaviour hand = hands[i];

			Transform t = hand.gameObject.transform;

			float offset = i * ((2 * Mathf.PI) / hands.Length);

			t.position = new Vector3 (
				transform.position.x + Mathf.Sin (spiral_sin + offset) * distance * magnitude,
				transform.position.y + Mathf.Cos (spiral_sin + offset) * distance * magnitude,
				0);

			spiral_sin += speed;

		}

		spiral_time += 0.005f;

		if (spiral_time > 1) {
			spiral_time = 0f;
			return true;
		} else {
			return false;
		}

	}

	public bool Clap() {
		return false;
	}

	public bool Push(float distance) {

		return false;

	}

}